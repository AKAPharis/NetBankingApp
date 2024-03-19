using NetBankingApp.Core.Domain.Common;

namespace NetBankingApp.Core.Domain.Models
{
    public class CreditCard : BaseEntity
    {
        public int Guid { get; set; }
        public double LimitAmount { get; set; }
        public double Debt {  get; set; }
        public string IdCustomer {  get; set; }
    }
}
