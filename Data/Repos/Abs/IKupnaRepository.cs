using Domain.Models;
using Domain.ViewModels.Kupna;

namespace Data.Repos.Abs
{
    public interface IKupnaRepository
    {
        Task<List<Kupno>> GetAll();
        Task<Kupno> Get(string id);
        Task<KupnoViewModel> Create(KupnoViewModel model);
        Task<KupnoViewModel> Update(KupnoViewModel model);
        Task<bool> Delete(string id);
    }
}
