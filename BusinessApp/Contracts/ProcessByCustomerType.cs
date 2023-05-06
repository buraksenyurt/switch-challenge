using BusinessApp.Model;
using BusinessApp.Shared;
using BusinessApp.Shared.Services;

namespace BusinessApp.Contracts
{
    public class ProcessByCustomerType
        : IProcessContract
    {
        //PROBLEM: Burada da switch'ten kurtulabilmek lazım
        public ReturnMessage<Customer> ApplyState(Customer customer)
        {
            ReturnMessage<Customer> rm = new ReturnMessage<Customer>();
            Publisher postman = new Publisher();

            switch (customer.CustomerType)
            {
                case CustomerType.Gold:
                case CustomerType.Platinium:
                case CustomerType.Basic:
                    customer.CalculatedBonus.IsActive = true;
                    customer.CalculatedBonus.BonusValidationDate = DateTime.Now.AddMonths(1);
                    customer.CalculatedBonus.Value = 990.99M;
                    postman.Send(customer);
                    postman.SendToManager($"{customer.Title.LastName}. Özel müşterimiz var.");
                    rm.ReturnCode = ReturnCode.Success;
                    break;
                case CustomerType.Newbee:
                    customer.CalculatedBonus.IsActive = true;
                    customer.CalculatedBonus.BonusValidationDate = DateTime.Now.AddDays(7);
                    customer.CalculatedBonus.Value = 100;
                    rm.ReturnCode = ReturnCode.Unsuccess;
                    postman.Send(customer);
                    break;
            }
            rm.Payload = customer;
            return rm;
        }
    }
}