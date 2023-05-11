using BusinessApp.IoC;
using BusinessApp.Model;
using BusinessApp.Shared;

namespace BusinessApp.Contracts
{
    public class ProcessByCustomerType
        : BaseContract, IContract
    {
        public ProcessByCustomerType(IPublisher publisher)
            : base(publisher)
        {
        }

        public ReturnMessage<Customer> Apply(Customer customer)
        {
            var instace = Resolver.GetContract(customer.CustomerType);
            return instace.Apply(customer);

            // ReturnMessage<Customer> rm = new ReturnMessage<Customer>();
            // switch (customer.CustomerType)
            // {
            //     case CustomerType.Gold:
            //     case CustomerType.Platinium:
            //     case CustomerType.Basic:

            //         _publisher.Send(customer.Email, string.Empty);
            //         _publisher.Send(customer.Owner.Email, $"{customer.Title.LastName}. Özel müşterimiz var.");

            //         break;
            //     case CustomerType.Newbee:

            //         _publisher.Send(customer.Email, string.Empty);
            //         break;
            // }
            // rm.Payload = customer;
            // return rm;
        }
    }
}