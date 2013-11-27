using System;

namespace SendMail
{
    public static class EmailServerLocator
    {
        [ThreadStatic]
        private static EmailServer emailServer;

        public static EmailServer EmailServer
        {
            get { return emailServer; }
            set { emailServer = value; }
        }
    }
}