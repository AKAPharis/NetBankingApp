using NetBankingApp.Core.Application.ViewModels.Transaction;

namespace NetBankingApp.Core.Application.Dtos.Transaction
{
    public class TransactionResponse
    {
        public string DestinationOwnerFirsName { get; set; }
        public string DestinationOwnerLastName { get; set; }
        public string GuidAccountOrigin { get; set; }
        public string GuidAccountDestination { get; set; }
        public string IdBeneficiary { get; set; }

        public double Amount { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
