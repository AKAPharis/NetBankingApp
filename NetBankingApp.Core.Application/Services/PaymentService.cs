using NetBankingApp.Core.Application.Dtos.Logs;
using NetBankingApp.Core.Application.Dtos.Payment;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.CreditCard;
using NetBankingApp.Core.Application.ViewModels.Payment;

namespace NetBankingApp.Core.Application.Services
{
    public class PaymentService : IPaymentService
    {
        IPaymentLogService _paymentLogService;
        ISavingAccountService _savingAccountService;
        ICreditCardService _creditCardService;
        ILoanService _loanService;

        public PaymentService(IPaymentLogService paymentLogService,
            ISavingAccountService savingAccountService,
            ICreditCardService creditCardService,
            ILoanService loanService)
        {
            _paymentLogService = paymentLogService;
            _savingAccountService = savingAccountService;
            _creditCardService = creditCardService;
            _loanService = loanService;
        }

        public async Task<PaymentResponse> LoanPayment(LoanPaymentViewModel vm)
        {
            PaymentResponse response = new();

            var result = await _loanService.PayDebt(vm.LoanGuid, vm.Amount, vm.SavingAccountGuid);
            if (result.HasError)
            {
                return result;
            }
            await _paymentLogService.AddLog(new CreatePaymentLogDTO
            {
                GuidAccountOrigin = vm.SavingAccountGuid,
                GuidProductDestination = vm.LoanGuid,
                Amount = vm.Amount
            });

            return response;
        }

        public async Task<PaymentResponse> CreditPayment(CreditPaymentViewModel vm)
        {
            PaymentResponse response = new();

            var result = await _creditCardService.PayDebt(vm.CreditCardGuid, vm.Amount, vm.SavingAccountGuid);
            if (result.HasError)
            {
                return result;
            }
            await _paymentLogService.AddLog(new CreatePaymentLogDTO
            {
                GuidAccountOrigin = vm.SavingAccountGuid,
                GuidProductDestination = vm.CreditCardGuid,
                Amount = vm.Amount
            });

            return response;
        }
    }
}
