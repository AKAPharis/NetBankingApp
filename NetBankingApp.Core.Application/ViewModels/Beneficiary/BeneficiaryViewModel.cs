using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Core.Application.ViewModels.Beneficiary
{
    public class BeneficiaryViewModel
    {
        public string IdUser { get; set; }
        public string IdBeneficiary { get; set; }
        public int BeneficiaryAccountGuid { get; set; }
        public string NickName { get; set; }
        public string BeneficiaryFirstName { get; set; }
        public string BeneficiaryLastName { get; set; }
    }
}
