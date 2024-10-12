namespace Application.Services.Abs
{
    public interface IEmailSender
    {
        public void Send(string email);
        public void Send(string email, string title, string htmlBody);
    }
}
