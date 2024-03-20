namespace NetBankingApp.Core.Application.ViewModels.CreditCard
{
    public class CreditCardViewModel
    {
        public int Id { get; set; }
        public DateTime Created {  get; set; }

        public int Guid { get; set; }
        public double LimitAmount { get; set; }
        public double Debt { get; set; }
        public string IdCustomer { get; set; }
    }
}
