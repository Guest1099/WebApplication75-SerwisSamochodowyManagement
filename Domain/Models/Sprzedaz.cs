using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Sprzedaz
    {
        [Key]
        public string SprzedazId { get; set; }
        public string Opis { get; set; }


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
        public double VatBrutto23 { get; set; }

        public double ZyskNetto { get; set; }

        public double ZyskBrutto { get; set; }


        public int Sztuk { get; set; }
        public double Rabat { get; set; }


        public string DodatkoweInformacje { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string DataSprzedazy { get; set; }



        public string? OwnerId { get; set; }
        public Owner? Owner { get; set; }


        public string? ClientId { get; set; }
        public Client? Client { get; set; }



        public string? TowarId { get; set; }
        public Towar? Towar { get; set; }


        public List<PhotoSprzedaz>? PhotosSprzedaz { get; set; }

    }
}
