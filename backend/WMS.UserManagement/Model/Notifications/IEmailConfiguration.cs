using System.Net.Mail;

namespace WMS.UserManagement.Model.Notifications
{
    public interface IEmailConfiguration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public SmtpClient GetSmtpClient();
    }
}
