using Domain.Models;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels.Kupna
{
    public class KupnoViewModel
    {
        public string Imie_DaneOsobowe { get; set; }
        public string Nazwisko_DaneOsobowe { get; set; }
        public string Telefon_DaneOsobowe { get; set; }
        public string Ulica_DaneOsobowe { get; set; }
        public string NumerUlicy_DaneOsobowe { get; set; }
        public string Miejscowosc_DaneOsobowe { get; set; }
        public string Pesel_DaneOsobowe { get; set; }
        public string Kraj_DaneOsobowe { get; set; }
        public string KodPocztowy_DaneOsobowe { get; set; }
        public DateTime DataUrodzenia_DaneOsobowe { get; set; }
        public Plec Plec_DaneOsobowe { get; set; }
        public string Email_DaneOsobowe { get; set; }
        public DateTime DataDodania_DaneOsobowe { get; set; }
        public RodzajTransakcji RodzajTransakcji_DaneOsobowe { get; set; }


        public Kupno Kupno { get; set; }


        public List<IFormFile> Files { get; set; }
        public bool Success { get; set; }
        public string Result { get; set; }
    }
}
