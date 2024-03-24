using NetBankingApp.Core.Application.Dtos.Payment;
using NetBankingApp.Core.Application.ViewModels.Payment;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponse> CreditPayment(CreditPaymentViewModel vm);
        Task<PaymentResponse> LoanPayment(LoanPaymentViewModel vm);
    }
}