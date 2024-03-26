using System.ComponentModel.DataAnnotations;

namespace NetBankingApp.Core.Application.ViewModels.CreditCard
{
    public class SaveCreditCardViewModel
    {
        public string? Guid { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "The amount entered is not even greater than 0.")]
        public double LimitAmount { get; set; }
        public double Debt {  get; set; }


        public string IdCustomer { get; set; }
    }
}
