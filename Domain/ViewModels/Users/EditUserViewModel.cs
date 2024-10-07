namespace Domain.ViewModels.Users
{
    public class EditUserViewModel : CreateEditUserViewModel
    {
        public List<string> Roles { get; set; }
        public bool IsExists { get; set; }
    }
}
