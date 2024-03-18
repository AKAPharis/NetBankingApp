using AutoMapper;
using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.Enums;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.Account;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;

namespace NetBankingApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, ISavingAccountService savingAccountService, IMapper mapper)
        {
            _accountService = accountService;
            _savingAccountService = savingAccountService;
            _mapper = mapper;
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }




        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {
            return await _accountService.ForgotPasswordAsync(_mapper.Map<ForgotPasswordRequest>(vm), origin);
        }


        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            return await _accountService.AuthenticateAsync(_mapper.Map<AuthenticationRequest>(vm));
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
        {
            var user = await _accountService.RegisterUserAsync(_mapper.Map<RegisterRequest>(vm), origin);
            if (user != null && vm.Role == Roles.Customer.ToString())
            {
                await _savingAccountService.CreateAsync(new SaveSavingAccountViewModel
                {
                   IdCustomer = user.Id,
                   Savings = vm.InitialAmount.Value,
                   IsMain = true,
                });

            }
            return user;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(string token, string username)
        {
            //Falta decidir si se va a hacer contraseña aleatorio o simplemente se cambia como se quiera
            return await _accountService.ResetPasswordAsync(new ResetPasswordRequest { Token = token, Username = username });
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }


        #region Pendientes

        //public async Task EditAsync(SaveUserViewModel vm, string origin)
        //{
        //    await _accountService.
        //}

        //public Task<UserViewModel> GetByIdAsync(string id)
        //{
        //    return _mapper.Map<UserViewModel>(_accountService.);
        //}

        //public Task<SaveUserViewModel> GetByIdSaveViewModelAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<UserViewModel> GetByUsernameAsync(string username)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}
