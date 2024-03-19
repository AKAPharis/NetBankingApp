using Microsoft.Extensions.DependencyInjection;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.Services;
using System.Reflection;

namespace NetBankingApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {

            #region Services
            //services.AddTransient<IUserService, UserService>();
            //services.AddTransient<ISavingAccountService, SavingAccountService>();


            #endregion
            #region Mapings
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            #endregion
        }
    }
}
