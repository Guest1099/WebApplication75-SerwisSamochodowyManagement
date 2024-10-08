using Domain.Models;
using Domain.ViewModels.Owners;

namespace Data.Repos.Abs
{
    public interface IOwnersRepository
    {
        Task<List<Owner>> GetAll();
        Task<Owner> Get(string id);
        Task<OwnerViewModel> Create(OwnerViewModel model);
        Task<OwnerViewModel> Update(OwnerViewModel model);
        Task<bool> Delete(string id);
    }
}
