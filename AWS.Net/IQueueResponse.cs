using System.Net;

namespace AWS.Net
{
    public interface IQueueResponse
    {
        string MessageId { get; set; }
        HttpStatusCode HttpStatusCode { get; set; }
    }
}