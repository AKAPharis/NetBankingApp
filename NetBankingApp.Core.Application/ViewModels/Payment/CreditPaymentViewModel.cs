using NetBankingApp.Core.Application.ViewModels.CreditCard;
using NetBankingApp.Core.Application.ViewModels.Loan;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Core.Application.ViewModels.Payment
{
    public class CreditPaymentViewModel
    {
        public string CreditCardGuid {  get; set; } 
        public double Amount { get; set; }
        public string SavingAccountGuid { get; set; }

        public List<CreditCardViewModel>? CreditCards { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}
