using AutoMapper;
using NetBankingApp.Core.Application.Dtos.Transaction;
using NetBankingApp.Core.Application.Enums;
using NetBankingApp.Core.Application.Helpers;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.CreditCard;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using NetBankingApp.Core.Domain.Models;
using System;

namespace NetBankingApp.Core.Application.Services
{
    public class SavingAccountService : GenericService<SaveSavingAccountViewModel, SavingAccountViewModel, SavingAccount>, ISavingAccountService
    {
        ISavingAccountRepository _savingAccountRepository;
        public SavingAccountService(ISavingAccountRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _savingAccountRepository = repo;
        }


        public async Task<int> TodayTotal()
        {
            return await _savingAccountRepository.DailyTotal();
        }
        public async Task<int> Total()
        {
            return await _savingAccountRepository.Total();
        }

        public override async Task<SaveSavingAccountViewModel> CreateAsync(SaveSavingAccountViewModel viewModel)
        {
            SavingAccount account;
            do
            {
                viewModel.Guid = GuidHelper.Guid((int)Products.SavingAccount);
                account = await _savingAccountRepository.GetByIdAsync(int.Parse(viewModel.Guid));

            } while (account != null);
            return await base.CreateAsync(viewModel);
        }

        public async Task<TransactionResponse> Deposit(double amount, string guid)
        {
            TransactionResponse response = new();
            SavingAccount account = await _savingAccountRepository.GetByGuid(guid);
            if (account == null)
            {
                response.Error = "The account to deposit was not found";
                response.HasError = true;
                return response;
            }
            account.Savings += amount;
            var result = await _savingAccountRepository.UpdateAsync(account, account.Id);
            if (result == null)
            {
                response.Error = "There was an error depositing to the account";
                response.HasError = true;
                return response;
            }
            return response;
        }

        public async Task<TransactionResponse> DepositToMain(double amount, string customerId)
        {
            TransactionResponse response = new();
            SavingAccount account = await _savingAccountRepository.GetMain(customerId);
            if(account == null)
            {
                response.Error = "The account to deposit was not found";
                response.HasError = true;
                return response;
            }
            account.Savings += amount;
            var result = await _savingAccountRepository.UpdateAsync(account, account.Id);
            if(result == null)
            {
                response.Error = "There was an error depositing to the account";
                response.HasError = true;
                return response;
            }
            return response;
        }

        public async Task<List<SavingAccountViewModel>> GetByCustomer(string idCustomer)
        {
            return _mapper.Map<List<SavingAccountViewModel>>(await _savingAccountRepository.GetByCustomer(idCustomer));
        }

        public async Task<SavingAccountViewModel> GetByGuid(string guid)
        {
            return _mapper.Map<SavingAccountViewModel>(await _savingAccountRepository.GetByGuid(guid));
        }

        public async Task<double> Withdraw(string guid, double amount)
        {
            SavingAccount account = await _savingAccountRepository.GetByGuid(guid);
            if(account.Savings - amount > 0)
            {
                account.Savings -= amount;
                await _savingAccountRepository.UpdateAsync(account, account.Id);
                return amount;
            }
            return 0;
        }
        public override async Task DeleteAsync(int id)
        {
            var account = await _savingAccountRepository.GetByIdAsync(id);
            if (!account.IsMain)
            {
                await DepositToMain(account.Savings,account.IdCustomer);
                await _savingAccountRepository.DeleteAsync(account);
            }
        }
    }
}
