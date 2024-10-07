using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class RodzajPojazdu
    {
        [Key]
        public string RodzajPojazduId { get; set; }
        public string Name { get; set; }


        public List<Kupno>? Kupna { get; set; }
        public List<Sprzedaz>? Sprzedasze { get; set; }

    }
}
