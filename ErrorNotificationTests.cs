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
            Assert.AreEqual("test(some exception)", server.CreateMailMessage(errorNotification).Subject);
        }

        [TestMethod]
        public void InnermostExceptionInSubject()
        {
            ErrorNotification errorNotification = new ErrorNotification("test", complexException);
            Assert.AreEqual("test(Innermost)", server.CreateMailMessage(errorNotification).Subject);
        }

        [TestMethod]
        public void ExceptionMessagesInBody()
        {
            ErrorNotification errorNotification = new ErrorNotification("test", complexException);
            string body = server.CreateMailMessage(errorNotification).Body;
            StringAssert.Contains(body, "Outmost");
            StringAssert.Contains(body, "MiddleOne");
            StringAssert.Contains(body, "Innermost");
        }

        [TestMethod]
        public void ErrorMessagesInReverseOrderInBody()
        {
            ErrorNotification errorNotification = new ErrorNotification("test", complexException);
            string body = server.CreateMailMessage(errorNotification).Body;
            Assert.IsTrue(body.IndexOf("Innermost") < body.IndexOf("MiddleOne"));
            Assert.IsTrue(body.IndexOf("MiddleOne") < body.IndexOf("Outmost"));
        }
    }
}