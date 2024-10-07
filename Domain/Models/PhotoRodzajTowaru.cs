using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class PhotoRodzajTowaru
    {
        [Key]
        public string PhotoRodzajTowaruId { get; set; }
        public byte[] PhotoData { get; set; }


        public string RodzajTowaruId { get; set; }
        public RodzajTowaru RodzajTowaru { get; set; }
    }
}
