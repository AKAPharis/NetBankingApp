using Microsoft.EntityFrameworkCore;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Domain.Models;
using NetBankingApp.Infrastucture.Persistence.Contexts;

namespace NetBankingApp.Infrastucture.Persistence.Repositories
{
    public class BeneficiaryRepository : GenericRepository<Beneficiary>, IBeneficiaryRepository
    {
        public BeneficiaryRepository(ApplicationContext context) : base(context)
        {
        }

        public Task<List<Beneficiary>> GetBeneficiaries(string idUser)
        {
            return _dbSet.Where(x => x.IdUser == idUser).ToListAsync();
        }

        public Task<Beneficiary> GetBeneficiary(string idUser, string idBeneficiary)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.IdBeneficiary == idBeneficiary && x.IdUser == idUser);
        }
    }
}
