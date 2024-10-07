using Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Client
    {
        [Key]
        public string ClientId { get; set; }



        

        public List<Owner>? Owner { get; set; }
        public List<Kupno>? Kupna { get; set; }
        public List<Sprzedaz>? Sprzedaze { get; set; }
    }
}
