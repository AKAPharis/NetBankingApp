using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Interfaces.Repositories
{
    public interface ISavingAccountRepository : IGenericRepository<SavingAccount>
    {
        Task<int> Total();
        Task<int> DailyTotal();
    }
}
