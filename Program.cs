using System;

namespace SendMail
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DoStuff();
            }
            catch (Exception e)
            {
                // Use System.Net.Mail to send an email:
                // 
                // From: service@company.com
                // To: admin@company.com
                // Subject: [SERVICE] Service failed: <root cause for error>
                // Body:
                //  Error messages:
                //      <root cause for error>
                //      <one level up>
                //      ...
                //
                //  Stack trace:
                //  <outermost stack trace>
                //  <next stack trace>
                //  ...
                //  <stack trace for root cause>
            }
        }

        private static void DoStuff()
        {
            // Do stuff here which can throw
        }
    }
}
