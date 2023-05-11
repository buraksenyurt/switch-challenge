using BusinessApp.Model;
using BusinessApp.Shared;

namespace BusinessApp.Contracts
{

    public interface IContract
    {
        ReturnMessage<Customer> Apply(Customer source);
    }
}