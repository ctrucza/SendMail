using System;
using System.Net.Mail;
using System.Text;

namespace SendMail
{
    public class EmailServer
    {
        public void Send(string to, string subject, string body)
        {
            using (SmtpClient client = new SmtpClient())
            {
                using (MailMessage message = new MailMessage())
                {
                    message.To.Add(to);

                    // Seemingly having \r and \n in the subject breaks MailMessage
                    // Unfortunately it's hard to find out what other validations are done
                    message.Subject = CleanupSubject(subject);
                    message.SubjectEncoding = Encoding.UTF8;

                    message.Body = body;
                    message.BodyEncoding = Encoding.UTF8;

                    message.Priority = MailPriority.High;

                    client.Send(message);
                }
            }
        }

        private string CleanupSubject(string subject)
        {
            return subject.Replace(Environment.NewLine, " ");
        }
    }
}