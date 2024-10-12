using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class PhotoSprzedaz
    {
        [Key]
        public string PhotoSprzedazId { get; set; }
        public byte[] PhotoData { get; set; }


        public string SprzedazId { get; set; }
        public Sprzedaz Sprzedaz { get; set; }
    }
}
