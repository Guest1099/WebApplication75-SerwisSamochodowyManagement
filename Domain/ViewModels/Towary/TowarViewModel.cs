using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels.Towary
{
    public class TowarViewModel : Towar
    {
        public List<IFormFile> Files { get; set; }
        public bool Success { get; set; }
        public string Result { get; set; }
    }
}
