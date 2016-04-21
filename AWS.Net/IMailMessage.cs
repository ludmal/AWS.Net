namespace AWS.Net
{
    public interface IMailMessage
    {
        string To { get; set; }
        string From { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        string TemplateFileName { get; set; }

        EmailFieldValueCollection FieldList { get; set; }
    }
}