using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Sprzedaz
    {
        [Key]
        public string SprzedazId { get; set; }

        [Required]
        public string Nazwa { get; set; }
        [Required]
        public string Opis { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double CenaZakupu { get; set; }
        public int Sztuk { get; set; }
        public double Rabat { get; set; }
        [Required]
        public string Kolor { get; set; }
        [Required]
        public double Wysokosc { get; set; }
        [Required]
        public double Szerokosc { get; set; }
        [Required]
        public double Waga { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime RokProdukcji { get; set; }
        [Required]
        public double Przebieg { get; set; }
        [Required]
        public double CenaSprzedazy { get; set; }
        public double CenaSprzedazyVat { get; set; }
        public double CenaSprzedazyNetto22 { get; set; }

        public double Zysk { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataSprzedazy { get; set; }



        public string? SprzedajacyId { get; set; }
        public DaneOsobowe? Sprzedajacy { get; set; }

        public string? KupujacyId { get; set; }
        public DaneOsobowe? Kupujacy { get; set; }

        public string? RodzajPojazduId { get; set; }
        public RodzajPojazdu? RodzajPojazdu { get; set; }


        public string? TowarId { get; set; }
        public Towar? Towar { get; set; }


        public string? RodzajTowaruId { get; set; }
        public RodzajTowaru? RodzajTowaru { get; set; }

        public string? MarkaId { get; set; }
        public Marka? Marka { get; set; }

        public List<PhotoSprzedaz>? PhotosSprzedaz { get; set; }

    }
}
