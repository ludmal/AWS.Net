using System.Collections.Generic;
using System.Linq;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;

namespace AWS.Net
{
    public class SqsService<T> : IQueueService<T>
    {
        private readonly AwsCredentials _credentials;
        private readonly List<T> _list = new List<T>();

        public SqsService(AwsCredentials credentials)
        {
            _credentials = credentials;
        }

        public IQueueResponse Push(T model)
        {
            var sqs = new AmazonSQSClient(_credentials.Key, _credentials.Secret, _credentials.RegionEndpoint);

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = QueueUrl,
                MessageBody = JsonConvert.SerializeObject(model)
            };
            var response = sqs.SendMessage(sendMessageRequest);
            return new SqsResponse
            {
                HttpStatusCode = response.HttpStatusCode,
                MessageId = response.MessageId
            };
        }

        public string QueueUrl { get; set; }

        public IList<T> Process()
        {
            var sqs = new AmazonSQSClient(_credentials.Key, _credentials.Secret, _credentials.RegionEndpoint);

            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = QueueUrl,
                MaxNumberOfMessages = 10,
                WaitTimeSeconds = 1
            };

            var response = sqs.ReceiveMessageAsync(receiveMessageRequest);

            if (!response.Result.Messages.Any())
            {
                return _list;
            }

            foreach (var message in response.Result.Messages)
            {
                var request = JsonConvert.DeserializeObject<T>(message.Body);
                sqs.DeleteMessage(new DeleteMessageRequest
                {
                    ReceiptHandle = message.ReceiptHandle,
                    QueueUrl = QueueUrl
                });
                _list.Add(request);
            }

            return _list;
        }
    }
}