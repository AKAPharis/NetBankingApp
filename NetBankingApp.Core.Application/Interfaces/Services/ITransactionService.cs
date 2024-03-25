using NetBankingApp.Core.Application.Dtos.Transaction;
using NetBankingApp.Core.Application.ViewModels.Transaction;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<TransactionResponse> TransactionBeneficiary(TransactionBeneficiaryViewModel vm);
        Task<TransactionResponse> TransactionExpress(TransactionExpressViewModel vm);
        Task<TransactionResponse> TransactionToAccount(TransactionToAccountViewModel vm);
    }
}