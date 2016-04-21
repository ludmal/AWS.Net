using System.Collections.Generic;

namespace AWS.Net
{
    public interface IQueueService<T>
    {
        string QueueUrl { get; set; }
        IQueueResponse Push(T model);
        IList<T> Process();
    }
}