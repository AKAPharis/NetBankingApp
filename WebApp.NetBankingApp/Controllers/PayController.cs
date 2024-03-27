using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.Payment;
using NetBankingApp.Core.Application.Helpers;
using NetBankingApp.Core.Domain.Models;

namespace WebApp.NetBankingApp.Controllers
{
    [Authorize]
    public class PayController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly ICreditCardService _creditCardService;
        private readonly ILoanService _loanService;
        private readonly IHttpContextAccessor _contextAccessor;

        public PayController(IPaymentService paymentService, ISavingAccountService savingAccountService, ICreditCardService creditCardService, ILoanService loanService, IHttpContextAccessor contextAccessor)
        {
            _paymentService = paymentService;
            _savingAccountService = savingAccountService;
            _creditCardService = creditCardService;
            _loanService = loanService;
            _contextAccessor = contextAccessor;
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> LoanPayment()
        {
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;

            LoanPaymentViewModel vm = new();
            vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
            vm.Loans = await _loanService.GetByCustomer(idCustomer);
            return View(vm);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> LoanPayment(LoanPaymentViewModel vm)
        {
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;
            if (!ModelState.IsValid)
            {
                vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
                vm.Loans = await _loanService.GetByCustomer(idCustomer);
                return View(vm);
            }
            var response = await _paymentService.LoanPayment(vm);
            if (response.HasError)
            {
                vm.Error = response.Error;
                vm.HasError = response.HasError;
                vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
                vm.Loans = await _loanService.GetByCustomer(idCustomer);
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Home", action = "CustomerHome" });
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreditPayment()
        {
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;

            CreditPaymentViewModel vm = new();
            vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
            vm.CreditCards = await _creditCardService.GetByCustomer(idCustomer);
            return View(vm);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreditPayment(CreditPaymentViewModel vm)
        {
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;
            if (!ModelState.IsValid)
            {
                vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
                vm.CreditCards = await _creditCardService.GetByCustomer(idCustomer);
                return View(vm);
            }
            var response = await _paymentService.CreditPayment(vm);
            if (response.HasError)
            {
                vm.Error = response.Error;
                vm.HasError = response.HasError;
                vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
                vm.CreditCards = await _creditCardService.GetByCustomer(idCustomer);
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Home", action = "CustomerHome" });
        }
    }
}
