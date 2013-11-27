using System;
using System.Configuration;

namespace SendMail
{
    public class ErrorNotification
    {
        private readonly string subject;
        private readonly string body;

        public ErrorNotification(string operationName, string message, Exception exception)
        {
            subject = message;
            string innerExceptions = GetInnerExceptions(exception);
            body = operationName + " stopped. Stack trace:" +
                Environment.NewLine +
                innerExceptions;
        }

        private static string GetInnerExceptions(Exception exception)
        {
            string result = exception.Message + Environment.NewLine + exception.StackTrace;
            if (exception.InnerException != null)
                result += GetInnerExceptions(exception.InnerException);
            return result;
        }

        public void Send()
        {
            EmailServer emailServer = EmailServerLocator.EmailServer;
            string recipientAddresses = ConfigurationManager.AppSettings["ErrorNotificationsReceivers"];
            emailServer.Send(recipientAddresses, subject, body);
        }
    }
}