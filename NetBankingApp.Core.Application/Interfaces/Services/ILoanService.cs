using NetBankingApp.Core.Application.ViewModels.Loan;
using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface ILoanService : IGenericService<SaveLoanViewModel,LoanViewModel,Loan>
    {
        Task<int> TodayTotal();
        Task<int> Total();

        Task PayDebt(string loanGuid, double amount, string savingAccountGuid);

    }
}
