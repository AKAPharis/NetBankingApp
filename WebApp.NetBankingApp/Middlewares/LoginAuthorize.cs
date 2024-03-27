using Microsoft.AspNetCore.Mvc.Filters;
using WebApp.NetBankingApp.Controllers;
using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.Helpers;

namespace WebApp.NetBankingApp.Middlewares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginAuthorize(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            AuthenticationResponse userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            if (userViewModel != null)
            {
                var controller = (UserController)context.Controller;

                if (userViewModel.Roles.Any(r => r == "Admin"))
                {
                    context.Result = controller.RedirectToAction("index", "home");
                }
                else
                {
                    context.Result = controller.RedirectToAction("customerhome", "home");
                }
            }
            else
            {
                await next();
            }
        }
    }
}
