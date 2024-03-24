namespace NetBankingApp.Core.Application.Dtos.Logs
{
    public class CreatePaymentLogDTO
    {
        public string GuidAccountOrigin { get; set; }
        public string GuidProductDestination { get; set; }
        public double Amount { get; set; }
    }
}
