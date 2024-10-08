using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels.Kupna
{
    public class KupnoViewModel
    {
        public string TowarId { get; set; }
        public Models.DaneOsobowe DaneOsobowe { get; set; }
        public Kupno Kupno { get; set; }
        public Towar Towar { get; set; }
        public PhotoKupno PhotoKupnot { get; set; }



        public List<IFormFile> Files { get; set; }
        public bool Success { get; set; }
        public string Result { get; set; }
    }
}
