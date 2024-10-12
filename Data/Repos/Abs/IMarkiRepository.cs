using Domain.Models;
using Domain.ViewModels.Marki;

namespace Data.Repos.Abs
{
    public interface IMarkiRepository
    {
        Task<List<Marka>> GetAll();
        Task<Marka> Get(string id);
        Task<MarkaViewModel> Create(MarkaViewModel model);
        Task<MarkaViewModel> Update(MarkaViewModel model);
        Task<bool> Delete(string id);
    }
}
