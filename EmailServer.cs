using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace SendMail
{
    public class EmailServer
    {
        public void Send(Notification notification)
        {
            using (MailMessage mailMessage = CreateMailMessage(notification))
            using (SmtpClient client = new SmtpClient())
            {
                client.Send(mailMessage);
            }
        }

        private MailMessage CreateMailMessage(Notification notification)
        {
            MailMessage message = new MailMessage();
            string to = ConfigurationManager.AppSettings["ErrorNotificationsReceivers"];
            message.To.Add(to);

            // Seemingly having \r and \n in the subject breaks MailMessage
            // Unfortunately it's hard to find out what other validations are done
            message.Subject = notification.Title.Replace(Environment.NewLine, " ");
            message.SubjectEncoding = Encoding.UTF8;

            message.Body = notification.Summary + Environment.NewLine + notification.Content;
            message.BodyEncoding = Encoding.UTF8;

            message.Priority = MailPriority.High;

            return message;
        }
    }
}