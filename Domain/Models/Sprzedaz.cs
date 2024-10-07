using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Sprzedaz
    {
        [Key]
        public string SprzedazId { get; set; }
         

        [Required]
        [DataType(DataType.Currency)]
        public double CenaZakupu { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double CenaSprzedazyNetto23 { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double CenaSprzedazyBrutto23 { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double VatNetto23 { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double VatBrutton23 { get; set; }

        public double ZyskNetto { get; set; }

        public double ZyskBrutto { get; set; }


        public int Sztuk { get; set; }
        public double Rabat { get; set; }

         
        public string DodatkoweInformacje { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataSprzedazy { get; set; }



        public string? OwnerId { get; set; }
        public Owner? OwnerKupujacy { get; set; }


        public string? ClientSprzedajacyId { get; set; }
        public Client? ClientSprzedajacy { get; set; }

         

        public string? TowarId { get; set; }
        public Towar? Towar { get; set; }

         
        public List<PhotoSprzedaz>? PhotosSprzedaz { get; set; }

    }
}
