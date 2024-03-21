using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface ISavingAccountService : IGenericService<SaveSavingAccountViewModel,SavingAccountViewModel,SavingAccount>
    {
        Task<int> TodayTotal();
        Task<int> Total();



        Task<SavingAccountViewModel> GetByGuid(int guid);
        Task Deposit(double amount, int guid);
        Task DepositToMain(double amount, string customerId);
        Task<double> Withdraw(int guid, double amount);
        
    }
}
