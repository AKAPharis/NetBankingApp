using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.ViewModels.Account;

namespace NetBankingApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserViewModel> GetByIdAsync(string id);
        Task<SaveUserViewModel> GetByIdSaveViewModelAsync(string id);
        Task<UserViewModel> GetByUsernameAsync(string username);
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<EditResponse> EditAsync(SaveUserViewModel vm, string origin);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(string token, string username);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<List<UserViewModel>> GetAll();
        Task SignOutAsync();
        Task<int> GetActiveUsers();

        Task<int> GetInactiveUsers();

        Task DeactivateUser(string id);

        Task ActivateUser(string id);
    }
}
