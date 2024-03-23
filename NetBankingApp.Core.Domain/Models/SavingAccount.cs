using NetBankingApp.Core.Domain.Common;

namespace NetBankingApp.Core.Domain.Models
{
    public class SavingAccount : BaseEntity
    {
        public string Guid { get; set; }
        public double Savings { get; set; }
        public string IdCustomer {  get; set; }
        public bool IsMain { get; set; }

    }
}
