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
 //SQS Service Region
 RegionEndpoint = RegionEndpoint.USWest2
});

//SQS Queue Url
service.QueueUrl = ConfigurationManager.AppSettings["EmailQueue"];

var items = service.Process();
```

SES - Simple Email Services
---------------------------
> SES uses S3 bucket to store email templates

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

> Example email HTML template 

```html
<html>
  <body>
    <div>
      Hello [NAME]
    </div>
  </body>
<html>
```

Send email using **SesService** class
```javascript
var service = new SesService(new S3Service(new AwsCredentials
{
  //SES Service Region
  RegionEndpoint = RegionEndpoint.USEast1
}, "bucketname"), new AwsCredentials
{
  //S3 Service Region
  RegionEndpoint = RegionEndpoint.USWest2
});

service.Send(new HelloEmail());
```
