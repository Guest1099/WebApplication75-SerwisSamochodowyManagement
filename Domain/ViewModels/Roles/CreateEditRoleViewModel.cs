using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ViewModels.Roles
{
    public class CreateEditRoleViewModel
    {
        [Required (ErrorMessage = "*")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public bool IsExists { get; set; }
        public string Result { get; set; }
    }
}
