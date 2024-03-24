using AutoMapper;
using NetBankingApp.Core.Application.Dtos.Payment;
using NetBankingApp.Core.Application.Enums;
using NetBankingApp.Core.Application.Helpers;
using NetBankingApp.Core.Application.Interfaces.Repositories;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.CreditCard;
using NetBankingApp.Core.Application.ViewModels.Loan;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using NetBankingApp.Core.Domain.Models;
using System.ComponentModel.DataAnnotations;

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

        public async Task<List<CreditCardViewModel>> GetByCustomer(string idCustomer)
        {
            return _mapper.Map<List<CreditCardViewModel>>(await _creditCardRepository.GetByCustomer(idCustomer));
        }

        public async Task<PaymentResponse> AdvanceCredit(string creditCardGuid, double amount, string savingAccountGuid)
        {
            PaymentResponse response = new();
            CreditCard creditCard = await _creditCardRepository.GetByGuid(creditCardGuid);
            if (creditCard == null)
            {
                response.Error = $"There is not an credit card with the guid {creditCardGuid}";
                response.HasError = true ;
                return response;
            }
            SavingAccountViewModel savingAccount = await _savingAccountService.GetByGuid(savingAccountGuid);
            if (savingAccount == null)
            {
                response.Error = $"There is not an account with the guid {savingAccount}";
                response.HasError = true;
                return response;
            }
            if (amount <= creditCard.LimitAmount)
            {
                creditCard.Debt += amount + (amount * 6.25/100);
                savingAccount.Savings += amount;

                var savingAccountResult = await _savingAccountService.UpdateAsync(_mapper.Map<SaveSavingAccountViewModel>(savingAccount), savingAccount.Id);
                if (savingAccountResult == null)
                {
                    response.Error = $"There was an error updating the balance of the saving account";
                    response.HasError = true;
                    return response;
                }
                var creditCardResult = await _creditCardRepository.UpdateAsync(creditCard, creditCard.Id);
                if (creditCardResult == null)
                {
                    response.Error = $"There was an error updating the debt of the credit card";
                    response.HasError = true;
                    return response;
                }
            }
            return response;

        }

        public async Task<PaymentResponse> PayDebt(string creditCardGuid, double amount, string savingAccountGuid)
        {
            PaymentResponse response = new();
            CreditCard creditCard = await _creditCardRepository.GetByGuid(creditCardGuid);
            if(creditCard == null)
            {
                response.Error = $"There is not an credit card with the guid {creditCardGuid}";
                response.HasError = true;
                return response;
            }
            if (creditCard.Debt - amount < 0)
                amount = creditCard.Debt;
            double withdrawResult = await _savingAccountService.Withdraw(savingAccountGuid, amount);
            if (withdrawResult == 0)
            {
                response.Error = $"There was an error withdrawing from the saving account";
                response.HasError = true;
                return response;
            }
            creditCard.Debt -= withdrawResult;
            var result = await _creditCardRepository.UpdateAsync(creditCard, creditCard.Id);
            if(result == null)
            {
                response.Error = $"There was an error updating the debt of the credit card";
                response.HasError = true;
                return response;
            }
            return response;
        }


        public override async Task<SaveCreditCardViewModel> CreateAsync(SaveCreditCardViewModel viewModel)
        {
            CreditCard account;
            do
            {
                viewModel.Guid = GuidHelper.Guid((int)Products.CreditCard);
                account = await _creditCardRepository.GetByIdAsync(int.Parse(viewModel.Guid));

            } while (account != null);

            return await base.CreateAsync(viewModel);
        }

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
