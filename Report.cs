namespace SendMail
{
    class Report : Notification
    {
        private string subject;

        public Report(string reportMailSubject)
        {
            subject = reportMailSubject;
        }

        public override string Title
        {
            get { return subject; }
        }

        public override string Summary
        {
            get { return "GripsImportSummary"; }
        }

        public override string Content
        {
            get { return "GripsImportContent"; }
        }
    }
}