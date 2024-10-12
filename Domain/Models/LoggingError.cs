using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class LoggingError
    {
        [Key]
        public string LoggingErrorId { get; set; }
        public string Controller { get; set; }
        public string Method { get; set; }
        public string Message { get; set; }
        public string DataUtworzenia { get; set; }



        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
