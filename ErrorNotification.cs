using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace SendMail
{
    public class ErrorNotification
    {
        private readonly string subject;
        private readonly string body;

        private readonly List<Exception> exceptions = new List<Exception>();

        public ErrorNotification(string subject, Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException("exception");

            CollectExceptions(exception);

            this.subject = subject + "(" + exceptions.Last().Message + ")";

            body +=
                "Error messages: " + Environment.NewLine +
                GetErrorMessages() + Environment.NewLine +
                "Stack trace: " + Environment.NewLine +
                GetStackTrace();
        }

        private void CollectExceptions(Exception exception)
        {
            while (exception != null)
            {
                exceptions.Add(exception);
                exception = exception.InnerException;
            }
        }

        private string GetErrorMessages()
        {
            // Error messages are included in reverse order:
            // The innermos message (the one which caused the error) is first
            return exceptions.Select(e => e.Message).Reverse().Aggregate((i, j) => i + Environment.NewLine + j);
        }

        private string GetStackTrace()
        {
            return exceptions.Select(e => e.StackTrace).Aggregate((i, j) => i + Environment.NewLine + j);
        }

        public MailMessage GetMailMessage()
        {
            MailMessage message = new MailMessage();
            string to = ConfigurationManager.AppSettings["ErrorNotificationsReceivers"];
            message.To.Add(to);

            // Seemingly having \r and \n in the subject breaks MailMessage
            // Unfortunately it's hard to find out what other validations are done
            message.Subject = subject.Replace(Environment.NewLine, " ");
            message.SubjectEncoding = Encoding.UTF8;

            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;

            message.Priority = MailPriority.High;

            return message;
        }
    }
}