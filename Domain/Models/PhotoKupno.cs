using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class PhotoKupno
    {
        [Key]
        public string PhotoKupnoId { get; set; }
        public byte[] PhotoData { get; set; }


        public string KupnoId { get; set; }
        public Kupno Kupno { get; set; }
    }
}
