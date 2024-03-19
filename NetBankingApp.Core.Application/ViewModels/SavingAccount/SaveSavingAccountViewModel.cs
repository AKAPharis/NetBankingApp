using System.ComponentModel.DataAnnotations;

namespace NetBankingApp.Core.Application.ViewModels.SavingAccount
{
    public class SaveSavingAccountViewModel
    {
        [Required]
        public double Savings { get; set; }
        [Required]
        public string IdCustomer { get; set; }
        [Required]
        public bool IsMain { get; set; }

        public string? Guid { get; set; }
    }
}
