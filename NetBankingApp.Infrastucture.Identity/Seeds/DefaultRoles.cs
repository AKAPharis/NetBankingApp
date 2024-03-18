using Microsoft.AspNetCore.Identity;
using NetBankingApp.Core.Application.Enums;
using NetBankingApp.Infrastucture.Identity.Models;

namespace NetBankingApp.Infrastucture.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<BankingUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Customer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

        }
    }
}
