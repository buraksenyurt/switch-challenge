using BusinessApp.Model;
using BusinessApp.Shared;

namespace BusinessApp.Contracts
{
    public class ProcessFirstStage
        : ITypedProcessContract
    {
        private readonly IPublisher _publisher;
        public ProcessFirstStage(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public ReturnMessage<Customer> Apply(Customer customer)
        {
            ReturnMessage<Customer> rm = new ReturnMessage<Customer>();
            customer.CalculatedBonus.IsActive = true;
            customer.CalculatedBonus.BonusValidationDate = DateTime.Now.AddMonths(1);
            customer.CalculatedBonus.Value = 990.99M;
            rm.ReturnCode = ReturnCode.Success;            
            rm.Payload = customer;

            _publisher.Send(customer.Email, $"Bizden Bonus kazanzınıd. 990.99 birim ve tam 1 ay geçerli :)");
            _publisher.Send(customer.Owner.Email, $"{customer.Title.LastName}. Özel müşterimiz var. 1 ay geçerli 990.99 birim bonus kazandı!");

            return rm;
        }
    }
}