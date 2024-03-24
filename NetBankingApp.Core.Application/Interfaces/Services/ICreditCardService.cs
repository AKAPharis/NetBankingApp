using NetBankingApp.Core.Application.Dtos.Payment;
using NetBankingApp.Core.Application.ViewModels.CreditCard;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface ICreditCardService : IGenericService<SaveCreditCardViewModel,CreditCardViewModel,CreditCard>
    {
        Task<int> TodayTotal();
        Task<int> Total();
        Task<List<CreditCardViewModel>> GetByCustomer(string idCustomer);

        Task<PaymentResponse> AdvanceCredit(string creditCardGuid, double amount, string savingAccountGuid);
        Task<PaymentResponse> PayDebt(string creditCardGuid, double amount, string savingAccountGuid);
    }
}
