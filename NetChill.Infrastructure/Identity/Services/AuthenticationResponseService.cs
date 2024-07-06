using NetChill.Application.DTOs.Account;
using NetChill.Application.DTOs.Account.Login;
using NetChill.Application.DTOs.Account.SignUp;
using NetChill.Application.DTOs.Email;
using NetChill.Domain.ValueObjects;
using NetChill.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using NetChill.Application.Abstractions.Repositories.Authentication;
using NetChill.Application.Abstractions;

namespace NetChill.Infrastructure.Identity.Services
{
    public class AuthenticationResponseService : IAuthenticationResponseRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailSender;
        private readonly JWTValues _Jwt;

        public AuthenticationResponseService
        (
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailSender,
            IOptions<JWTValues> jwt
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _Jwt = jwt.Value;
        }

        #region Create JSON Web Token
        private async Task<JwtSecurityToken> CreateJwtAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userId", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            //Generate the symmetric security key by the s.key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Jwt.Key));

            //Generate the signing credentials by symmetric security key
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //Define the values used to create a JWT
            var jwtSecurityToken = new JwtSecurityToken
                (
                issuer: _Jwt.Issuer,
                audience: _Jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_Jwt.DurationInMinutes),
                signingCredentials: signingCredentials
                );

            return jwtSecurityToken;
        }
        #endregion

        #region Generate Refresh Token
        private RefreshTokenDto GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            // Disable the warning.
            #pragma warning disable SYSLIB0023

            // Code that uses obsolete API.
            var generator = new RNGCryptoServiceProvider();

            // Re-enable the warning.
            #pragma warning restore SYSLIB0023

            generator.GetBytes(randomNumber);

            return new RefreshTokenDto
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpireOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow,
            };
        }
        #endregion

        #region User Registration
        public async Task<AuthenticationResponse> SignUpAsync(SignUpDto signUpDto, string origin)
        {
            var authentication = new AuthenticationResponse();
            var mailAddress = new MailAddress(signUpDto.Email); //To get Username from Email

            var userEmail = await _userManager.FindByEmailAsync(signUpDto.Email);

            //Checking Email Existence
            if (userEmail != null)
            {
                return new AuthenticationResponse { Message = "This email already exist !" };
            }

            //Map Model Data
            var user = new ApplicationUser()
            {
                FirstName = signUpDto.FirstName,
                LastName = signUpDto.LastName,
                UserName = mailAddress.User,
                Email = signUpDto.Email,
            };

            var result = await _userManager.CreateAsync(user, signUpDto.Password);

            //Check result
            if(!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }

                return new AuthenticationResponse { Message = errors };
            }

            //Assign role to user
            await _userManager.AddToRoleAsync(user, "User");

            #region SendVerificationEmail Content
            var verificationUri = await SendVerificationEmail(user, origin);

            await _emailSender.SendEmailAsync(new EmailRequestDto()
            {
                EmailTo = user.Email,
                Subject = "User Email Verification",
                Body = $"Please confirm your account by clicking on this link: <a href=\"{verificationUri}\">Verify Email<a/>"
            });
            #endregion


            var jwtSecurityToken = await CreateJwtAsync(user);

            authentication.Email = user.Email;
            authentication.Roles = new List<string> { "User" };
            authentication.IsAuthenticated = true;
            authentication.UserName = user.UserName;
            authentication.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authentication.TokeExpirationUtc = jwtSecurityToken.ValidTo.ToLocalTime();
            authentication.Message = "User Registration Succeeded.";

            //Refresh Token
            var newRefreshToken = GenerateRefreshToken();
            authentication.RefreshToken = newRefreshToken.Token;
            authentication.RefreshTokenExpiration = newRefreshToken.ExpireOn;

            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            return authentication;
        }
        #endregion

        #region User Login
        public async Task<AuthenticationResponse> LoginAsync(LoginDto loginDto)
        {
            var authentication = new AuthenticationResponse();

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            var userPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (user == null || !userPassword) 
            {
                authentication.Message = "Incorrect Email or Password.";
                return authentication;
            }

            var jwtSecurityToken = await CreateJwtAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            authentication.Email = user.Email;
            authentication.Roles = roles.ToList();
            authentication.IsAuthenticated = true;
            authentication.UserName = user.UserName;
            authentication.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authentication.TokeExpirationUtc = jwtSecurityToken.ValidTo;
            authentication.Message = "Login Succeeded";

            //Check if user has any active refresh tokens
            if (user.RefreshTokens.Any(x => x.IsActive)) 
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(x => x.IsActive);
                authentication.RefreshToken = activeRefreshToken.Token;
                authentication.RefreshTokenExpiration = activeRefreshToken.ExpireOn;
            }
            else 
            {
                //Generate new token if user has no active refresh token 
                var newRefreshToken = GenerateRefreshToken();

                authentication.RefreshToken = newRefreshToken.Token;
                authentication.RefreshTokenExpiration = newRefreshToken.ExpireOn;

                user.RefreshTokens.Add(newRefreshToken);
                await _userManager.UpdateAsync(user);
            }

            return authentication;
        }

        #endregion

        #region Assign Roles
        public async Task<string> AssignRolesAsync(UserRoleDto userRolesDto)
        {
            var user = await _userManager.FindByIdAsync(userRolesDto.UserId);
            var role = await _roleManager.RoleExistsAsync(userRolesDto.Role);

            //Check the user Id and role
            if (user == null || !role) 
            {
                return "Invalid User or Role.";
            }

            //Check if user is already assigned to selected roles
            if (await _userManager.IsInRoleAsync(user, userRolesDto.Role))
            {
                return "This user has already been assigned to this role.";
            }

            var result = await _userManager.AddToRoleAsync(user, userRolesDto.Role);

            //Check result
            if (!result.Succeeded)
            {
                return "Something went wrong.";
            }
            else
            {
                return $"{role} role has been successfully assigned to user: {user.Id}.";
            }
        }
        #endregion

        #region Check Refresh Token
        public async Task<AuthenticationResponse> RefreshTokenCheckAsync(string token)
        {
            var authentication = new AuthenticationResponse();

            //Find the user that match the sent refresh token
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshTokens.Any(x => x.Token == token));

            if (user == null)
            {
                authentication.Message = "Invalid Token.";
                return authentication;
            }

            //Check if the refresh token is active
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
            {
                authentication.Message = "Inactive Token.";
                return authentication;
            }

            //Revoke/Cancel the sent refresh token
            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtSecurityToken = await CreateJwtAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            authentication.Email = user.Email;
            authentication.Roles = roles.ToList();
            authentication.IsAuthenticated = true;
            authentication.UserName = user.UserName;
            authentication.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authentication.TokeExpirationUtc = jwtSecurityToken.ValidTo;
            authentication.RefreshToken = newRefreshToken.Token;
            authentication.RefreshTokenExpiration = newRefreshToken.ExpireOn;

            return authentication;
        }

        #endregion

        #region Revoke Token
        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.RefreshTokens.Any(y => y.Token == token));

            if (user == null)
            {
                return false;
            }

            //Check if refresh token is active
            var refreshToken = user.RefreshTokens.Single(x=>x.Token == token);

            if (!refreshToken.IsActive)
            {
                return false;
            }

            //Revoke the sent refresh tokens
            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();

            await _userManager.UpdateAsync(user);

            return true;
        }

        #endregion

        #region Send Verification Email
        private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var route = "api/authentication/confirm-email/";
            var _endpointUri = new Uri(string.Concat($"{origin}/", route));

            var verificationUri = QueryHelpers.AddQueryString(_endpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);

            return verificationUri;
        }

        public async Task<string> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return $"Account Confirmed for email: {user.Email}. You can now use the /api/Account/authentication endpoint.";
            }
            else
            {
                throw new Exception($"An error occured while confirming email: {user.Email}.");
            }
        }

        #endregion
    }
}
