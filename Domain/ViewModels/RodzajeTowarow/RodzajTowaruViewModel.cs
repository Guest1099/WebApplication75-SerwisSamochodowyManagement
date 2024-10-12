using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels.RodzajeTowarow
{
    public class RodzajTowaruViewModel
    {
        public RodzajTowaru RodzajTowaru { get; set; }



        public List<IFormFile> Files { get; set; }
        public bool Success { get; set; }
        public string Result { get; set; }
    }
}
