using NetBankingApp.Core.Application.Dtos.Transaction;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using NetBankingApp.Core.Domain.Models;
using System.Transactions;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface ISavingAccountService : IGenericService<SaveSavingAccountViewModel,SavingAccountViewModel,SavingAccount>
    {
        Task<int> TodayTotal();
        Task<int> Total();


        Task<List<SavingAccountViewModel>> GetByCustomer(string idCustomer);
        Task<SavingAccountViewModel> GetByGuid(string guid);
        Task<TransactionResponse> Deposit(double amount, string guid);
        Task<TransactionResponse> DepositToMain(double amount, string customerId);
        Task<double> Withdraw(string guid, double amount);
        
    }
}
