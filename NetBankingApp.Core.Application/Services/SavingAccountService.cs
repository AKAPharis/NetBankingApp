using AutoMapper;
using NetBankingApp.Core.Application.Enums;
using NetBankingApp.Core.Application.Helpers;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Services
{
    public class SavingAccountService : GenericService<SaveSavingAccountViewModel, SavingAccountViewModel, SavingAccount>, ISavingAccountService
    {
        ISavingAccountRepository _savingAccountRepository;
        public SavingAccountService(ISavingAccountRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _savingAccountRepository = repo;
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
    }
}
