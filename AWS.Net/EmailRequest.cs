using System.Collections.Generic;

namespace AWS.Net
{
    public class EmailRequest
    {
        public Dictionary<string, string> Data { get; set; }
        public string To { get; set; }
        public string TypeName { get; set; }
    }
}