using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class RodzajTowaru
    {
        [Key]
        public string RodzajTowaruId { get; set; }
        public string Nazwa { get; set; }



        public List<Kupno> Kupna { get; set; }
        public List<Sprzedaz> Sprzedaze { get; set; }
        public List<Towar> Towary { get; set; }
        public List<PhotoRodzajTowaru> PhotosRodzajTowaru { get; set; }
    }
}
