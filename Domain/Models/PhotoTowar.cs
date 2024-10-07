using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class PhotoTowar
    {
        [Key]
        public string PhotoTowarId { get; set; }
        public byte[] PhotoData { get; set; }


        public string TowarId { get; set; }
        public Towar Towar { get; set; }

    }
}
