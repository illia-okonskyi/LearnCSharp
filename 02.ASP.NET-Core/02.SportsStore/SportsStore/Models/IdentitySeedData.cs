using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    public static class IdentitySeedData
    {
        // NOTE: It seems that IdentityUser uses the userName as userId too
        private const string AdminUserId = "admin";
        private const string AdminUserName = AdminUserId;
        private const string AdminUserPassword = "Secret123$";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            var userManager = app.ApplicationServices.GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser adminUser = await userManager.FindByIdAsync(AdminUserId);
            if (adminUser == null)
            {
                adminUser = new IdentityUser(AdminUserName);
                await userManager.CreateAsync(adminUser, AdminUserPassword);
            }
        }
    }
}
