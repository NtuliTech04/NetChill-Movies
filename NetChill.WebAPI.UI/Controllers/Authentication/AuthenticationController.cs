using NetChill.Application.DTOs.Account;
using NetChill.Application.DTOs.Account.Login;
using NetChill.Application.DTOs.Account.SignUp;
using Microsoft.AspNetCore.Mvc;
using NetChill.Application.Abstractions.Repositories.Authentication;

namespace NetChill.WebAPI.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationResponseRepository _authService;

        public AuthenticationController(IAuthenticationResponseRepository authService)
        {
            _authService = authService;
        }


        #region Set Refresh Token In Cookies

        private void SetRefreshTokenInCookies(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime()
            };

            //cookieOptionsExpires = DateTime.UtcNow.AddSeconds(cookieOptions.Timeout);

            Response.Cookies.Append("refreshTokenKey", refreshToken, cookieOptions);
        }

        #endregion

        #region Register Endpoint
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUpAsync([FromForm] SignUpDto signUpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var origin = Request.Headers["origin"];
            var result = await _authService.SignUpAsync(signUpDto, origin);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            //store the refresh token in a cookie
            SetRefreshTokenInCookies(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        #endregion

        #region Login Endpoint

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(loginDto);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            //Check if the user has a refresh token or not , to store it in a cookie
            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenInCookies(result.RefreshToken, result.RefreshTokenExpiration);
            }

            return Ok(result);
        }

        #endregion

        #region Assign Role Endpoint
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRoleAsync(UserRoleDto userRoleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.AssignRolesAsync(userRoleDto);

            if (!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            return Ok(userRoleDto);
        }

        #endregion

        #region Refresh Token Check Endpoint

        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshTokenCheckAsync()
        {
            var refreshToken = Request.Cookies["refreshTokenKey"];

            var result = await _authService.RefreshTokenCheckAsync(refreshToken);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        #endregion

        #region Revoke Token Async

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeTokenAsync(RevokeTokenDto revokeTokenDto)
        {
            var refreshToken = revokeTokenDto.Token ?? Request.Cookies["refreshTokenKey"];

            //Check for a token
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest("Token is required");
            }

            await _authService.RevokeTokenAsync(refreshToken);

            //Check if there is a problem with "result"
            //if (!result)
            //    return BadRequest("Token is Invalid");

            return Ok("Token Revoked");
        }
        #endregion

        #region Confirm Email

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            return Ok(await _authService.ConfirmEmailAsync(userId, code));
        }

        #endregion
    }
}
