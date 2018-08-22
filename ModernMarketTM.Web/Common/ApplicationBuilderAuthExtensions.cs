using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ModernMarketTM.Models;

namespace ModernMarketTM.Web.Common
{
    public static class ApplicationBuilderAuthExtensions
    {

        private static IdentityRole[] roles =
        {
            new IdentityRole("Admin"),
            new IdentityRole("Supplier"),
        };

        private const string DefaultAdminPassword = "admin123";
        private const string DefaultAdminHome = "AdminHome N17";

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

                var user = await userManager.FindByNameAsync("admin");
                if (user == null)
                {
                    user = new User()
                    {
                        UserName = "admin",
                        Email = "admin@example.com",
                        FullName = "Admin",
                        Address = DefaultAdminHome
                    };

                    await userManager.CreateAsync(user, DefaultAdminPassword);
                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, roles[0].Name));
                    await userManager.AddToRoleAsync(user, roles[0].Name);
                }
            }
        }
    }
}
