using NetChill.Application.DTOs.Account;
using NetChill.Application.DTOs.Account.Login;
using NetChill.Application.DTOs.Account.SignUp;

namespace NetChill.Application.Interfaces.Repositories.Authentication
{
    public interface IAuthenticationResponseRepository
    {
        Task<AuthenticationResponse> SignUpAsync(SignUpDto signUpDto, string origin);

        Task<AuthenticationResponse> LoginAsync(LoginDto loginDto);

        Task<string> AssignRolesAsync(UserRoleDto userRolesDto);

        Task<AuthenticationResponse> RefreshTokenCheckAsync(string token);

        Task<bool> RevokeTokenAsync(string token);

        Task<string> ConfirmEmailAsync(string userId, string code);
    }
}
