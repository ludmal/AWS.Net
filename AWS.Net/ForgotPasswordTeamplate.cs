namespace AWS.Net
{
    public class ForgotPasswordTeamplate : EmailMessage
    {
        public ForgotPasswordTeamplate(string username)
        {
            From = "ludmal@gmail.com";
            To = "ludmal@gmail.com";
            Subject = "Forgot Password";
            TemplateFileName = "forgot_password.html";
            this.FieldList.Add(new EmailFieldValue("NAME", username));
        }
    }
}