using System.Text.Json.Serialization;

namespace NetChill.Application.DTOs.Account
{
    public class AuthenticationResponse
    {
        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; }           //true by default
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
        public DateTime? TokeExpirationUtc { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }
    }
}
