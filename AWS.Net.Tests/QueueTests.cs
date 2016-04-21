using System.Configuration;
using System.Linq;
using System.Net;
using Amazon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AWS.Net.Tests
{
    [TestClass]
    public class QueueTests
    {
        [TestMethod]
        public void PostToQueue()
        {
            var service = new SqsService<EmailMessage>(new AwsCredentials
            {
                RegionEndpoint = RegionEndpoint.USWest2
            }) {QueueUrl = ConfigurationManager.AppSettings["EmailQueue"]};

            var response = service.Push(new HelloEmail());

            Assert.IsTrue(response.HttpStatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void ProcessQueue()
        {
            var service = new SqsService<EmailMessage>(new AwsCredentials
            {
                RegionEndpoint = RegionEndpoint.USWest2
            }) {QueueUrl = ConfigurationManager.AppSettings["EmailQueue"]};

            var items = service.Process();

            Assert.IsTrue(items.Any());
        }

        [TestMethod]
        public void SendEmail()
        {
            var service = new SesService(new S3Service(new AwsCredentials
            {
                RegionEndpoint = RegionEndpoint.USEast1
            }, "redfly.io"), new AwsCredentials
            {
                RegionEndpoint = RegionEndpoint.USWest2
            });
            service.Send(new HelloEmail());
        }
    }

    public class HelloEmail : EmailMessage
    {
        public HelloEmail()
        {
            Subject = "Hello world";
            TemplateFileName = "emails/test.html";
            From = "team@redfly.io";
            To = "ludmal@gmail.com";
            AddFields("NAME", "ludmal");
        }
    }
}