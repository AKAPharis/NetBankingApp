using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Domain.Settings;
using NetBankingApp.Infrastucture.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Infrastucture.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
