using AutoMapper;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.CreditCard;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Services
{
    public class CreditCardService : GenericService<SaveCreditCardViewModel, CreditCardViewModel, CreditCard>, ICreditCardService
    {
        ICreditCardRepository _creditCardRepository;
        ISavingAccountService _savingAccountService;

        public CreditCardService(ICreditCardRepository repo, IMapper mapper, ISavingAccountService savingAccountService) : base(repo, mapper)
        {
            _creditCardRepository = repo;
            _savingAccountService = savingAccountService;
        }

        public override async Task DeleteAsync(int id)
        {
            var creditCard = await _creditCardRepository.GetByIdAsync(id);
            if(creditCard.Debt == 0)
            {
                await _creditCardRepository.DeleteAsync(creditCard);
            }
        }

        #region Casi Terminados
        public async Task AdvanceCredit(string creditCardGuid, double amount, string savingAccountGuid)
        {
            CreditCard creditCard = await _creditCardRepository.GetByGuid(creditCardGuid);
            SavingAccountViewModel savingAccount = await _savingAccountService.GetByGuid(savingAccountGuid);

            if (creditCard.Debt <= creditCard.LimitAmount)
            {
                creditCard.Debt += amount;
                savingAccount.Savings += amount;

                await _savingAccountService.UpdateAsync(_mapper.Map<SaveSavingAccountViewModel>(savingAccount), savingAccount.Id);
                await _creditCardRepository.UpdateAsync(creditCard, creditCard.Id);
            }


        }

        public async Task PayDebt(string creditCardGuid, double amount, string savingAccountGuid)
        {
            CreditCard creditCard = await _creditCardRepository.GetByGuid(creditCardGuid);
            if (creditCard.Debt - amount < 0)
                amount = creditCard.Debt;
            double withdrawResult = await _savingAccountService.Withdraw(savingAccountGuid, amount);
            creditCard.Debt -= withdrawResult;
            await _creditCardRepository.UpdateAsync(creditCard, creditCard.Id);
        }
        #endregion

        public async Task<int> TodayTotal()
        {
            return await _creditCardRepository.DailyTotal();
        }

        public async Task<int> Total()
        {
            return await _creditCardRepository.Total();
        }
    }
}
