using AutoMapper;
using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.Dtos.Logs;
using NetBankingApp.Core.Application.ViewModels.Account;
using NetBankingApp.Core.Application.ViewModels.Beneficiary;
using NetBankingApp.Core.Application.ViewModels.CreditCard;
using NetBankingApp.Core.Application.ViewModels.Loan;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using NetBankingApp.Core.Domain.Models;

namespace NetBankingApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ReverseMap();
            CreateMap<LoginViewModel, AuthenticationRequest>()
                .ReverseMap();
            CreateMap<SaveUserViewModel, RegisterRequest>()
                .ReverseMap();
            CreateMap<SaveUserViewModel, EditRequest>()
                .ReverseMap();
            CreateMap<TransactionLog, CreateTransactionLogDTO>()
                .ReverseMap();
            CreateMap<SaveSavingAccountViewModel, SavingAccount>()
                .ReverseMap();
            CreateMap<SavingAccountViewModel, SavingAccount>()
                .ReverseMap();
            CreateMap<CreatePaymentLogDTO, PaymentLog>()
                .ReverseMap();
            CreateMap<CreatePaymentLogDTO, PaymentLog>()
                .ReverseMap();
            CreateMap<SaveLoanViewModel, Loan>()
                .ReverseMap();
            CreateMap<LoanViewModel, Loan>()
                .ReverseMap();
            CreateMap<SaveCreditCardViewModel, CreditCard>()
                .ReverseMap();
            CreateMap<CreditCardViewModel, CreditCard>()
                .ReverseMap();
            CreateMap<SaveBeneficiaryViewModel, Beneficiary>()
                .ReverseMap();

        }


    }
}
