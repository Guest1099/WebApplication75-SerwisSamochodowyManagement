using Domain.Models;
using Domain.ViewModels.DaneOsobowe;

namespace Data.Repos.Abs
{
    public interface IDaneOsobowe
    {
        Task<List<DaneOsobowe>> GetAll();
        Task<DaneOsobowe> Get(string id);
        Task<DanaOsobowaViewModel> Create(DanaOsobowaViewModel model);
        Task<DanaOsobowaViewModel> Update(DanaOsobowaViewModel model);
        Task<bool> Delete(string id);
    }
}
