using Domain.Models;

namespace Domain.ViewModels.Users
{
    public class UsersViewModel : BaseViewModel<ApplicationUser>
    {
        public List<ApplicationUser> Users { get; set; }
    }
}
