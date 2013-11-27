using System;
using System.Configuration;

namespace SendMail
{
    class Program
    {
        private static string errorMailSubjectPrefix;

        static void Main(string[] args)
        {
            errorMailSubjectPrefix = ConfigurationManager.AppSettings["ErrorMailSubjectPrefix"];

            try
            {
                DoStuff();
            }
            catch (Exception e)
            {
                string message = string.Format(
                    "{0} Service stopped: {1}",
                    errorMailSubjectPrefix,
                    e.Message
                    );

                EmailServerLocator.EmailServer = new EmailServer();
                ErrorNotification errorNotification = new ErrorNotification("Grips import", message, e);
                errorNotification.Send();
            }
        }

        private static void DoStuff()
        {
            // Do stuff here which can throw
        }
    }
}
