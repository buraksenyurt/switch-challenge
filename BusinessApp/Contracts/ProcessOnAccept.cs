using BusinessApp.Model;
using BusinessApp.Shared;

namespace BusinessApp.Contracts
{
    public class ProcessOnAccept
        : IProcessContract
    {
        public ReturnMessage<Customer> ApplyState(Customer customer)
        {
            ReturnMessage<Customer> rm = new ReturnMessage<Customer>();
            customer.CalculatedBonus.IsActive = true;
            customer.CalculatedBonus.BonusValidationDate = DateTime.Now.AddDays(3);
            customer.CalculatedBonus.Value = 45;
            rm.Payload = customer;
            rm.ReturnCode = ReturnCode.Unsuccess;
            return rm;

        }
    }
}