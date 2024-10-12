using Domain.Models;
using Domain.ViewModels.Users;

namespace Application.Services.Abs
{
    public interface IUsersService
    {
        Task<List<ApplicationUser>> GetAll();
        Task<ApplicationUser> Get(string id);
        Task<CreateUserViewModel> Create(CreateUserViewModel model);
        Task<EditUserViewModel> Update(EditUserViewModel model);
        Task<bool> DeleteByUserName(string userName);
    }
}
