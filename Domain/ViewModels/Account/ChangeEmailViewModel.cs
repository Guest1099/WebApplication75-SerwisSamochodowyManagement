using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels.Account
{
    public class ChangeEmailViewModel
    {
        public string UserName { get; set; }

        public string ObecnyEmail { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        public string NewEmail { get; set; }

        public bool Success { get; set; }

        public string Result { get; set; }
    }
}
