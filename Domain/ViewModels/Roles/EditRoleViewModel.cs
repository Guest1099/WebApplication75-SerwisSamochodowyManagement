using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ViewModels.Roles
{
    public class EditRoleViewModel : CreateEditRoleViewModel
    {
        public string RoleId { get; set; }
        public string ChangeName { get; set; }
    }
}
