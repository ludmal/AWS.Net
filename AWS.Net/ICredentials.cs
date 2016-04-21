using Amazon;

namespace AWS.Net
{
    public interface ICredentials
    {
        string Key { get; set; }
        string Secret { get; set; }
        RegionEndpoint RegionEndpoint { get; set; }
    }
}