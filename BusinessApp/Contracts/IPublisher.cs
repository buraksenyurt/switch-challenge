namespace BusinessApp.Contracts
{
    public interface IPublisher
    {
        void Send(string target,string message);
    }
}