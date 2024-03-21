using NetBankingApp.Core.Application.Dtos.Logs;
using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface IPaymentLogService
    {
        Task<int> Total();
        Task<int> DailyTotal();
        Task<CreatePaymentLogDTO> AddLog(CreatePaymentLogDTO logDTO);
    }
}
