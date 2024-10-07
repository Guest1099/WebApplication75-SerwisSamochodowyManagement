using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels.Sprzedaze
{
    public class SprzedazViewModel
    {
        public Sprzedaz Sprzedaz { get; set; }
        public List<IFormFile> Files { get; set; }
        public bool Success { get; set; }
        public string Result { get; set; }
    }
}
