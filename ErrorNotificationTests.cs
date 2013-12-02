using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SendMail
{
    [TestClass]
    public class ErrorNotificationTests
    {
        private readonly Exception complexException = new Exception(
            "Outmost",
            new Exception(
                "MiddleOne",
                new Exception("Innermost")
                )
            );

        private readonly EmailServer server = new EmailServer();

        [TestMethod]
        public void SimpleExceptionMessageInSubject()
        {
            ErrorNotification errorNotification = new ErrorNotification("test", new Exception("some exception"));
            Assert.AreEqual("test(some exception)", errorNotification.Title);
        }

        [TestMethod]
        public void InnermostExceptionInSubject()
        {
            ErrorNotification errorNotification = new ErrorNotification("test", complexException);
            Assert.AreEqual("test(Innermost)", errorNotification.Title);
        }

        [TestMethod]
        public void ExceptionMessagesInBody()
        {
            ErrorNotification errorNotification = new ErrorNotification("test", complexException);
            string body = errorNotification.Summary;
            StringAssert.Contains(body, "Outmost");
            StringAssert.Contains(body, "MiddleOne");
            StringAssert.Contains(body, "Innermost");
        }

        [TestMethod]
        public void ErrorMessagesInReverseOrderInBody()
        {
            ErrorNotification errorNotification = new ErrorNotification("test", complexException);
            string body = errorNotification.Summary;
            Assert.IsTrue(body.IndexOf("Innermost") < body.IndexOf("MiddleOne"));
            Assert.IsTrue(body.IndexOf("MiddleOne") < body.IndexOf("Outmost"));
        }

        [TestMethod]
        public void ExceptionStackTraceInBody()
        {
            try
            {
                // this is just for showing off...
                new Action(() => new Action(() => new Action(() =>
                {
                    throw new Exception("FOO");
                })())())();
            }
            catch (Exception e)
            {
                ErrorNotification n = new ErrorNotification("test", e);
                StringAssert.Contains(n.Content, e.StackTrace);
            }
        }
    }
}