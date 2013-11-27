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

        [TestMethod]
        public void SimpleExceptionMessageInSubject()
        {
            ErrorNotification notification = new ErrorNotification("test", new Exception("some exception"));
            Assert.AreEqual("test(some exception)", notification.Subject);
        }

        [TestMethod]
        public void InnermostExceptionInSubject()
        {
            ErrorNotification notification = new ErrorNotification("test", complexException);
            Assert.AreEqual("test(Innermost)", notification.Subject);
        }

        [TestMethod]
        public void ExceptionMessagesInBody()
        {
            ErrorNotification notification = new ErrorNotification("test", complexException);
            StringAssert.Contains(notification.Body, "Outmost");
            StringAssert.Contains(notification.Body, "MiddleOne");
            StringAssert.Contains(notification.Body, "Innermost");
        }

        [TestMethod]
        public void ErrorMessagesInReverseOrderInBody()
        {
            ErrorNotification notification = new ErrorNotification("test", complexException);
            Assert.IsTrue(notification.Body.IndexOf("Innermost") < notification.Body.IndexOf("MiddleOne"));
            Assert.IsTrue(notification.Body.IndexOf("MiddleOne") < notification.Body.IndexOf("Outmost"));
        }
    }
}
