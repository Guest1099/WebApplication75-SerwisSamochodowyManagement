using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class PhotoFirma
    {
        [Key]
        public string PhotoFirmaId { get; set; }
        public byte[] PhotoData { get; set; }


        public string FirmaId { get; set; }
        public Firma Firma { get; set; }

    }
}
