using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Core.Application.ViewModels.CreditCard
{
    public class AdvanceCreditViewModel
    {
        [Required]
        public string CreditCardGuid { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string SavingAccountGuid { get; set; }

        public string? Erorr {  get; set; }
        public bool HasError { get; set; }
        public List<SavingAccountViewModel>? SavingAccounts { get; set; }
        public List<CreditCardViewModel>? CreditCards { get; set; }
    }
}
