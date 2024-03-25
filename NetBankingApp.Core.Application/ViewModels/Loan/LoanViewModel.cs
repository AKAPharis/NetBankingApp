namespace NetBankingApp.Core.Application.ViewModels.Loan
{
    public class LoanViewModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Guid { get; set; }
        public double LoanAmount { get; set; }
        public double Debt { get; set; }
        public string IdCustomer { get; set; }
    }
}
