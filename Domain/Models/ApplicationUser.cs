using Domain.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Telefon { get; set; }
        public string Ulica { get; set; }
        public string NumerUlicy { get; set; }
        public string Miejscowosc { get; set; }
        public string Kraj { get; set; }
        public string KodPocztowy { get; set; }
        public string DataUrodzenia { get; set; }
        public Plec Plec { get; set; }
        public bool Newsletter { get; set; }
        public int IloscZalogowan { get; set; }
        public string DataOstatniegoZalogowania { get; set; }
        public string DataDodania { get; set; }




        public List<PhotoUser>? PhotosUser { get; set; }
        public List<LoggingError>? LoggingErrors { get; set; }
    }
}
