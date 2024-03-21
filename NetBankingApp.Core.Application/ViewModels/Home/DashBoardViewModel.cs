namespace NetBankingApp.Core.Application.ViewModels.Home
{
    public class DashBoardViewModel
    {
        public int TotalTransactions {  get; set; }
        public int TodayTransactions { get; set; }
        public int TotalPayments { get; set; }
        public int TodayPayments { get; set; }
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
        public int TotalProducts { get; set; }

    }
}
