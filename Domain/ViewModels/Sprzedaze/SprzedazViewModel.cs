using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels.Sprzedaze
{
    public class SprzedazViewModel
    {
        public string TowarId { get; set; }
        public Models.DaneOsobowe DaneOsobowe { get; set; }
        public Client Client { get; set; }
        public Sprzedaz Sprzedaz { get; set; }



        public List<IFormFile> Files { get; set; }
        public bool Success { get; set; }
        public string Result { get; set; }
    }
}
