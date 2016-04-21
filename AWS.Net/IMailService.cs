namespace AWS.Net
{
    public interface IMailService
    {
        void Send(IMailMessage message);
    }
}