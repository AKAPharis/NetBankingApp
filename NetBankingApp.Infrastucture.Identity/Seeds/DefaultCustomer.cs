using Microsoft.AspNetCore.Identity;
using NetBankingApp.Core.Application.Enums;
using NetBankingApp.Infrastucture.Identity.Models;

namespace NetBankingApp.Infrastucture.Identity.Seeds
{
    public static class DefaultCustomer
    {
        public static async Task SeedAsync(UserManager<BankingUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            BankingUser defaultUser = new();
            defaultUser.UserName = "defaultCustomer";
            defaultUser.Email = "defaultcustomer@gmail.com";
            defaultUser.FirstName = "Default";
            defaultUser.LastName = "Customer";
            defaultUser.DocumentId = "001-0250013-1";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;
            defaultUser.IsActived = true;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Customer.ToString());
                }
            }

        }
    }
}
