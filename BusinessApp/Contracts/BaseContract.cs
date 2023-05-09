namespace BusinessApp.Contracts{
    public abstract class BaseContract{
        protected readonly IPublisher _publisher;

        public BaseContract(IPublisher publisher)
        {
            _publisher=publisher;
        }
    }
}