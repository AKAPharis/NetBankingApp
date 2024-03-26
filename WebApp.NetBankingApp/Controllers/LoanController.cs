using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.Services;
using NetBankingApp.Core.Application.ViewModels.Loan;

namespace WebApp.NetBankingApp.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public IActionResult AddLoan(string id)
        {
            return View(new SaveLoanViewModel { IdCustomer = id});
        }

        [HttpPost]
        public async Task<IActionResult> AddLoan(SaveLoanViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            await _loanService.CreateAsync(vm);
            return RedirectToRoute(new { controller = "User", action = "UserProducts", Id = vm.IdCustomer });
        }

        public async Task<IActionResult> Delete(int Id, string IdCustomer)
        {
            await _loanService.DeleteAsync(Id);
            return RedirectToRoute(new { controller = "User", action = "UserProducts", Id = IdCustomer });
        }

    }
}
