using NetBankingApp.Core.Application.Dtos.Email;
using NetBankingApp.Core.Domain.Settings;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        public MailSettings MailSettings { get; }
        Task SendAsync(EmailRequest request);
    }
}
