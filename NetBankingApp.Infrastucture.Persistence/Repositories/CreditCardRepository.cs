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
    }
}
