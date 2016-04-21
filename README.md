AWS.Net - Simple library for AWS services. i.e. S3, SQS & SES
==============================================================

SQS - Simple Queue Service 
--------------------------
```javascript
var service = new SqsService<EmailMessage>(new AwsCredentials { RegionEndpoint = RegionEndpoint.USWest2 });
service.QueueUrl = ConfigurationManager.AppSettings["EmailQueue"];
var response = service.Push(new HelloEmail());
```
