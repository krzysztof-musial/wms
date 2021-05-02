using System.Net;
using System.Net.Mail;

namespace WMS.UserManagement.Model.Notifications
{
    public class EmailConfiguration : IEmailConfiguration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public SmtpClient GetSmtpClient()
        {
            return new SmtpClient
            {
                Credentials = new NetworkCredential(Username, Password),
                Host = Host,
                Port = Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };
        }
    }
}
