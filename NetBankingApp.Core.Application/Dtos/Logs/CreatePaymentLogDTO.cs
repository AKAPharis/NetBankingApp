namespace NetBankingApp.Core.Application.Dtos.Logs
{
    public class CreatePaymentLogDTO
    {
        public int GuidAccountOrigin { get; set; }
        public int GuidProductDestination { get; set; }
        public double Amount { get; set; }
    }
}
