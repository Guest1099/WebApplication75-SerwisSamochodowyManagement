using Domain.Models;
using Domain.ViewModels.Sprzedaze;

namespace Data.Repos.Abs
{
    public interface ISprzedazeRepository
    {
        Task<List<Sprzedaz>> GetAll();
        Task<Sprzedaz> Get(string id);
        Task<SprzedazViewModel> Create(SprzedazViewModel model);
        Task<SprzedazViewModel> Update(SprzedazViewModel model);
        Task<bool> Delete(string id);
    }
}
