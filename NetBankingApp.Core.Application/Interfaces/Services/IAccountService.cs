using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.ViewModels.Account;

namespace NetBankingApp.Core.Application.Interfaces.Services
{

    public interface IAccountService
    {
        Task<UserViewModel> GetByIdAsync(string id);
        Task<SaveUserViewModel> GetByIdSaveViewModelAsync(string id);
        Task<UserViewModel> GetByUsernameAsync(string username);
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
        Task<EditResponse> EditUserAsync(EditRequest request, string origin);

        Task SignOutAsync();
    }

}
