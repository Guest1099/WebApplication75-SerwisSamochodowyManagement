using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Client
    {
        [Key]
        public string ClientId { get; set; }


        public string DaneOsoboweId { get; set; }
        public DaneOsobowe DaneOsobowe { get; set; }


        public List<Kupno>? Kupna { get; set; }
        public List<Sprzedaz>? Sprzedaze { get; set; }
    }
}
