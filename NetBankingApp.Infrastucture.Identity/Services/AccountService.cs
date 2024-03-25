using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetBankingApp.Core.Application.Dtos.Account;
using NetBankingApp.Core.Application.Enums;
using NetBankingApp.Core.Application.Interfaces.Services;
using NetBankingApp.Core.Application.ViewModels.Account;
using NetBankingApp.Infrastucture.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NetBankingApp.Infrastucture.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<BankingUser> _userManager;
        private readonly SignInManager<BankingUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;


        public AccountService(UserManager<BankingUser> userManager, SignInManager<BankingUser> signInManager, IEmailService emailService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<UserViewModel> GetByIdAsync(string id)
        {
            return _mapper.Map<UserViewModel>(await _userManager.FindByIdAsync(id));
        }
        public async Task DeactivateUser(string id)
        {
            BankingUser user = await _userManager.FindByIdAsync(id);
            user.IsActived = false;
            await _userManager.UpdateAsync(user);
        }
        public async Task ActivateUser(string id)
        {
            BankingUser user = await _userManager.FindByIdAsync(id);
            user.IsActived = true;
            await _userManager.UpdateAsync(user);
        }
        public async Task<UserViewModel> GetByUsernameAsync(string username)
        {
            return _mapper.Map<UserViewModel>(await _userManager.FindByNameAsync(username));
        }
        public async Task<SaveUserViewModel> GetByIdSaveViewModelAsync(string id)
        {
            return _mapper.Map<SaveUserViewModel>(await _userManager.FindByIdAsync(id));
        }
        public async Task<int> GetActiveUsers()
        {
            var users = await _userManager.Users.Where(x => x.IsActived).ToArrayAsync();
            return users.Count();
        }
        public async Task<int> GetInactiveUsers()
        {
            var users = await _userManager.Users.Where(x => !x.IsActived).ToArrayAsync();
            return users.Count();
        }
        public async Task<List<UserViewModel>> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersVm = _mapper.Map<List<UserViewModel>>(users);
            if (users != null && users.Count > 0)
            {

                usersVm.ForEach(async x =>
                {
                    var roles = await _userManager.GetRolesAsync(users.FirstOrDefault(y => y.Id == x.Id));
                    x.Roles = roles.ToList();
                });
            }
            return usersVm;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Username}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Username}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.Username}";
                return response;
            }


            response.Id = user.Id;
            response.Email = user.Email;
            response.Username = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();



            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }
            var userWithSameDocumentId = await _userManager.Users.FirstOrDefaultAsync(x => x.DocumentId == request.DocumentId);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"The document id '{request.DocumentId}' is already registered.";
                return response;
            }

            if (request.Role != Roles.Customer.ToString() && request.Role != Roles.Admin.ToString())
            {
                response.HasError = true;
                response.Error = $"The role {request.Role} do not exist";
                return response;
            }


            var user = new BankingUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                DocumentId = request.DocumentId,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {

                switch (request.Role)
                {
                    case nameof(Roles.Admin):

                        await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());


                        break;
                    case nameof(Roles.Customer):

                        await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());


                        break;


                }

                var verificationUri = await SendVerificationEmailUri(user, origin);

                await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                {
                    To = user.Email,
                    Body = $"Please confirm your account visiting this URL {verificationUri}",
                    Subject = "Confirm registration"
                });
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }
            var createdUser = await _userManager.FindByNameAsync(user.UserName);
            response.Id = createdUser.Id;
            return response;
        }


        public async Task<EditResponse> EditUserAsync(EditRequest request, string origin)
        {
            EditResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            if (request.Role != Roles.Customer.ToString() && request.Role != Roles.Admin.ToString())
            {
                response.HasError = true;
                response.Error = $"The role {request.Role} do not exist";
                return response;
            }



            BankingUser user = await _userManager.FindByIdAsync(request.DocumentId);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"We couldn't be able to find your user.";
                return response;

            }
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.DocumentId = request.DocumentId;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {

                response.HasError = true;
                response.Error = $"An error occurred trying to edit the user.";
                return response;
            }

            if (request.Password != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, request.Password);
            }

            return response;
        }


        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No accounts registered with this user";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            else
            {
                return $"An error occurred while confirming {user.Email}.";
            }
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Username}";
                return response;
            }

            var verificationUri = await SendForgotPasswordUri(user, origin);

            await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
            {
                To = user.Email,
                Body = $"Please reset your account visiting this URL {verificationUri}",
                Subject = "reset password"
            });


            return response;
        }
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Username}";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while reset password";
                return response;
            }

            return response;
        }

        //private async Task<JwtSecurityToken> GenerateJWToken(BankingUser user)
        //{
        //    var userClaims = await _userManager.GetClaimsAsync(user);
        //    var roles = await _userManager.GetRolesAsync(user);

        //    var roleClaims = new List<Claim>();

        //    foreach (var role in roles)
        //    {
        //        roleClaims.Add(new Claim("roles", role));
        //    }

        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Email,user.Email),
        //        new Claim("uid", user.Id)
        //    }
        //    .Union(userClaims)
        //    .Union(roleClaims);

        //    var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        //    var signingCredetials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);
        //    var jwtSecurityToken = new JwtSecurityToken(
        //        issuer: _jwtSettings.Issuer,
        //        audience: _jwtSettings.Audience,
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
        //        signingCredentials: signingCredetials);

        //    return jwtSecurityToken;
        //}

        //private RefreshToken GenerateRefreshToken()
        //{
        //    return new RefreshToken
        //    {
        //        Token = RandomTokenString(),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        Created = DateTime.UtcNow
        //    };
        //}

        //private string RandomTokenString()
        //{
        //    using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
        //    var ramdomBytes = new byte[40];
        //    rngCryptoServiceProvider.GetBytes(ramdomBytes);

        //    return BitConverter.ToString(ramdomBytes).Replace("-", "");
        //}


        private async Task<string> SendVerificationEmailUri(BankingUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id.ToString());
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }
        private async Task<string> SendForgotPasswordUri(BankingUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "username", user.UserName);


            return verificationUri;
        }
    }
}
