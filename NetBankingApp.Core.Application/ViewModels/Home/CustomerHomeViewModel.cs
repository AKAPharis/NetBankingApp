using NetBankingApp.Core.Application.ViewModels.CreditCard;
using NetBankingApp.Core.Application.ViewModels.Loan;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;

namespace NetBankingApp.Core.Application.ViewModels.Home
{
    public class CustomerHomeViewModel
    {
        public string? IdCustomer { get; set; }
        public string ProductType { get; set; }
        public double Amount { get; set; }
        public List<CreditCardViewModel>? CreditCards { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
        public List<LoanViewModel>? Loans { get; set; }
    }
}
