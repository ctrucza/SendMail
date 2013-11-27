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
            catch (Exception e)
            {
                SendErrorMail(e);
            }
        }

        private static void SendErrorMail(Exception cause)
        {
            ErrorNotification errorNotification = new ErrorNotification(errorMailSubject, cause);
            errorNotification.Send();
        }

        private static void DoStuff()
        {
            // Do stuff here which can throw
        }
    }
}
