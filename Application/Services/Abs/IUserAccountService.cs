using Domain.Models;
using Domain.ViewModels.Account;
using Domain.ViewModels.Users;

namespace Application.Services.Abs
{
    public interface IUserAccountService
    {
        Task<List<ApplicationUser>> GetAll();
        Task<ApplicationUser> GetUserById(string userId);
        Task<ApplicationUser> GetUserByEmail(string userEmail);
        Task<ApplicationUser> GetUserByName(string userName);
        Task<LoginViewModel> Login(LoginViewModel model);
        Task Logout();
        Task<ChangeEmailViewModel> ChangeEmail(ChangeEmailViewModel model);
        Task<ChangePasswordViewModel> ChangePassword(ChangePasswordViewModel model);
        Task<UpdateAccountViewModel> UpdateAccount(UpdateAccountViewModel model);
        Task<UpdateAccountViewModel> UpdateAccountSingle(UpdateAccountViewModel model);

        Task<CreateUserViewModel> CreateUserAccount(CreateUserViewModel model);
        Task<EditUserViewModel> UpdateUserAccount(EditUserViewModel model);

        Task<bool> DeleteAccountByUserId(string id);
        Task<bool> DeleteAccountByEmail(string email);
        Task<bool> DeleteAccountByName(string name);
        Task<bool> RemoveFromRole(string userName, string roleName);

        Task<bool> AddToRole(string userName, string roleName);
        Task<bool> AddClaim(string userName, string roleName);
        Task<List<string>> GetUserRoles(string userName);
        Task<List<ApplicationUser>> GetUsersInRole(string roleName);
        Task<bool> LoggedUserIsAdmin(string email);
    }
}
