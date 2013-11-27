namespace SendMail
{
    internal class MockEmailServer: EmailServer
    {
        protected override void InternalSend(string recipientsAddresses, string subject, string emailBody)
        {
            SendWasCalled = true;
            Subject = subject;
            Body = emailBody;
            To = recipientsAddresses;
        }

        internal bool SendWasCalled { get; private set; }
        internal string Subject { get; private set; }
        internal string Body { get; private set; }
        internal string To { get; private set; }
    }
}