using Microsoft.AspNetCore.Mvc;
using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.Account;
using NetBankingApp.Core.Application.Helpers;
using NetBankingApp.Infrastucture.Identity.Seeds;
using Microsoft.AspNetCore.Authorization;
using NetBankingApp.Core.Application.Enums;

namespace WebApp.NetBankingApp.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);

                if (userVm.Roles.Any(r => r == "Customer"))
                {
                    return RedirectToRoute(new { controller = "Home", action = "CustomerHome" });
                }
                else
                {
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }

        public async Task<IActionResult> AdminUser()
        {
            var aa = await _userService.GetAll();
            return View();
        }

        public IActionResult Create()
        {
            return View("SaveUser");
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
