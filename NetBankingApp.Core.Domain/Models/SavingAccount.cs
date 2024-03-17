using NetBankingApp.Core.Domain.Common;

namespace NetBankingApp.Core.Domain.Models
{
    public class SavingAccount : BaseEntity
    {
        public int Guid { get; set; }
        public double Savings { get; set; }
        public int IdCustomer {  get; set; }
        public bool IsMain { get; set; }

    }
}
