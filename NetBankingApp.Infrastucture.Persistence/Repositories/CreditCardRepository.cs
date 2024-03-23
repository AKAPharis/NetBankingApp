using Microsoft.EntityFrameworkCore;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Domain.Models;
using NetBankingApp.Infrastucture.Persistence.Contexts;

namespace NetBankingApp.Infrastucture.Persistence.Repositories
{
    public class CreditCardRepository : GenericRepository<CreditCard>, ICreditCardRepository
    {
        public CreditCardRepository(ApplicationContext context) : base(context)
        {
        }
        public Task<int> DailyTotal()
        {
            return _dbSet.Where(x => x.Created.Day == DateTime.Now.Day).CountAsync();
        }

        public async Task<CreditCard> GetByGuid(string guid)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Guid == guid);
        }

        public Task<int> Total()
        {
            return _dbSet.CountAsync();
        }
    }
}
