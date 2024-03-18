using NetBankingApp.Core.Domain.Common;

namespace NetBankingApp.Core.Domain.Models
{
    public class Loan : BaseEntity
    {
        public int Guid { get; set; }
        public double LoanAmount { get; set; }
        public double Debt {  get; set; }
        public string IdCustomer {  get; set; }
    }
}
