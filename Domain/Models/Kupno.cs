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



        public string? OwnerKupujacyId { get; set; }
        public Owner? OwnerKupujacy { get; set; }


        public string? ClientSprzedajacyId { get; set; }
        public Client? ClientSprzedajacy { get; set; }


        public string? TowarId { get; set; }
        public Towar Towar { get; set; }


        public List<PhotoKupno>? PhotosKupno { get; set; }
    }
}
