using AutoMapper;
using NetBankingApp.Core.Application.Dtos.Logs;
using NetBankingApp.Core.Application.Dtos.Transaction;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.Transaction;


namespace NetBankingApp.Core.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionLogService _transactionLogService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly ICreditCardService _creditCardService;
        private readonly ILoanService _loanService;
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionLogService transactionLogService, ISavingAccountService savingAccountService, ICreditCardService creditCardService, ILoanService loanService, IBeneficiaryService beneficiaryService, IUserService userService, IMapper mapper)
        {
            _transactionLogService = transactionLogService;
            _savingAccountService = savingAccountService;
            _creditCardService = creditCardService;
            _loanService = loanService;
            _beneficiaryService = beneficiaryService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<TransactionResponse> TransactionToAccount(TransactionToAccountViewModel vm)
        {
            TransactionResponse response = new();
            if (vm.GuidAccountOrigin == vm.GuidAccountDestination)
            {
                response.Error = "You cannot make transactions between the same account";
                response.HasError = true;
                return response;
            }
            var withdraw = await _savingAccountService.Withdraw(vm.GuidAccountOrigin, vm.Amount);
            if (withdraw == 0)
            {
                response.Error = "The origin account do not have enough founds";
                response.HasError = true;
                return response;
            }
            var depositResult = await _savingAccountService.Deposit(vm.Amount, vm.GuidAccountDestination);
            if (depositResult.HasError)
            {
                return depositResult;
            }
            await _transactionLogService.AddLog(new CreateTransactionLogDTO
            {
                Amount = vm.Amount,
                GuidAccountDestination = vm.GuidAccountDestination,
                GuidAccountOrigin = vm.GuidAccountOrigin
            });


            return response;
        }
        public async Task<TransactionResponse> TransactionExpressValidation(TransactionExpressViewModel vm)
        {
            TransactionResponse response = new();
            var destinationAccount = await _savingAccountService.GetByGuid(vm.GuidAccountDestination);
            if(destinationAccount == null)
            {
                response.Error = "The account to deposit was not found";
                response.HasError = true;
                return response;
            }
            var originAccount = await _savingAccountService.GetByGuid(vm.GuidAccountOrigin);
            if(originAccount == null)
            {
                response.Error = "The account origin was not found";
                response.HasError = true;
                return response;
            }
            if (originAccount.Savings < vm.Amount)
            {
                response.Error = "The origin account do not have enough founds";
                response.HasError = true;
                return response;
            }
            var destinationUser = await _userService.GetByIdAsync(destinationAccount.IdCustomer);
            if (destinationUser == null)
            {
                response.Error = "The owner of the account destination was not found";
                response.HasError = true;
                return response;
            }
            response.DestinationOwnerFirsName = destinationUser.FirstName;
            response.DestinationOwnerLastName = destinationUser.LastName;
            response.Amount = vm.Amount;
            response.GuidAccountOrigin = vm.GuidAccountOrigin;
            response.GuidAccountDestination = vm.GuidAccountDestination;
            return response;
        }
        public async Task<TransactionResponse> TransactionExpress(TransactionExpressViewModel vm)
        {
            TransactionResponse response = new();
            var withdraw = await _savingAccountService.Withdraw(vm.GuidAccountOrigin, vm.Amount);
            if (withdraw == 0)
            {
                response.Error = "The origin account do not have enough founds";
                response.HasError = true;
                return response;
            }
            var depositResult = await _savingAccountService.Deposit(vm.Amount, vm.GuidAccountDestination);
            if (depositResult.HasError)
            {
                return depositResult;
            }
            await _transactionLogService.AddLog(new CreateTransactionLogDTO
            {
                Amount = vm.Amount,
                GuidAccountDestination = vm.GuidAccountDestination,
                GuidAccountOrigin = vm.GuidAccountOrigin
            });


            return response;
        }
        public async Task<TransactionResponse> TransactionBeneficiaryValidation(TransactionBeneficiaryViewModel vm)
        {
            TransactionResponse response = new();
            var beneficiary = await _beneficiaryService.GetBeneficiary(vm.IdUser, vm.IdBeneficiary);
            if (beneficiary == null)
            {
                response.Error = "The beneficiary was not found";
                response.HasError = true;
                return response;
            }
            var destinationAccount = await _savingAccountService.GetByGuid(beneficiary.BeneficiaryAccountGuid);
            if (destinationAccount == null)
            {
                response.Error = "The account to deposit was not found";
                response.HasError = true;
                return response;
            }
            var originAccount = await _savingAccountService.GetByGuid(vm.GuidAccountOrigin);
            if (originAccount == null)
            {
                response.Error = "The account origin was not found";
                response.HasError = true;
                return response;
            }
            if (originAccount.Savings < vm.Amount)
            {
                response.Error = "The origin account do not have enough founds";
                response.HasError = true;
                return response;
            }
            var destinationUser = await _userService.GetByIdAsync(destinationAccount.IdCustomer);
            if (destinationUser == null)
            {
                response.Error = "The owner of the account destination was not found";
                response.HasError = true;
                return response;
            }
            response.DestinationOwnerFirsName = destinationUser.FirstName;
            response.DestinationOwnerLastName = destinationUser.LastName;
            response.Amount = vm.Amount;
            response.GuidAccountOrigin = vm.GuidAccountOrigin;
            response.IdBeneficiary = vm.IdBeneficiary;
            return response;
        }
        public async Task<TransactionResponse> TransactionBeneficiary(TransactionBeneficiaryViewModel vm)
        {
            TransactionResponse response = new();
            var beneficiary = await _beneficiaryService.GetBeneficiary(vm.IdUser, vm.IdBeneficiary);
            if (beneficiary == null)
            {
                response.Error = "The beneficiary was not found";
                response.HasError = true;
                return response;
            }


            var withdraw = await _savingAccountService.Withdraw(vm.GuidAccountOrigin, vm.Amount);
            if (withdraw == 0)
            {
                response.Error = "The origin account do not have enough founds";
                response.HasError = true;
                return response;
            }
            var depositResult = await _savingAccountService.Deposit(vm.Amount, beneficiary.BeneficiaryAccountGuid);
            if (depositResult.HasError)
            {
                return depositResult;
            }
            await _transactionLogService.AddLog(new CreateTransactionLogDTO
            {
                Amount = vm.Amount,
                GuidAccountDestination = beneficiary.BeneficiaryAccountGuid,
                GuidAccountOrigin = vm.GuidAccountOrigin
            });


            return response;
        }





    }
}
