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
            services.AddTransient<ICreditCardService, CreditCardService>();
            services.AddTransient<ISavingAccountService, SavingAccountService>();
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<IPaymentLogService, PaymentLogService>();
            services.AddTransient<ITransactionLogService, TransactionLogService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILogService, LogService>();



            #endregion
            #region Mapings
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            #endregion
        }
    }
}
