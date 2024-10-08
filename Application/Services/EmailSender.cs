namespace Application.Services
{
    /// <summary>
    /// Wysyłanie maili przez Brevo
    /// </summary>
    public class EmailSender //: IEmailSender
    {
        /*private const string apiKey = "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";
        public void Send(string email)
        {
            if (!Configuration.Default.ApiKey.ContainsKey("api-key"))
                Configuration.Default.ApiKey.Add("api-key", "apiKey");

            var apiInstance = new TransactionalEmailsApi();
            string SenderName = "dariuszwacchat";
            string SenderEmail = "dariuszwacchat@gmail.com";

            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
            string ToEmail = "mgmcdeveloper@gmail.com";
            string ToName = "mgmcdeveloper";

            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);

            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);


            string HtmlContent = null;
            string TextContent = "text content";


            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, TextContent, "Subjectttttt ");
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
            }
            catch (Exception e)
            {
                // Debug.WriteLine(e.Message);
            }
        }




        public void Send(string email, string title, string htmlBody)
        {
            if (!Configuration.Default.ApiKey.ContainsKey("api-key"))
                Configuration.Default.ApiKey.Add("api-key", "apiKey");

            var apiInstance = new TransactionalEmailsApi();
            string SenderName = "dariuszwacchat";
            string SenderEmail = "dariuszwacchat@gmail.com";

            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
            string ToEmail = "mgmcdeveloper@gmail.com";
            string ToName = "mgmcdeveloper";

            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);

            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);


            try
            {
                //var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, htmlBody, title);
                var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, htmlBody, null, title);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
            }
            catch (Exception e)
            {
                // Debug.WriteLine(e.Message);
            }
        }*/

    }
}
