using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.NetBankingApp.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
