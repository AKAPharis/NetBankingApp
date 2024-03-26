
using Microsoft.AspNetCore.Mvc;
using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.Account;
using NetBankingApp.Core.Application.Helpers;
using NetBankingApp.Infrastucture.Identity.Seeds;
using Microsoft.AspNetCore.Authorization;
using NetBankingApp.Core.Application.Enums;
using NetBankingApp.Core.Application.ViewModels.Home;

namespace WebApp.NetBankingApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICreditCardService _creditCardService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly ILoanService _loanService;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserController(IUserService userService, ICreditCardService creditCardService, ISavingAccountService savingAccountService, ILoanService loanService, IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _creditCardService = creditCardService;
            _savingAccountService = savingAccountService;
            _loanService = loanService;
            _contextAccessor = contextAccessor;
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeactivateUser(string Id)
        {
            await _userService.DeactivateUser(Id);
            return RedirectToAction("AdminUser");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActivateUser(string Id)
        {
            await _userService.ActivateUser(Id);
            return RedirectToAction("AdminUser");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminUser()
        {
            return View(await _userService.GetAll());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserProducts(string Id)
        {
            CustomerHomeViewModel home = new();
            home.IdCustomer = Id;
            home.Loans = await _loanService.GetByCustomer(Id);
            home.CreditCards = await _creditCardService.GetByCustomer(Id);
            home.SavingAccounts = await _savingAccountService.GetByCustomer(Id);

            return View(home);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View("SaveUser", new SaveUserViewModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveUser", vm);
            }
            var origin = Request.Headers["origin"];
            RegisterResponse response = await _userService.RegisterAsync(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View("SaveUser", vm);
            }
            return RedirectToAction("AdminUser");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string Id)
        {
            SaveUserViewModel vm = await _userService.GetByIdSaveViewModelAsync(Id);
            return View("SaveUser", vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveUser", vm);
            }
            var origin = Request.Headers["origin"];
            EditResponse response = await _userService.EditAsync(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View("SaveUser", vm);
            }
            return RedirectToAction("AdminUser");
        }

        [Authorize]
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

        public IActionResult ConfirmEmail()
        {
            return View();
        }
    }
}
