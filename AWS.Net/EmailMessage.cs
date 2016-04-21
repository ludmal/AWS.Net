namespace AWS.Net
{
    public class EmailMessage : IMailMessage
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string TemplateFileName { get; set; }

        public EmailFieldValueCollection FieldList { get; set; }

        public EmailMessage()
        {
            this.From = string.Empty;
            this.To = string.Empty;
            this.Subject = string.Empty;
            this.Body = string.Empty;
            this.TemplateFileName = string.Empty;
            this.FieldList = new EmailFieldValueCollection();
        }

        public void AddFields(string key, string value)
        {
            this.FieldList.Add(new EmailFieldValue(key, value));
        }

        public EmailMessage(string to, string subject, string body)
            : this(string.Empty, to, subject, body)
        {
        }

        public EmailMessage(string from, string to, string subject, string body)
        {
            this.From = from;
            this.To = to;
            this.Subject = subject;
            this.Body = body;
            this.TemplateFileName = string.Empty;
            this.FieldList = new EmailFieldValueCollection();
        }
    }
}