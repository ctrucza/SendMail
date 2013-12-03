using System;
using System.Configuration;

namespace SendMail
{
    class Program
    {
        static void Main(string[] args)
        {
            string errorMailSubject = ConfigurationManager.AppSettings["ErrorMailSubject"];
            string reportMailSubject = ConfigurationManager.AppSettings["ReportMailSubject"]; ;
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
            finally
            {
                Report report = new Report(reportMailSubject);
                EmailServer emailServer = new EmailServer();
                emailServer.Send(report);
            }
        }

        private static void DoStuff()
        {
            // Do stuff here which can throw
        }
    }
}
