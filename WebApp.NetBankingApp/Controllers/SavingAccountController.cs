using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.Loan;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;

namespace WebApp.NetBankingApp.Controllers
{
    [Authorize]
    public class SavingAccountController : Controller
    {
        private readonly ISavingAccountService _savingAccountService;

        public SavingAccountController(ISavingAccountService savingAccountService)
        {
            _savingAccountService = savingAccountService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public IActionResult AddSavingAccount(string id)
        {
            return View(new SaveSavingAccountViewModel { IdCustomer = id });
        }

        [HttpPost]
        public async Task<IActionResult> AddSavingAccount(SaveSavingAccountViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _savingAccountService.CreateAsync(vm);
            return RedirectToRoute(new { controller = "User", action = "AdminUser" });
        }
    }
}
