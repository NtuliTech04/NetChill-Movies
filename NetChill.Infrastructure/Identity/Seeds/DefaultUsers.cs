using NetChill.Infrastructure.Identity.Enums;
using NetChill.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace NetChill.Infrastructure.Identity.Seeds
{
    public class DefaultUsers
    {
        public static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            #region First User - SuperAdmin
            var appUser1 = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@netchill.net",
                FirstName = "NetChill",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(x => x.Id != appUser1.Id))
            {
                var user = await userManager.FindByEmailAsync(appUser1.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(appUser1, "123456");
                    await userManager.AddToRoleAsync(appUser1, UserRoles.SuperAdmin.ToString());
                }
            }
            #endregion

            #region User - Default
            var appUser2 = new ApplicationUser()
            {
                UserName = "yanga.ntuli",
                Email = "yanga.ntuli@netchill.net",
                FirstName = "Yanga",
                LastName = "Ntuli",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(x => x.Id != appUser2.Id))
            {
                var user = await userManager.FindByEmailAsync(appUser2.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(appUser2, "123456");
                    await userManager.AddToRoleAsync(appUser2, UserRoles.User.ToString());
                }
            }
            #endregion
        }
    }
}
