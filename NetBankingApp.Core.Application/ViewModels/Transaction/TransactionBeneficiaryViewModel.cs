using NetBankingApp.Core.Application.ViewModels.Beneficiary;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using System.ComponentModel.DataAnnotations;

namespace NetBankingApp.Core.Application.ViewModels.Transaction
{
    public class TransactionBeneficiaryViewModel
    {
        public string? IdUser { get; set; }
        public string GuidAccountOrigin { get; set; }

        public string IdBeneficiary { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "The amount entered is not even greater than 0.")]
        public double Amount { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
        public List<BeneficiaryViewModel>? Beneficiaries { get; set; }

        public string? DestinationOwnerFirsName { get; set; }
        public string? DestinationOwnerLastName { get; set; }


        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}
