using System;
using System.Configuration;
using System.Net.Mail;

namespace SendMail
{
    class Program
    {
        private static string errorMailSubject;

        static void Main(string[] args)
        {
            errorMailSubject = ConfigurationManager.AppSettings["ErrorMailSubject"];
            try
            {
                DoStuff();
            }
            catch (Exception cause)
            {
                ErrorNotification errorNotification = new ErrorNotification(errorMailSubject, cause);
                MailMessage mailMessage = errorNotification.GetMailMessage();
                EmailServer emailServer = new EmailServer();
                emailServer.SendMail(mailMessage);
            }
        }

        private static void DoStuff()
        {
            // Do stuff here which can throw
        }
    }
}
