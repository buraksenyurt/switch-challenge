using BusinessApp.Model;
using BusinessApp.Shared;
using BusinessApp.Shared.Services;

namespace BusinessApp.Contracts
{
    public class ProcessInvalid
        : IProcessContract
    {
        public ReturnMessage<Customer> ApplyState(Customer customer)
        {
            ReturnMessage<Customer> rm=new ReturnMessage<Customer>();
            customer.CalculatedBonus.IsActive = false;
            customer.CalculatedBonus.BonusValidationDate = DateTime.MinValue;
            customer.CalculatedBonus.Value = 0;
            rm.ReturnCode = ReturnCode.Unsuccess;
            rm.Payload=customer;

            //PROBLEM: Bu bağımlılıkta sınıf dışına alınabilir
            Publisher postman=new Publisher();
            postman.SendToManager("Müşteri ödeme ve limitleri düzensiz ya da incelemede.");

            return rm;
        }
    }
}