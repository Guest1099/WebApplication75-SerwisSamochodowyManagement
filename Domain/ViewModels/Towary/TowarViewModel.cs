using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels.Towary
{
    public class TowarViewModel
    {
        public Towar? Towar { get; set; }


        public List<IFormFile>? Files { get; set; }
        public bool Success { get; set; }
        public string? Result { get; set; }
    }
}
