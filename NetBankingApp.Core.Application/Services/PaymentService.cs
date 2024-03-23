using NetBankingApp.Core.Application.Interfaces.Services;

namespace NetBankingApp.Core.Application.Services
{
    public class PaymentService
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



    }
}
