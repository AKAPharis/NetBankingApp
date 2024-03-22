using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Interfaces.Repositories
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<int> Total();
        Task<int> DailyTotal();
        Task<Loan> GetByGuid(int guid);
    }
}
