namespace BusinessApp.Shared
{
    public class ReturnMessage<T>
    {
        public ReturnCode ReturnCode { get; set; }
        public T? Payload { get; set; } = default;
    }
}