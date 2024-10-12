using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels.Owners
{
    public class OwnersViewModel : BaseViewModel<Owner>
    {
        public List<Owner> Owners { get; set; }



        public List<IFormFile> Files { get; set; }
        public bool Success { get; set; }
        public string Result { get; set; }
    }
}
