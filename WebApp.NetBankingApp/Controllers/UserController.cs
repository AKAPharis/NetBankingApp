using Microsoft.AspNetCore.Mvc;

namespace WebApp.NetBankingApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
