﻿using NetBankingApp.Core.Application.ViewModels.CreditCard;
using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface ICreditCardService : IGenericService<SaveCreditCardViewModel,CreditCardViewModel,CreditCard>
    {
        Task<int> TodayTotal();
        Task<int> Total();

        Task AdvanceCredit(string creditCardGuid, double amount, string savingAccountGuid);
        Task PayDebt(string creditCardGuid, double amount, string savingAccountGuid);
    }
}
