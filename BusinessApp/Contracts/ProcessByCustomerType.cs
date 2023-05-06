using BusinessApp.Model;
using BusinessApp.Shared;

namespace BusinessApp.Contracts{
    public class ProcessByCustomerType
        : IProcessContract
    {
        public ReturnMessage<Customer> ApplyState(Customer source)
        {
            throw new NotImplementedException();
        }
    }
}