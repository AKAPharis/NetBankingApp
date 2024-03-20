using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Interfaces.Repositories
{
    public interface ICreditCardRepository : IGenericRepository<CreditCard>
    {
        Task<int> Total();
        Task<int> DailyTotal();
        Task<CreditCard> GetByGuid(int guid);
    }
}
