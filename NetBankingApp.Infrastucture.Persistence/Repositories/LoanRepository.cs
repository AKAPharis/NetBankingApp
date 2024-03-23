using Microsoft.EntityFrameworkCore;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Domain.Models;
using NetBankingApp.Infrastucture.Persistence.Contexts;

namespace NetBankingApp.Infrastucture.Persistence.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(ApplicationContext context) : base(context)
        {
        }
        public Task<int> DailyTotal()
        {
            return _dbSet.Where(x => x.Created.Day == DateTime.Now.Day).CountAsync();
        }

        public async Task<Loan> GetByGuid(string guid)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Guid == guid);
        }

        public async Task<List<Loan>> GetByCustomer(string idCustomer)
        {
            return await _dbSet.Where(x => x.IdCustomer == idCustomer).ToListAsync();
        }
        public Task<int> Total()
        {
            return _dbSet.CountAsync();
        }
    }
}
