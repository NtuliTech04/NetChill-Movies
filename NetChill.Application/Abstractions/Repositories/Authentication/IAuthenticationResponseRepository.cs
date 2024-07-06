using NetChill.Application.DTOs.Account;
using NetChill.Application.DTOs.Account.Login;
using NetChill.Application.DTOs.Account.SignUp;

namespace NetChill.Application.Abstractions.Repositories.Authentication
{
    public interface IAuthenticationResponseRepository
    {
        Task<AuthenticationResponse> SignUpAsync(SignUpDto signUpDto, string origin);
        Task<string> ConfirmEmailAsync(string userId, string code);

        Task<string> AssignRolesAsync(UserRoleDto userRolesDto);

        Task<AuthenticationResponse> LoginAsync(LoginDto loginDto);

        Task<AuthenticationResponse> RefreshTokenCheckAsync(string token);

        Task<bool> RevokeTokenAsync(string token);
    }
}
