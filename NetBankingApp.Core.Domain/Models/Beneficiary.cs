using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBankingApp.Core.Domain.Models
{
    public class Beneficiary
    {
        public string IdUser { get; set; }
        public string IdBeneficiary { get; set; }
        public string BeneficiaryAccountGuid { get; set; }
        public string NickName { get; set; }
    }
}
