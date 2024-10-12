using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels.Marki
{
    public class MarkaViewModel
    {
        public Marka? Marka { get; set; }


        public List<IFormFile>? Files { get; set; }
        public bool Success { get; set; }
        public string? Result { get; set; }
    }
}
