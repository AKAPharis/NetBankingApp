namespace NetBankingApp.Core.Application.ViewModels.Beneficiary
{
    public class SaveBeneficiaryViewModel
    {
        public string? IdUser { get; set; }
        public string? IdBeneficiary { get; set; }
        public string BeneficiaryAccountGuid { get; set; }
        public string NickName { get; set; }

        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}
