using System.Configuration;
using System.Linq;
using Amazon;

namespace AWS.Net
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var service = new SqsService<EmailMessage>(new AwsCredentials
            {
                RegionEndpoint = RegionEndpoint.USWest2
            });
            service.QueueUrl = ConfigurationManager.AppSettings["EmailQueue"];
            
            var response = service.Push(new HelloEmail());
            Chalk.Green(string.Format("Message {0} send successfully", response.MessageId));
            

            while (true)
            {
                var items = service.Process();
                Chalk.Green(items.Select(x => x.Subject).ToString());
                System.Threading.Thread.Sleep(3000);
            }
        }
    }

}