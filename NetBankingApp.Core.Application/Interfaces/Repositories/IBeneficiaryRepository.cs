using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Interfaces.Repositories
{
    public interface IBeneficiaryRepository : IGenericRepository<Beneficiary>
    {
        Task<Beneficiary> GetBeneficiary(string idUser, string idBeneficiary);
        Task<List<Beneficiary>> GetBeneficiaries(string idUser);
    }
}
