using System;
using System.Collections.Generic;
using System.Linq;

namespace SendMail
{
    public class ErrorNotification
    {
        public readonly string Title;
        public readonly string Summary;
        public readonly string Content;

        private readonly List<Exception> exceptions = new List<Exception>();

        public ErrorNotification(string message, Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException("exception");

            CollectExceptions(exception);

            Title = message + "(" + exceptions.Last().Message + ")";
            Summary = 
                "Error messages: " + Environment.NewLine + 
                GetErrorMessages() + Environment.NewLine;
            Content = 
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
    }
}