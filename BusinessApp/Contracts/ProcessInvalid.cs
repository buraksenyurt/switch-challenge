using BusinessApp.Model;
using BusinessApp.Shared;
using BusinessApp.Shared.Services;

namespace BusinessApp.Contracts
{
    public class ProcessInvalid
        : IProcessContract
    {
        private readonly IPublisher _publisher;

        public ProcessInvalid(IPublisher publisher)
        {
            _publisher=publisher;
        }
        public ReturnMessage<Customer> Apply(Customer customer)
        {
            ReturnMessage<Customer> rm=new ReturnMessage<Customer>();
            customer.CalculatedBonus.IsActive = false;
            customer.CalculatedBonus.BonusValidationDate = DateTime.MinValue;
            customer.CalculatedBonus.Value = 0;
            rm.ReturnCode = ReturnCode.Unsuccess;
            rm.Payload=customer;

            _publisher.Send(customer.Email,"Müşteri ödeme ve limitleri düzensiz ya da incelemede.");

            return rm;
        }
    }
}