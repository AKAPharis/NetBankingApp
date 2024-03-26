using AutoMapper;
using NetBankingApp.Core.Application.Dtos.Payment;
using NetBankingApp.Core.Application.Enums;
using NetBankingApp.Core.Application.Helpers;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.Loan;
using NetBankingApp.Core.Domain.Models;
using System.ComponentModel;

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

        public async Task<PaymentResponse> PayDebt(string loanGuid, double amount, string savingAccountGuid)
        {
            PaymentResponse response = new();
            Loan loan = await _loanRepository.GetByGuid(loanGuid);
            if (loan == null)
            {
                response.Error = $"There is not a loan with the guid {loanGuid}";
                response.HasError = true;
                return response;
            }
            if (loan.Debt - amount < 0)
                amount = loan.Debt;

            double withdrawResult = await _savingAccountService.Withdraw(savingAccountGuid, amount);
            if (withdrawResult == 0)
            {
                response.Error = $"There was an error withdrawing from the saving account";
                response.HasError = true;
                return response;
            }
            loan.Debt -= withdrawResult;
            var result = await _loanRepository.UpdateAsync(loan, loan.Id);
            if (result == null)
            {

                response.Error = $"There was an error updating the debt of the loan";
                response.HasError = true;
                return response;

            }


            return response;
        }


        public override async Task DeleteAsync(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan.Debt == 0)
            {
                await _loanRepository.DeleteAsync(loan);
            }
        }


        public async Task<List<LoanViewModel>> GetByCustomer(string idCustomer)
        {
            return _mapper.Map<List<LoanViewModel>>(await _loanRepository.GetByCustomer(idCustomer));
        }

        public override async Task<SaveLoanViewModel> CreateAsync(SaveLoanViewModel viewModel)
        {
            Loan account;
            do
            {
                viewModel.Guid = GuidHelper.Guid((int)Products.Loan);
                account = await _loanRepository.GetByIdAsync(int.Parse(viewModel.Guid));

            } while (account != null);
            var result = await base.CreateAsync(viewModel);
            if(result != null)
            {
                await _savingAccountService.DepositToMain(result.LoanAmount,result.IdCustomer);
            }
            return result;
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
