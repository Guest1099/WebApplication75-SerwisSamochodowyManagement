using Domain.Models;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels.Account
{
    public class UpdateAccountViewModel
    {
        public string UserId { get; set; }

        public string Email { get; set; }



        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        public string Imie { get; set; }


        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        public string Nazwisko { get; set; }


        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        public string Ulica { get; set; }


        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^-?\d+$", ErrorMessage = "Wartość pola musi być liczbą całkowitą.")]
        public string NumerUlicy { get; set; }


        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        public string Miejscowosc { get; set; }


        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        public string Kraj { get; set; }


        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Nieprawidłowy format kodu pocztowego")]
        public string KodPocztowy { get; set; }



        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{3}$|^\d{9}$", ErrorMessage = "*")]
        public string Telefon { get; set; }


        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        public string DataUrodzenia { get; set; }

        public string DataDodania { get; set; }


        public Plec Plec { get; set; }

        public List<PhotoUser> PhotosUser { get; set; }

        public List<IFormFile> Files { get; set; }

        public List<string> SelectedRoles { get; set; } = new List<string>();
        public bool Success { get; set; }
        public string Result { get; set; }
    }
}
