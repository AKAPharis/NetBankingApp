using AutoMapper;
using Microsoft.AspNetCore.Http;
using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.Enums;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.Account;
using NetBankingApp.Core.Application.ViewModels.SavingAccount;
using NetBankingApp.Core.Application.Helpers;

namespace NetBankingApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IAccountService accountService, ISavingAccountService savingAccountService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _savingAccountService = savingAccountService;
            _mapper = mapper;
            _contextAccessor = httpContextAccessor;
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
            var response = await _accountService.RegisterUserAsync(_mapper.Map<RegisterRequest>(vm), origin);
            if (response != null && !response.HasError && vm.Role == Roles.Customer.ToString())
            {
                await _savingAccountService.CreateAsync(new SaveSavingAccountViewModel
                {
                    IdCustomer = response.Id,
                    Savings = vm.InitialAmount,
                    IsMain = true,
                });

            }
            return response;
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

        public async Task<EditResponse> EditAsync(SaveUserViewModel vm, string origin)
        {
            var result = new EditResponse();
            var loggedUser = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            if (loggedUser == null)
            {

                result.Error = "You need to be logged to edit an user";
                result.HasError = true;
                return result;

            }
            if (vm.Role == Roles.Admin.ToString() && vm.Id == loggedUser.Id)
            {
                result.Error = "You cannot edit yourself";
                result.HasError = true;
                return result;
            }

            result = await _accountService.EditUserAsync(_mapper.Map<EditRequest>(vm), origin);
            if (result != null && !result.HasError)
            {
                if (vm.Role == Roles.Customer.ToString() && vm.InitialAmount > 0)
                {
                    await _savingAccountService.DepositToMain(vm.InitialAmount, vm.Id);
                }
            }

            return result;
        }
        public async Task<List<UserViewModel>> GetAll()
        {
            return await _accountService.GetAll();
        }

        public async Task<UserViewModel> GetByIdAsync(string id)
        {
            return await _accountService.GetByIdAsync(id);
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModelAsync(string id)
        {
            return await _accountService.GetByIdSaveViewModelAsync(id);
        }

        public async Task<UserViewModel> GetByUsernameAsync(string username)
        {
            return await _accountService.GetByUsernameAsync(username);
        }

        public async Task<int> GetActiveUsers()
        {
            return await _accountService.GetActiveUsers();
        }

        public async Task<int> GetInactiveUsers()
        {
            return await _accountService.GetInactiveUsers();
        }

        public async Task DeactivateUser(string id)
        {
            var loggedUser = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            if (loggedUser != null && loggedUser.Id != id)
            {
                await _accountService.DeactivateUser(id);


            }


        }

        public async Task ActivateUser(string id)
        {
            var loggedUser = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            if (loggedUser != null && loggedUser.Id != id)
            {


                await _accountService.ActivateUser(id);
            }
        }
    }
}
