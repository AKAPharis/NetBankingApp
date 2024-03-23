using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.Home;

namespace NetBankingApp.Core.Application.Services
{
    public class LogService : ILogService
    {
        private readonly IPaymentLogService _paymentLogService;
        private readonly ITransactionLogService _transactionLogService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly ILoanService _loanService;
        private readonly IUserService _userService;
        private readonly ICreditCardService _creditCardService;

        public LogService(IPaymentLogService logService,
            ITransactionLogService transactionLogService,
            ISavingAccountService savingAccountService,
            ILoanService loanService,
            IUserService userService,
            ICreditCardService creditCardService)
        {
            _paymentLogService = logService;
            _transactionLogService = transactionLogService;
            _savingAccountService = savingAccountService;
            _loanService = loanService;
            _userService = userService;
            _creditCardService = creditCardService;
        }

        public async Task<DashBoardViewModel> GetLogs()
        {

            //Yo (oscar) he programado esto y no me gusta ni un chin
            //POR QUE TANTAS LLAMADAS POR DIOS

            DashBoardViewModel dashBoardViewModel = new();

            dashBoardViewModel.TotalProducts = 0;
            dashBoardViewModel.TotalProducts += await _loanService.Total();
            dashBoardViewModel.TotalProducts += await _creditCardService.Total();
            dashBoardViewModel.TotalProducts += await _savingAccountService.Total();
            dashBoardViewModel.TotalTransactions = await _transactionLogService.Total();
            dashBoardViewModel.TodayTransactions = await _transactionLogService.DailyTotal();
            dashBoardViewModel.TotalPayments = await _paymentLogService.Total();
            dashBoardViewModel.TodayPayments = await _paymentLogService.DailyTotal();
            dashBoardViewModel.ActiveUsers = await _userService.GetActiveUsers();
            dashBoardViewModel.InactiveUsers = await _userService.GetInactiveUsers();

            return dashBoardViewModel;
        }
    }
}
