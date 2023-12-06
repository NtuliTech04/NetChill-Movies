using Microsoft.EntityFrameworkCore;

namespace NetChill.Application.DTOs.Account
{
    [Owned]
    public class RefreshTokenDto
    {
        public string Token { get; set; }
        public DateTime ExpireOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpireOn;

        //Time when token is created
        public DateTime CreatedOn { get; set; }

        //Time when token is revoked/canceled
        public DateTime? RevokedOn { get; set; }

        //When token is not revoked and isn't expired => It's Active
        public bool IsActive => RevokedOn == null && !IsExpired;

    }
}
