using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Marka
    {
        [Key]
        public string? MarkaId { get; set; }

        [Required]
        [MinLength(1)]
        public string? Name { get; set; }



        public List<Towar>? Towary { get; set; }

    }
}
