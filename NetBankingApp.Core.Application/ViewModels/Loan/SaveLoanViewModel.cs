namespace NetBankingApp.Core.Application.ViewModels.Loan
{
    public class SaveLoanViewModel
    {
        //public int? Id { get; set; }
        public string? Guid { get; set; }
        public double LoanAmount { get; set; }

        private double _debt;
        public double Debt
        {
            get { return _debt; }

            set
            {
                _debt = LoanAmount;
            }
        }

        public string IdCustomer { get; set; }
    }
}
