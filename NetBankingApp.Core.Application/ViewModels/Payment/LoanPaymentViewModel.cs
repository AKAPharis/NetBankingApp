using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Core.Application.ViewModels.Payment
{
    public class LoanPaymentViewModel
    {
        public string SavingAccountGuid {  get; set; }
        public string LoanGuid { get; set; }
        public double Amount { get; set; }
    }
}
