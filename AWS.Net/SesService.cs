using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace AWS.Net
{
    public class SesService
    {
        private readonly AwsCredentials _credentials;
        private readonly S3Service _s3Service;

        public SesService(S3Service s3Service, AwsCredentials credentials)
        {
            _s3Service = s3Service;
            _credentials = credentials;
        }

        public void Send(EmailMessage message)
        {
            var body = string.Empty;

            var response = _s3Service.Download(message.TemplateFileName);

            using (var reader = new StreamReader(response.ResponseStream))
            {
                body = reader.ReadToEnd();
            }

            body = message.FieldList.Render(body);
            var destination = new Destination();

            var emailOverride = ConfigurationManager.AppSettings["EmailOverride"] ?? "";

            char[] c = {';'};

            if (!string.IsNullOrEmpty(emailOverride))
            {
                var emailList = emailOverride.Split(c).ToList();
                emailList.ForEach(x => destination.ToAddresses.Add(x));
            }
            else
            {
                destination.ToAddresses.Add(message.To);
            }
            var emailSubject = new Content(message.Subject);
            var textBody = new Content(body);
            var emailBody = new Body {Html = textBody};
            var msg = new Message(emailSubject, emailBody);

            var request = new SendEmailRequest(message.From, destination, msg);

            var client = new AmazonSimpleEmailServiceClient(_credentials.Key, _credentials.Secret,
                _credentials.RegionEndpoint);

            var sendResult = client.SendEmail(request);

            if (sendResult.HttpStatusCode != HttpStatusCode.OK)
            {
                //Error
            }
        }
    }
}