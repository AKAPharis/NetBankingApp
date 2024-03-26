namespace NetBankingApp.Core.Application.ViewModels.CreditCard
{
    public class SaveCreditCardViewModel
    {
        public string? Guid { get; set; }
        public double LimitAmount { get; set; }
        public double Debt {  get; set; }


        public string IdCustomer { get; set; }
    }
}
