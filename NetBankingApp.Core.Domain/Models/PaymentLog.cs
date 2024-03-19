using NetBankingApp.Core.Domain.Common;

namespace NetBankingApp.Core.Domain.Models
{
    public class PaymentLog : BaseEntity
    {
        public int GuidAccountOrigin { get; set; }
        public int GuidProductDestination { get; set; }
        public double Amount { get; set; }
    }
}
