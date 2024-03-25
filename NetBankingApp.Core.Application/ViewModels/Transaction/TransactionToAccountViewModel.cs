using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Core.Application.ViewModels.Transaction
{
    public class TransactionToAccountViewModel
    {
        public string GuidAccountOrigin { get; set; }
        public string GuidAccountDestination { get; set; }
        public double Amount { get; set; }

        public List<SavingAccountViewModel> SavingAccounts { get; set; }

        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}
