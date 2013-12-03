namespace SendMail
{
    public class Notification
    {
        public virtual string Title { get { return "[title]"; } }
        public virtual string Summary { get { return "[summary]"; } }
        public virtual string Content { get { return "[content]"; } }

    }
}