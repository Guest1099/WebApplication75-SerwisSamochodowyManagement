using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class PhotoDaneOsobowe
    {
        [Key]
        public string PhotoDaneOsoboweId { get; set; }
        public byte[] PhotoData { get; set; }



        public string DaneOsoboweId { get; set; }
        public DaneOsobowe DaneOsobowe { get; set; }
    }
}
