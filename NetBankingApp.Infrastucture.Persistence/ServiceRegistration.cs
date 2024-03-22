using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Infrastucture.Persistence.Contexts;
using NetBankingApp.Infrastucture.Persistence.Repositories;


namespace NetBankingApp.Infrastucture.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
                });
            }
            #endregion




            #region Repositories

            services.AddTransient<ICreditCardRepository, CreditCardRepository>();
            services.AddTransient<ILoanRepository, LoanRepository>();
            services.AddTransient<IPaymentLogRepository, PaymentLogRepository>();
            services.AddTransient<ISavingAccountRepository, SavingAccountRepository>();
            services.AddTransient<ITransactionLogRepository, TransactionLogRepository>();
            services.AddTransient<IBeneficiaryRepository, BeneficiaryRepository>();



            #endregion
        }
    }
}
