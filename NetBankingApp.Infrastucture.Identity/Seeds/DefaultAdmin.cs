using Microsoft.AspNetCore.Identity;
using NetBankingApp.Core.Application.Enums;
using NetBankingApp.Infrastucture.Identity.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Infrastucture.Identity.Seeds
{
    public static class DefaultAdmin
    {
        public static async Task SeedAsync(UserManager<BankingUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            BankingUser defaultUser = new();
            defaultUser.UserName = "defaultAdmin";
            defaultUser.Email = "defaultadmin@gmail.com";
            defaultUser.FirstName = "Default";
            defaultUser.LastName = "Admin";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }

        }
    }
}
