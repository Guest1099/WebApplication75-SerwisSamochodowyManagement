using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels.Roles
{
    public class CreateEditRoleViewModel
    {
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public bool IsExists { get; set; }
        public string Result { get; set; }
    }
}
