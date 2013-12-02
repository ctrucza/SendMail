using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace SendMail
{
    public class EmailServer
    {
        public void Send(ErrorNotification errorNotification)
        {
            using (MailMessage mailMessage = CreateMailMessage(errorNotification))
            using (SmtpClient client = new SmtpClient())
            {
                client.Send(mailMessage);
            }
        }

        public MailMessage CreateMailMessage(ErrorNotification errorNotification)
        {
            MailMessage message = new MailMessage();
            string to = ConfigurationManager.AppSettings["ErrorNotificationsReceivers"];
            message.To.Add(to);

            // Seemingly having \r and \n in the subject breaks MailMessage
            // Unfortunately it's hard to find out what other validations are done
            message.Subject = errorNotification.Title.Replace(Environment.NewLine, " ");
            message.SubjectEncoding = Encoding.UTF8;

            message.Body = errorNotification.Summary + Environment.NewLine + errorNotification.Content;
            message.BodyEncoding = Encoding.UTF8;

            message.Priority = MailPriority.High;

            return message;
        }
    }
}