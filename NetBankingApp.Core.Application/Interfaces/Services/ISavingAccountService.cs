using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface ISavingAccountService : IGenericService<SaveSavingAccountViewModel,SavingAccountViewModel,SavingAccount>
    {
        Task<int> TodayTotal();
        Task<int> Total();


        Task<List<SavingAccountViewModel>> GetByCustomer(string idCustomer);
        Task<SavingAccountViewModel> GetByGuid(string guid);
        Task Deposit(double amount, string guid);
        Task DepositToMain(double amount, string customerId);
        Task<double> Withdraw(string guid, double amount);
        
    }
}
