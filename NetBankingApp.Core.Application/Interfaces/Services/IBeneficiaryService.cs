using NetBankingApp.Core.Application.ViewModels.Beneficiary;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface IBeneficiaryService
    {
        Task Delete(SaveBeneficiaryViewModel vm);
        Task<List<BeneficiaryViewModel>> GetBenficiaries(string idUser);
        Task<SaveBeneficiaryViewModel> CreateBeneficiary(SaveBeneficiaryViewModel vm);
        Task<BeneficiaryViewModel> GetBeneficiary(string idUser, string idBeneficiary);

    }
}
