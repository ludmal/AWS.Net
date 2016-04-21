AWS.Net - Simple library for AWS services. i.e. S3, SQS & SES
==============================================================

SQS - Simple Queue Service 
--------------------------

Sending **T object** to Queue
```javascript
var service = new SqsService<EmailMessage>(new AwsCredentials
{
  RegionEndpoint = RegionEndpoint.USWest2
});

service.QueueUrl = ConfigurationManager.AppSettings["EmailQueue"];

var response = service.Push(new HelloEmail());
```

Receiving **T object list** from Queue
```javascript
var service = new SqsService<EmailMessage>(new AwsCredentials
{
 RegionEndpoint = RegionEndpoint.USWest2
});

service.QueueUrl = ConfigurationManager.AppSettings["EmailQueue"];

var items = service.Process();
```

SES - Simple Email Services
---------------------------

Create your own email message using **EmailMessage** class
```javascript
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
```
