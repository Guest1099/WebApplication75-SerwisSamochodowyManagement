using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace Domain.ViewModels.Roles
{
    public class RolesViewModel : BaseViewModel <ApplicationRole>
    {
        public List <ApplicationRole> Roles { get; set; }
    }
}
