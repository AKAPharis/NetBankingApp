namespace NetBankingApp.Core.Application.ViewModels.Transaction
{
    public class TransactionBeneficiaryViewModel
    {
        public string IdUser { get; set; }
        public string GuidAccountOrigin { get; set; }

        public string IdBeneficiary { get; set; }
        public double Amount { get; set; }
    }
}
