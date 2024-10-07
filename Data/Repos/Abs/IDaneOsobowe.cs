using Domain.Models;
using Domain.ViewModels.DaneOsobowe;

namespace Data.Repos.Abs
{
    public interface IDaneOsobowe
    {
        Task<List<Client>> GetAll();
        Task<Client> Get(string id);
        Task<DanaOsobowaViewModel> Create(DanaOsobowaViewModel model);
        Task<DanaOsobowaViewModel> Update(DanaOsobowaViewModel model);
        Task<bool> Delete(string id);
    }
}
