using NetBankingApp.Core.Application.ViewModels.Loan;
using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface ILoanService : IGenericService<SaveLoanViewModel,LoanViewModel,Loan>
    {
        Task PayDebt(int loanGuid, double amount, int savingAccountGuid);

    }
}
