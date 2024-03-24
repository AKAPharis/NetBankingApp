namespace NetBankingApp.Core.Application.Dtos.Transaction
{
    public class TransactionResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
