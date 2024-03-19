using Microsoft.EntityFrameworkCore;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Domain.Models;
using NetBankingApp.Infrastucture.Persistence.Contexts;

namespace NetBankingApp.Infrastucture.Persistence.Repositories
{
    public class PaymentLogRepository : GenericRepository<PaymentLog>, IPaymentLogRepository
    {
        public PaymentLogRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<int> DailyTotal()
        {
            return await _dbSet
                .Where(x => x.Created.Day == DateTime.Now.Day)
                .CountAsync();
        }
        public async Task<int> Total()
        {
            return await _dbSet
                .CountAsync();
        }

    }
}
