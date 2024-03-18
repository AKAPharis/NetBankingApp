using NetBankingApp.Core.Application.ViewModels.Account;

namespace NetBankingApp.Core.Application.ViewModels.SavingAccount
{
    public class SavingAccountViewModel
    {
        public int Id { get; set; }
        public DateTime Created {  get; set; }
        public int Guid { get; set; }
        public double Savings { get; set; }
        public string IdCustomer { get; set; }
        public UserViewModel? Customer { get; set; }
        public bool IsMain { get; set; }
    }
}
