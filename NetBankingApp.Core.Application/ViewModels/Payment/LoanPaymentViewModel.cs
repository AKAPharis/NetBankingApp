using NetBankingApp.Core.Application.ViewModels.Loan;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using System.ComponentModel.DataAnnotations;

namespace NetBankingApp.Core.Application.ViewModels.Payment
{
    public class LoanPaymentViewModel
    {
        public string SavingAccountGuid {  get; set; }
        public string LoanGuid { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "The amount entered is not even greater than 0.")]
        public double Amount { get; set; }

        public List<LoanViewModel>? Loans { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}
