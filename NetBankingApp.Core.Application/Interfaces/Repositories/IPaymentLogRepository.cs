using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Interfaces.Repositories
{
    public interface IPaymentLogRepository : IGenericRepository<PaymentLog>
    {
        Task<int> Total();
        Task<int> DailyTotal();
    }
}
