using AutoMapper;
using NetBankingApp.Core.Application.Enums;
using NetBankingApp.Core.Application.Helpers;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.Loan;
using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Services
{
    public class LoanService : GenericService<SaveLoanViewModel, LoanViewModel, Loan>, ILoanService
    {
        ISavingAccountService _savingAccountService;
        ILoanRepository _loanRepository;
        public LoanService(ILoanRepository repo, IMapper mapper, ISavingAccountService savingAccountService) : base(repo, mapper)
        {
            _savingAccountService = savingAccountService;
            _loanRepository = repo;
        }

        #region Casi Terminados
        public async Task PayDebt(string loanGuid, double amount, string savingAccountGuid)
        {
            Loan loan = await _loanRepository.GetByGuid(loanGuid);
            if (loan.Debt - amount < 0)
                amount = loan.Debt;

            double withdrawResult = await _savingAccountService.Withdraw(savingAccountGuid, amount);
            loan.Debt -= withdrawResult;
            await _loanRepository.UpdateAsync(loan, loan.Id);
        }

        #endregion

        public override async Task<SaveLoanViewModel> CreateAsync(SaveLoanViewModel viewModel)
        {
            Loan account;
            do
            {
                viewModel.Guid = GuidHelper.Guid((int)Products.Loan);
                account = await _loanRepository.GetByIdAsync(int.Parse(viewModel.Guid));

            } while (account != null);

            return await base.CreateAsync(viewModel);
        }

        public async Task<int> TodayTotal()
        {
            return await _loanRepository.DailyTotal();
        }

        public async Task<int> Total()
        {
            return await _loanRepository.Total();
        }
    }
}
