using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

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

        public string Subject
        {
            get { return subject; }
        }

        public string Body
        {
            get { return body; }
        }

        private string GetErrorMessages()
        {
            return exceptions.Select(e => e.Message).Reverse().Aggregate((i, j) => i + Environment.NewLine + j);
        }

        private string GetStackTrace()
        {
            return exceptions.Select(e => e.StackTrace).Aggregate((i, j) => i + Environment.NewLine + j);
        }

        public void Send()
        {
            EmailServer emailServer = new EmailServer();
            string recipientAddresses = ConfigurationManager.AppSettings["ErrorNotificationsReceivers"];
            emailServer.Send(recipientAddresses, subject, body);
        }
    }
}