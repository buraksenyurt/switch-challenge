using BusinessApp.Model;
using BusinessApp.Shared;

namespace BusinessApp.Contracts{
    public interface IProcessContract{
        ReturnMessage<Customer> ApplyState(Customer source);
    }
}