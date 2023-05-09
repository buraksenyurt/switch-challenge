using BusinessApp.Model;
using BusinessApp.Shared;

namespace BusinessApp.Contracts{
    public interface ITypedProcessContract{
        ReturnMessage<Customer> Apply(Customer source);
    }
}