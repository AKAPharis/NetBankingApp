namespace NetBankingApp.Core.Application.Dtos.Payment
{
    public class PaymentResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
