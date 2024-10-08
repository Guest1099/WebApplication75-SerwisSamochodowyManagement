using Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    /// <summary>
    /// Dane osobowe przypisane są do klasy Client oraz Owner, po to aby zmniejszyć powtarzalność kodu
    /// </summary>
    public class DaneOsobowe
    {
        [Key]
        public string DaneOsoboweId { get; set; }


        // dane personalne

        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        [Required]
        public string Ulica { get; set; }
        [Required]
        public string NumerUlicy { get; set; }
        [Required]
        public string Miejscowosc { get; set; }
        public string KodPocztowy { get; set; }
        [Required]
        public string Powiat { get; set; }
        [Required]
        public string Kraj { get; set; }
        [Required]
        public string Pesel { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string DataUrodzenia { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public Plec Plec { get; set; }
        public RodzajOsoby RodzajOsoby { get; set; }



        // dane firmy
        public string? Firma_Nazwa { get; set; }
        public string? Firma_NIP { get; set; }
        public string? Firma_Regon { get; set; }
        public string? Firma_Ulica { get; set; }
        public string? Firma_NumerUlicy { get; set; }
        public string? Firma_Miejscowosc { get; set; }
        public string? Firma_KodPocztowy { get; set; }
        public string? Firma_Powiat { get; set; }
        public string? Firma_Kraj { get; set; }



        public string DataDodania { get; set; }




        public List<Owner>? Owners { get; set; }
        public List<Client>? Clients { get; set; }
        public List<PhotoDaneOsobowe>? PhotosDaneOsobowe { get; set; }
    }
}
