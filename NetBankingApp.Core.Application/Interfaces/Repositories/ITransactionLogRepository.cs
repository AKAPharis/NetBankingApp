using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Interfaces.Repositories
{
    public interface ITransactionLogRepository : IGenericRepository<TransactionLog>
    {
        Task<int> Total();
        Task<int> DailyTotal();
    }
}
