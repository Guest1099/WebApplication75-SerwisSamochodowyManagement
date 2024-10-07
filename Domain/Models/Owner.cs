using Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Owner
    {
        [Key]
        public string OwnerId { get; set; }


        // dane personalne

        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string DataUrodzenia { get; set; }
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
        public RodzajKlienta RodzajOsoby { get; set; }
         
        public string DataDodania { get; set; }





        // dane firmy
        public string? Firma_Nazwa { get; set; }
        public string? Firma_NIP { get; set; }
        public string? Firma_Regon { get; set; }
        public string? Firma_Kraj { get; set; }
        public string? Firma_Powiat { get; set; }
        public string? Firma_Miasto { get; set; }
        public string? Firma_Ulica { get; set; }
        public string? Firma_NumerUlicy { get; set; }

    }
}
