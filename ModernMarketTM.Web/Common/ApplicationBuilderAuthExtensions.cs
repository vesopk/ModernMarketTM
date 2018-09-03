using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Constants;

namespace ModernMarketTM.Web.Common
{
    public static class ApplicationBuilderAuthExtensions
    {

        private static IdentityRole[] roles =
        {
            new IdentityRole("Admin"),
            new IdentityRole("Supplier"),
            new IdentityRole("Operator"),
        };


        public static async void SeedDatabase(
            this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();

            using (scope)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

                var user = await userManager.FindByNameAsync(AdminConstants.DefaultAdminUsername);
                if (user == null)
                {
                    user = new User()
                    {
                        UserName = AdminConstants.DefaultAdminUsername,
                        Email = AdminConstants.DefaultAdminEmail,
                        FullName = AdminConstants.DefaultAdminFullName,
                        Address = AdminConstants.DefaultAdminHome
                    };

                    await userManager.CreateAsync(user, AdminConstants.DefaultAdminPassword);
                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, roles[0].Name));
                    await userManager.AddToRoleAsync(user, roles[0].Name);
                }
            }
        }
    }
}
