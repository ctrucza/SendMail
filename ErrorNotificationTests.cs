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
        public void SimpleExceptionMessageInTitle()
        {
            ErrorNotification errorNotification = new ErrorNotification("test", new Exception("some exception"));
            Assert.AreEqual("test(some exception)", errorNotification.Title);
        }

        [TestMethod]
        public void InnermostExceptionInTitle()
        {
            ErrorNotification errorNotification = new ErrorNotification("test", complexException);
            Assert.AreEqual("test(Innermost)", errorNotification.Title);
        }

        [TestMethod]
        public void ErrorMessagesInSummary()
        {
            ErrorNotification errorNotification = new ErrorNotification("test", complexException);
            string body = errorNotification.Summary;
            StringAssert.Contains(body, "Outmost");
            StringAssert.Contains(body, "MiddleOne");
            StringAssert.Contains(body, "Innermost");
        }

        [TestMethod]
        public void ErrorMessagesInReverseOrderInSummary()
        {
            ErrorNotification errorNotification = new ErrorNotification("test", complexException);
            string body = errorNotification.Summary;
            Assert.IsTrue(body.IndexOf("Innermost") < body.IndexOf("MiddleOne"));
            Assert.IsTrue(body.IndexOf("MiddleOne") < body.IndexOf("Outmost"));
        }
    }
}