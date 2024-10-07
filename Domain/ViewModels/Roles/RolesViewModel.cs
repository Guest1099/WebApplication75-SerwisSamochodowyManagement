using Domain.Models;

namespace Domain.ViewModels.Roles
{
    public class RolesViewModel : BaseViewModel<ApplicationRole>
    {
        public List<ApplicationRole> Roles { get; set; }
    }
}
