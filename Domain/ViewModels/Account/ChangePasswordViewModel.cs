using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        public string UserName { get; set; }


        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        public bool Success { get; set; }

        public string Result { get; set; }
    }
}
