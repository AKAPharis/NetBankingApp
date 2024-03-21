using NetBankingApp.Core.Application.ViewModels.Home;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface ILogService
    {
        Task<DashBoardViewModel> GetLogs();
    }
}