using Domain.Models;
using Domain.ViewModels.Clients;

namespace Data.Repos.Abs
{
    public interface IClientsRepository
    {
        Task<List<Client>> GetAll();
        Task<Client> Get(string id);
        Task<ClientViewModel> Create(ClientViewModel model);
        Task<ClientViewModel> Update(ClientViewModel model);
        Task<bool> Delete(string id);
    }
}
