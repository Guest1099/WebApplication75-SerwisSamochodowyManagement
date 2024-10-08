using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels.Clients
{
    public class ClientViewModel
    {

        public Client Client { get; set; }
        public Models.DaneOsobowe DaneOsobowe { get; set; }



        public List<IFormFile> Files { get; set; }
        public bool Success { get; set; }
        public string Result { get; set; }
    }
}
