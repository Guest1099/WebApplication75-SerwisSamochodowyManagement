using Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Client
    {
        [Key]
        public string ClientId { get; set; }

        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataUrodzenia { get; set; }
        public string Pesel { get; set; }
        [Required]
        public string Kraj { get; set; }
        [Required]
        public string Miasto { get; set; }
        [Required]
        public string Powiat { get; set; }
        [Required]
        public string Ulica { get; set; }
        [Required]
        public string NumerUlicy { get; set; }
        [Required]
        public string KodPocztowy { get; set; }
        [Required]
        public string Miejscowosc { get; set; }
        public Plec Plec { get; set; }
        public RodzajKlienta RodzajKlienta { get; set; }

        public RodzajTransakcji RodzajTransakcji { get; set; }
        public string DataDodania { get; set; }


        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefon { get; set; }


        

        public List<Owner>? Firma { get; set; }
        public List<Kupno>? Kupna { get; set; }
        public List<Sprzedaz>? Sprzedaze { get; set; }
    }
}
