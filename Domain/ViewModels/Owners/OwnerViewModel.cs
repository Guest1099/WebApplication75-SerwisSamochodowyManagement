using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels.Owners
{
    public class OwnerViewModel
    {
        public Owner Owner { get; set; }
        public Models.DaneOsobowe DaneOsobowe { get; set; }



        public List<IFormFile> Files { get; set; }
        public bool Success { get; set; }
        public string Result { get; set; }
    }
}
