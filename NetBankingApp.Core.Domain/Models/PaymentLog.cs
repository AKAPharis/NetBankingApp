using NetBankingApp.Core.Domain.Common;

namespace NetBankingApp.Core.Domain.Models
{
    public class PaymentLog : BaseEntity
    {
        public string GuidAccountOrigin { get; set; }
        public string GuidProductDestination { get; set; }
        public double Amount { get; set; }
    }
}
