using System.Net.Mail;
using System.Text;

namespace SendMail
{
    public class EmailServer
    {
        public void Send(string recipientsAddresses, string subject, string emailBody)
        {
            InternalSend(recipientsAddresses, subject, emailBody);
        }

        protected virtual void InternalSend(string recipientsAddresses, string subject, string emailBody)
        {
            using (SmtpClient client = new SmtpClient())
            {
                using (MailMessage message = new MailMessage())
                {
                    message.To.Add(recipientsAddresses);

                    message.Subject = subject;
                    message.SubjectEncoding = Encoding.UTF8;

                    message.Body = emailBody;
                    message.BodyEncoding = Encoding.UTF8;

                    client.Send(message);
                }
            }
        }
    }
}