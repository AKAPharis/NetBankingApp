using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.NetBankingApp.Controllers
{
    [Authorize]
    public class CreditController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
