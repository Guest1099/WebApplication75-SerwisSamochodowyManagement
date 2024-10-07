using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class PhotoOwner
    {
        [Key]
        public string PhotoFirmaId { get; set; }
        public byte[] PhotoData { get; set; }


        public string OwnerId { get; set; }
        public Owner Owner { get; set; }

    }
}
