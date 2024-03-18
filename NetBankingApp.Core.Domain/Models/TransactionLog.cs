using NetBankingApp.Core.Domain.Common;

namespace NetBankingApp.Core.Domain.Models
{
    public class TransactionLog : BaseEntity
    {
        public int GuidAccountOrigin {  get; set; }
        public int GuidAccountDestination { get; set; }
        public double Amount { get; set; }
    }
}
