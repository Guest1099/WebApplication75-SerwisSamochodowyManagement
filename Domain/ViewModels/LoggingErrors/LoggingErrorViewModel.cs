using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels.LoggingErrors
{
    public class LoggingErrorViewModel
    {
        public LoggingError LoggingError { get; set; }



        public List<IFormFile> Files { get; set; }
        public bool Success { get; set; }
        public string Result { get; set; }
    }
}
