using System.Net.Mail;

namespace SendMail
{
    public class EmailServer
    {
        public void SendMail(MailMessage mailMessage)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Send(mailMessage);
            }
        }
    }
}