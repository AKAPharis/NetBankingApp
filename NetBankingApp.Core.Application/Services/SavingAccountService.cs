using AutoMapper;
using NetBankingApp.Core.Application.Enums;
using NetBankingApp.Core.Application.Helpers;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Application.Interfaces.Services;
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

        public async Task Deposit(double amount, int guid)
        {
            SavingAccount account = await _savingAccountRepository.GetByGuid(guid);
            account.Savings += amount;
            await _savingAccountRepository.UpdateAsync(account, account.Id);
        }

        public async Task DepositToMain(double amount, string customerId)
        {
            SavingAccount account = await _savingAccountRepository.GetMain(customerId);
            account.Savings += amount;
            await _savingAccountRepository.UpdateAsync(account, account.Id);
        }

        public async Task<SavingAccountViewModel> GetByGuid(int guid)
        {
            return _mapper.Map<SavingAccountViewModel>(await _savingAccountRepository.GetByGuid(guid));
        }

        public async Task<double> Withdraw(int guid, double amount)
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
    }
}
