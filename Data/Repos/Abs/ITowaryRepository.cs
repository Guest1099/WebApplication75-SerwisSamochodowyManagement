using Domain.Models;
using Domain.ViewModels.Towary;

namespace Data.Repos.Abs
{
    public interface ITowaryRepository
    {
        Task<List<Towar>> GetAll();
        Task<Towar> Get(string id);
        Task<TowarViewModel> Create(TowarViewModel model);
        Task<TowarViewModel> Update(TowarViewModel model);
        Task<bool> Delete(string id);
    }
}
