using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SendMail
{
    [TestClass]
    public class ErrorNotificationTests
    {
        private MockEmailServer mockEmailServer;
        private const string OperationName = "Grips import";
        private const string TestMessage = "test message";
        private const string ExceptionMessage = "Exception message";

        [TestInitialize]
        public void Init()
        {
            mockEmailServer = new MockEmailServer();
            EmailServerLocator.EmailServer = mockEmailServer;
        }

        [TestMethod]
        public void Send_CallsEmailServerSend()
        {
            ErrorNotification notification = new ErrorNotification(OperationName, TestMessage, new Exception(ExceptionMessage));
            notification.Send();

            Assert.IsTrue(mockEmailServer.SendWasCalled);
        }

        [TestMethod]
        public void Send_IncludesTheExceptionMessageInTheBody()
        {
            Exception exception = new Exception(ExceptionMessage);
            ErrorNotification notification = new ErrorNotification(OperationName, TestMessage, exception);
            notification.Send();

            Assert.IsTrue(mockEmailServer.Body.IndexOf(exception.Message, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [TestMethod]
        public void Send_IncludesTheMesageAsTheSubject()
        {
            const string message = TestMessage;
            ErrorNotification notification = new ErrorNotification(OperationName, message, new Exception(ExceptionMessage));
            notification.Send();

            Assert.IsTrue(mockEmailServer.Subject.IndexOf(message, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        [TestMethod]
        public void Send_SendsTheEmailToTheReceiverSpecifiedInTheConfigFile()
        {
            const string message = TestMessage;
            ErrorNotification notification = new ErrorNotification(OperationName, message, new Exception(ExceptionMessage));
            notification.Send();

            string receiver = ConfigurationManager.AppSettings["ErrorNotificationsReceivers"];
            Assert.AreEqual(receiver, mockEmailServer.To);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Send_SuccesfullySendAnEmailMessage()
        {
            EmailServerLocator.EmailServer = new EmailServer();

            ErrorNotification notification = new ErrorNotification(OperationName, TestMessage, new Exception(ExceptionMessage));
            notification.Send();
        }

    }
}
