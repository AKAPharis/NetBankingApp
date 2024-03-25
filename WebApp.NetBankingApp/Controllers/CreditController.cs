using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.CreditCard;
using NetBankingApp.Core.Application.Helpers;

namespace WebApp.NetBankingApp.Controllers
{
    [Authorize]
    public class CreditController : Controller
    {
        private readonly ICreditCardService _creditCardService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IHttpContextAccessor _contextAccessor;

        public CreditController(ICreditCardService creditCardService, ISavingAccountService savingAccountService, IHttpContextAccessor contextAccessor)
        {
            _creditCardService = creditCardService;
            _savingAccountService = savingAccountService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> AdvanceCredit()
        {
            AdvanceCreditViewModel vm = new();
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;

            vm.CreditCards = await _creditCardService.GetByCustomer(idCustomer);
            vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AdvanceCredit(AdvanceCreditViewModel vm)
        {
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;

            if (!ModelState.IsValid)
            {
                vm.CreditCards = await _creditCardService.GetByCustomer(idCustomer);
                vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
                return View(vm);
            }
            var response = await _creditCardService.AdvanceCredit(vm);
            if (response.HasError)
            {

                vm.CreditCards = await _creditCardService.GetByCustomer(idCustomer);
                vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
                vm.Error = response.Error;
                vm.HasError = response.HasError;
                return View(vm);

            }
            return RedirectToRoute(new { controller = "Credit", action = "Index" });
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
