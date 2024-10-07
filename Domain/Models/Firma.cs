using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Firma
    {
        [Key]
        public string FirmaId { get; set; }
        public string NazwaFirmy { get; set; }
        public string NIP { get; set; }
        public string Regon { get; set; }
        public string Kraj { get; set; }
        public string Powiat { get; set; }
        public string Miasto { get; set; }
        public string Ulica { get; set; }
        public string NumerUlicy { get; set; }
        


        public string? WlascicielId { get; set; }
        public DaneOsobowe? Wlasciciel { get; set; }

    }
}
