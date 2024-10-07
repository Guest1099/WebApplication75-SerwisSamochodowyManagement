using Domain.Models;
using Domain.ViewModels.Roles;

namespace Application.Services.Abs
{
    public interface IRolesService
    {
        Task<List<ApplicationRole>> GetAll();
        Task<ApplicationRole> Get(string id);
        Task<CreateRoleViewModel> Create(CreateRoleViewModel model);
        Task Delete(string id);
        Task<EditRoleViewModel> Update(EditRoleViewModel model);
        Task<List<ApplicationUser>> UsersInRole(string roleName);
    }
}
