using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Core.Application.ViewModels.Transaction
{
    public class TransactionExpressViewModel
    {
        public string GuidAccountOrigin { get; set; }
        public string GuidAccountDestination { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "The amount entered is not even greater than 0.")]
        public double Amount { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
        public string? DestinationOwnerFirsName { get; set; }
        public string? DestinationOwnerLastName { get; set; }

        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}
