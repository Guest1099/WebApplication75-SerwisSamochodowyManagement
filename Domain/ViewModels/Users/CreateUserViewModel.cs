using Data.Services;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels.Users
{
    public class CreateUserViewModel : CreateEditUserViewModel
    {
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć co najmniej 10 znaków")]
        [DataType(DataType.Password)]
        [PasswordRequirements]
        public string Password { get; set; }
    }
}
