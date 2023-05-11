using BusinessApp.Model;
using BusinessApp.Shared;

namespace BusinessApp.Contracts
{
    public class ProcessInvalid
        : BaseContract, IContract
    {
        public ProcessInvalid(IPublisher publisher)
            : base(publisher)
        {
        }
        public ReturnMessage<Customer> Apply(Customer customer)
        {
            ReturnMessage<Customer> rm = new ReturnMessage<Customer>();
            customer.CalculatedBonus.IsActive = false;
            customer.CalculatedBonus.BonusValidationDate = DateTime.MinValue;
            customer.CalculatedBonus.Value = 0;
            rm.ReturnCode = ReturnCode.Unsuccess;
            rm.Payload = customer;

            _publisher.Send(customer.Email, "Müşteri ödeme ve limitleri düzensiz ya da incelemede.");

            return rm;
        }
    }
}