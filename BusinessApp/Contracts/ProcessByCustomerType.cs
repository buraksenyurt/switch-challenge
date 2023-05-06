using BusinessApp.Model;
using BusinessApp.Shared;
using BusinessApp.Shared.Services;

namespace BusinessApp.Contracts
{
    public class ProcessByCustomerType
        : IProcessContract
    {
        private readonly IPublisher _publisher;

        public ProcessByCustomerType(IPublisher publisher)
        {
            _publisher = publisher;
        }

        //PROBLEM: Burada da switch'ten kurtulabilmek lazım
        public ReturnMessage<Customer> Apply(Customer customer)
        {
            ReturnMessage<Customer> rm = new ReturnMessage<Customer>();
            switch (customer.CustomerType)
            {
                case CustomerType.Gold:
                case CustomerType.Platinium:
                case CustomerType.Basic:
                    customer.CalculatedBonus.IsActive = true;
                    customer.CalculatedBonus.BonusValidationDate = DateTime.Now.AddMonths(1);
                    customer.CalculatedBonus.Value = 990.99M;
                    _publisher.Send(customer.Email, string.Empty);
                    _publisher.Send(customer.Owner.Email, $"{customer.Title.LastName}. Özel müşterimiz var.");
                    rm.ReturnCode = ReturnCode.Success;
                    break;
                case CustomerType.Newbee:
                    customer.CalculatedBonus.IsActive = true;
                    customer.CalculatedBonus.BonusValidationDate = DateTime.Now.AddDays(7);
                    customer.CalculatedBonus.Value = 100;
                    rm.ReturnCode = ReturnCode.Unsuccess;
                    _publisher.Send(customer.Email, string.Empty);
                    break;
            }
            rm.Payload = customer;
            return rm;
        }
    }
}