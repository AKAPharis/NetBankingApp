using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.Home;
using NetBankingApp.Core.Application.Helpers;

namespace WebApp.NetBankingApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly ILogService _logService;
        private readonly ICreditCardService _creditCardService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly ILoanService _loanService;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger, ILogService logService, ICreditCardService creditCardService, ISavingAccountService savingAccountService, ILoanService loanService, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _logService = logService;
            _creditCardService = creditCardService;
            _savingAccountService = savingAccountService;
            _loanService = loanService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        { 
            return View(await _logService.GetLogs());
        }

        public async Task<IActionResult> CustomerHome()
        {

            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;

            CustomerHomeViewModel home = new();
            home.Loans = await _loanService.GetByCustomer(idCustomer);
            home.CreditCards = await _creditCardService.GetByCustomer(idCustomer);
            home.SavingAccounts = await _savingAccountService.GetByCustomer(idCustomer);

            return View(home);
        }
    }
}
