using NetBankingApp.Core.Application.ViewModels.Loan;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;

namespace NetBankingApp.Core.Application.ViewModels.Payment
{
    public class LoanPaymentViewModel
    {
        public string SavingAccountGuid {  get; set; }
        public string LoanGuid { get; set; }
        public double Amount { get; set; }

        public List<LoanViewModel>? Loans { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}
