using System.Net;
using Amazon.SQS.Model;

namespace AWS.Net
{
    public class SqsResponse : IQueueResponse
    {
        public string MessageId { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}