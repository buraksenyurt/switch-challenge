using BusinessApp.Model;
using BusinessApp.Shared;

namespace BusinessApp.Contracts
{
    public class ProcessSecondStage
        : BaseContract, ITypedProcessContract
    {
        public ProcessSecondStage(IPublisher publisher)
            : base(publisher)
        {
        }
        public ReturnMessage<Customer> Apply(Customer customer)
        {
            ReturnMessage<Customer> rm = new ReturnMessage<Customer>();
            customer.CalculatedBonus.IsActive = true;
            customer.CalculatedBonus.BonusValidationDate = DateTime.Now.AddDays(7);
            customer.CalculatedBonus.Value = 100;
            rm.ReturnCode = ReturnCode.Unsuccess;
            rm.Payload = customer;
            _publisher.Send(customer.Email, string.Empty);
            return rm;
        }
    }
}