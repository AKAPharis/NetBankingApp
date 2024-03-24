using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBankingApp.Core.Application.Interfaces.Services;
using System.Diagnostics;
using WebApp.NetBankingApp.Models;

namespace WebApp.NetBankingApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly ILogService _logService;

        public HomeController(ILogger<HomeController> logger, ILogService logService)
        {
            _logService = logService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        { 
            return View(await _logService.GetLogs());
        }

        public IActionResult CustomerHome()
        {
            return View();
        }
    }
}
