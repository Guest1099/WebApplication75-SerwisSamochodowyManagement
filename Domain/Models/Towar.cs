using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Towar
    {
        [Key]
        public string? TowarId { get; set; }
        public string? Nazwa { get; set; }
        public string? Opis { get; set; }
        public double? Cena { get; set; }
        public int? Ilosc { get; set; }
        public string? Kolor { get; set; }
        public double? Wysokosc { get; set; }
        public double? Szerokosc { get; set; }
        public double? Waga { get; set; }
        public string? RokProdukcji { get; set; }
        public double? Przebieg { get; set; }
        public double? Rabat { get; set; }
        public string? DataDodania { get; set; }




        public string? RodzajTowaruId { get; set; }
        public RodzajTowaru? RodzajTowaru { get; set; }



        public string? MarkaId { get; set; }
        public Marka? Marka { get; set; }




        public List<Kupno>? Kupna { get; set; }
        public List<Sprzedaz>? Sprzedaze { get; set; }
        public List<PhotoTowar>? PhotosTowar { get; set; }
    }
}
