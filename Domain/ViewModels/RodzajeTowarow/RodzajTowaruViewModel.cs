using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels.RodzajeTowarow
{
    public class RodzajTowaruViewModel : RodzajTowaru
    {
        public List<IFormFile> Files { get; set; }
        public bool Success { get; set; }
        public string Result { get; set; }
    }
}
