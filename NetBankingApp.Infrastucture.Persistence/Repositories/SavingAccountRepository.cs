using Microsoft.EntityFrameworkCore;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Domain.Models;
using NetBankingApp.Infrastucture.Persistence.Contexts;

namespace NetBankingApp.Infrastucture.Persistence.Repositories
{
    public class SavingAccountRepository : GenericRepository<SavingAccount>, ISavingAccountRepository
    {
        public SavingAccountRepository(ApplicationContext context) : base(context)
        {
        }

        public Task<int> DailyTotal()
        {
            return _dbSet.Where(x => x.Created.Day == DateTime.Now.Day).CountAsync();
        }

        public Task<int> Total()
        {
            return _dbSet.CountAsync();
        }
    }
}
