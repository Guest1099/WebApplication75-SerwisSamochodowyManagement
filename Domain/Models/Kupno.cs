using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Kupno
    {
        [Key]
        public string KupnoId { get; set; }

        public double CenaZakupu { get; set; }
        public double CenaSprzedazy { get; set; }


        [Required]
        public string DataZakupu { get; set; }
        public string DodatkoweInformacje { get; set; }



        public string? OwnerId { get; set; }
        public Owner? Owner { get; set; }


        public string? ClientId { get; set; }
        public Client? Client { get; set; }


        public string? TowarId { get; set; }
        public Towar? Towar { get; set; }


        public List<PhotoKupno>? PhotosKupno { get; set; }
    }
}
