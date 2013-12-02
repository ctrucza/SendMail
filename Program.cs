using System;
using System.Configuration;

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
                EmailServer emailServer = new EmailServer();
                emailServer.Send(errorNotification);
            }
        }

        private static void DoStuff()
        {
            // Do stuff here which can throw
        }
    }
}
