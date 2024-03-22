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
        #region Casi Terminados
        public async Task AdvanceCredit(int creditCardGuid, double amount, int savingAccountGuid)
        {
            CreditCard creditCard = await _creditCardRepository.GetByGuid(creditCardGuid);
            SavingAccountViewModel savingAccount = await _savingAccountService.GetByIdAsync(savingAccountGuid);

            if (creditCard.Debt <= creditCard.LimitAmount)
            {
                creditCard.Debt += amount;
                savingAccount.Savings += amount;

                await _savingAccountService.UpdateAsync(_mapper.Map<SaveSavingAccountViewModel>(savingAccount), savingAccount.Id);
                await _creditCardRepository.UpdateAsync(creditCard, creditCard.Id);
            }


        }

        public async Task PayDebt(int creditCardGuid, double amount, int savingAccountGuid)
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
