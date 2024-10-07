using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Marka
    {
        [Key]
        public string MarkaId { get; set; }
        public string Name { get; set; }

        public List<Kupno>? Kupna { get; set; }
        public List<Sprzedaz>? Sprzedaze { get; set; }
    }
}
