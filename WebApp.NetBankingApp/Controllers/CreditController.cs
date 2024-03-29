﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.CreditCard;
using NetBankingApp.Core.Application.Helpers;
using NetBankingApp.Core.Application.Services;
using NetBankingApp.Core.Application.ViewModels.Loan;

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

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AdvanceCredit()
        {
            AdvanceCreditViewModel vm = new();
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;

            vm.CreditCards = await _creditCardService.GetByCustomer(idCustomer);
            vm.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);
            return View(vm);
        }

        [Authorize(Roles = "Customer")]
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
            return RedirectToRoute(new { controller = "Home", action = "CustomerHome" });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddCreditCard(string id)
        {
            return View(new SaveCreditCardViewModel { IdCustomer = id });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCreditCard(SaveCreditCardViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _creditCardService.CreateAsync(vm);
            return RedirectToRoute(new { controller = "User", action = "UserProducts", Id = vm.IdCustomer });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int Id, string IdCustomer)
        {
            await _creditCardService.DeleteAsync(Id);
            return RedirectToRoute(new { controller = "User", action = "UserProducts", Id = IdCustomer });
        }
    }
}
