using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class RodzajTowaru
    {
        [Key]
        public string RodzajTowaruId { get; set; }
        public string Name { get; set; }


        public List<Towar>? Towary { get; set; }
    }
}
