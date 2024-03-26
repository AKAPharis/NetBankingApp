using NetBankingApp.Core.Application.ViewModels.CreditCard;
using NetBankingApp.Core.Application.ViewModels.Loan;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Core.Application.ViewModels.Payment
{
    public class CreditPaymentViewModel
    {
        [Required]
        public string CreditCardGuid {  get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "The amount entered is not even greater than 0.")]
        public double Amount { get; set; }
        [Required]
        public string SavingAccountGuid { get; set; }

        public List<CreditCardViewModel>? CreditCards { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}
