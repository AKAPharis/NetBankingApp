using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.Transaction;
using NetBankingApp.Core.Application.Helpers;

namespace WebApp.NetBankingApp.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IBeneficiaryService _beneficiaryService;

        private readonly IHttpContextAccessor _contextAccessor;

        public TransactionController(ITransactionService transactionService, ISavingAccountService savingAccountService, IBeneficiaryService beneficiaryService, IHttpContextAccessor contextAccessor)
        {
            _transactionService = transactionService;
            _savingAccountService = savingAccountService;
            _beneficiaryService = beneficiaryService;
            _contextAccessor = contextAccessor;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TransferToAccount()
        {
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;
            TransactionToAccountViewModel vm = new();
            vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> TransferToAccount(TransactionToAccountViewModel vm)
        {
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;

            if (!ModelState.IsValid)
            {
                vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
                return View(vm);
            }
            var response = await _transactionService.TransactionToAccount(vm);
            if (response.HasError)
            {
                vm.Error = response.Error;
                vm.HasError = response.HasError;
                vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
                return View(vm);    
            }
            return RedirectToRoute(new { controller = "Transaction", action = "Index" });
        }
        public async Task<IActionResult> TransferExpress()
        {
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;
            TransactionExpressViewModel vm = new();
            vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> TransferExpress(TransactionExpressViewModel vm)
        {
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;

            if (!ModelState.IsValid)
            {
                vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
                return View(vm);
            }
            var response = await _transactionService.TransactionExpress(vm);
            if (response.HasError)
            {
                vm.Error = response.Error;
                vm.HasError = response.HasError;
                vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Transaction", action = "Index" });
        }
        public async Task<IActionResult> TransferToBeneficiary()
        {
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;
            TransactionBeneficiaryViewModel vm = new();
            vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
            vm.Beneficiaries = await _beneficiaryService.GetBenficiaries(idCustomer);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> TransferToBeneficiary(TransactionBeneficiaryViewModel vm)
        {
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;
            vm.IdUser = idCustomer;
            if (!ModelState.IsValid)
            {
                vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
                vm.Beneficiaries = await _beneficiaryService.GetBenficiaries(idCustomer);
                return View(vm);
            }
            var response = await _transactionService.TransactionBeneficiary(vm);
            if (response.HasError)
            {
                vm.Error = response.Error;
                vm.HasError = response.HasError;
                vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
                vm.Beneficiaries = await _beneficiaryService.GetBenficiaries(idCustomer);
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Transaction", action = "Index" });
        }
    }
}
