using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.Helpers;
using NetBankingApp.Core.Application.ViewModels.Beneficiary;

namespace WebApp.NetBankingApp.Controllers
{
    [Authorize(Roles = "Customer")]
    public class BeneficiaryController : Controller
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IHttpContextAccessor _contextAccessor;

        public BeneficiaryController(IBeneficiaryService beneficiaryService, IHttpContextAccessor contextAccessor)
        {
            _beneficiaryService = beneficiaryService;
            _contextAccessor = contextAccessor;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _beneficiaryService.GetBenficiaries(_contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id));
        }

        public async Task<IActionResult> Delete(string idBeneficiary)
        {
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;
            return View(await _beneficiaryService.GetBeneficiarySaveViewModel(idCustomer, idBeneficiary));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SaveBeneficiaryViewModel vm)
        {
            await _beneficiaryService.Delete(vm);
            return RedirectToRoute(new { controller = "Beneficiary", action = "Index" });
        }


        public IActionResult Create()
        {
            return View(new SaveBeneficiaryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveBeneficiaryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            string idCustomer = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;
            vm.IdUser = idCustomer;
            var response = await _beneficiaryService.CreateBeneficiary(vm);
            if(response.HasError)
            {
                return View(response);
            }

            return RedirectToRoute(new { controller = "Beneficiary", action = "Index" });
        }
    }
}
