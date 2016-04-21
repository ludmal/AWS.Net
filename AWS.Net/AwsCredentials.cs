using System.Configuration;
using Amazon;

namespace AWS.Net
{
    public class AwsCredentials
    {
        private string _key;
        private string _secret;

        public string Key
        {
            get
            {
                _key = ConfigurationManager.AppSettings["AWSAccessKey"] ?? "";
                return _key;
            }
            set { _key = value; }
        }

        public RegionEndpoint RegionEndpoint { get; set; }

        public string Secret
        {
            get
            {
                _secret = ConfigurationManager.AppSettings["AWSSecretKey"] ?? "";
                return _secret;
            }
            set { _secret = value; }
        }
    }
}