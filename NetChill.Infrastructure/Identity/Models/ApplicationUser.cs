using NetChill.Application.DTOs.Account;
using Microsoft.AspNetCore.Identity;

namespace NetChill.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<RefreshTokenDto>? RefreshTokens { get; set; }
    }
}
