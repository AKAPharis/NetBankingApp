using NetBankingApp.Core.Application.ViewModels.Transaction;

namespace NetBankingApp.Core.Application.Dtos.Transaction
{
    public class TransactionResponse
    {
        public string DestinationOwnerFirsName { get; set; }
        public string DestinationOwnerLastName { get; set; }



        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
