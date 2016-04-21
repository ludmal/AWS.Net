using System.Configuration;
using System.Linq;
using System.Net;
using Amazon;
using Amazon.S3;
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
            });

            service.QueueUrl = ConfigurationManager.AppSettings["EmailQueue"];

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
            }, "bucketname"), new AwsCredentials
            {
                RegionEndpoint = RegionEndpoint.USWest2
            });

            service.Send(new HelloEmail());
        }

        [TestMethod]
        public void UploadToS3()
        {
            var service = new S3Service(new AwsCredentials
            {
                //S3 Service Region
                RegionEndpoint = RegionEndpoint.USEast1
            }, "bucketname");

            var content = service.Download("myfile.jpg");
        }
    }

    public class HelloEmail : EmailMessage
    {
        public HelloEmail()
        {
            Subject = "Hello world";
            //Email template file Url in the S3 bucket
            TemplateFileName = "emails/test.html";
            From = "hello@ludmal.com";
            To = "ludmal@gmail.com";
            //Field values to replace in the template
            AddFields("NAME", "ludmal");
        }
    }
}