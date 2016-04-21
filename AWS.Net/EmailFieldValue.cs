namespace AWS.Net
{
    public class EmailFieldValue
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }

        public EmailFieldValue(string fieldName, string fieldValue)
        {
            FieldName = string.Format("[{0}]", fieldName);
            FieldValue = fieldValue;
        }

        public EmailFieldValue()
        {
        }
    }
}