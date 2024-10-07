using Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DaneOsobowe
    {
        [Key]
        public string DaneOsoboweId { get; set; }

        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string DataUrodzenia { get; set; }
        public string Pesel { get; set; }
        [Required]
        public string Ulica { get; set; }
        [Required]
        public string NumerUlicy { get; set; }
        [Required]
        public string Kraj { get; set; }
        [Required]
        public string Powiat { get; set; }
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
    }
}
