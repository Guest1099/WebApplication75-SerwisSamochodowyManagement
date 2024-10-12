using Domain.Models;

namespace Domain.ViewModels.Clients
{
    public class ClientsViewModel : BaseViewModel<Client>
    {
        public List <Client> Clients { get; set; }
    }
}
