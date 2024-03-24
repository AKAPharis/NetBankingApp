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
    }
}
