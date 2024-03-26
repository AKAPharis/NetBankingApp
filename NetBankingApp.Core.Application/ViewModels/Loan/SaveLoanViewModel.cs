namespace NetBankingApp.Core.Application.ViewModels.Loan
{
    public class SaveLoanViewModel
    {
        //public int? Id { get; set; }
        public string? Guid { get; set; }
        public double LoanAmount { get; set; }

        public double Debt {  get; set; }
        public string IdCustomer { get; set; }
    }
}
