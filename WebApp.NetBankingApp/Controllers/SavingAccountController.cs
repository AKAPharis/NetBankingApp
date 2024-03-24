using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.NetBankingApp.Controllers
{
    public class SavingAccountController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
