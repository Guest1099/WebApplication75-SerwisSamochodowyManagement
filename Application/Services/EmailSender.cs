using Application.Services.Abs;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private string apiKeyBrevo = "";

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            apiKeyBrevo = _configuration["ApiKeyBrevo"].ToString();
        }


        public void Send(string email)
        {
             
        }




        public void Send(string email, string title, string htmlBody)
        {
           
        }


    }
}
