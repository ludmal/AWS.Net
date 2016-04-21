using System.Collections.Generic;
using System.Linq;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;

namespace AWS.Net
{
    public class SqsReciever<T>
    {
        private readonly AwsCredentails _credentails;
        private readonly string _queueUrl;
        private readonly List<T> _list = new List<T>();

        public SqsReciever(AwsCredentails credentails, string queueUrl)
        {
            _credentails = credentails;
            _queueUrl = queueUrl;
        }

        public IList<T> Process()
        {
            var sqs = new AmazonSQSClient(_credentails.Key, _credentails.Secret, _credentails.RegionEndpoint);

            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = _queueUrl,
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
                    QueueUrl = _queueUrl
                });
                _list.Add(request);
            }

            return _list;
        }
    }
}