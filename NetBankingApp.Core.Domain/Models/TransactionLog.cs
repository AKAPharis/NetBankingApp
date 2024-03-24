using NetBankingApp.Core.Domain.Common;

namespace NetBankingApp.Core.Domain.Models
{
    public class TransactionLog : BaseEntity
    {
        public string GuidAccountOrigin {  get; set; }
        public string GuidAccountDestination { get; set; }
        public double Amount { get; set; }
    }
}
