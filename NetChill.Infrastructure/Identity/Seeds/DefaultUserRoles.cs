using NetChill.Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Identity;

namespace NetChill.Infrastructure.Identity.Seeds
{
    public class DefaultUserRoles
    {
        public static async Task SeedUserRoles(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User.ToString()));
        }
    }
}
