using BusinessApp.Contracts;
using BusinessApp.Model;
using BusinessApp.Shared;
using BusinessApp.Shared.Services;

namespace BusinessApp.Business
{

    public class CustomerLogic
    {
        public ReturnMessage<Customer> CalculateBonus(IProcessContract contract, Customer customer)
        {
            customer.ModifiedDate = DateTime.Now;
            customer.CalculatedBonus = new Bonus();
            customer.CalculatedBonus.CustomerId = customer.Id;
            var result = contract.Apply(customer);
            return result;
        }

        ////PROBLEM: Fonksiyon switch'lerden kurtularak yazılabilmeli

        // public ReturnMessage<Customer> CalculateBonus(Customer customer, CustomerProcessState customerProcessState)
        // {
        //     var postman = new Publisher();
        //     var rm = new ReturnMessage<Customer>();
        //     rm.ReturnCode = ReturnCode.Unsuccess;
        //     rm.Payload = customer;

        //     customer.ModifiedDate = DateTime.Now;
        //     customer.CalculatedBonus = new Bonus();
        //     customer.CalculatedBonus.CustomerId = customer.Id;

        //     switch (customerProcessState)
        //     {
        //         case CustomerProcessState.OnAcceptingPhase: // ProcessOnAccept
        //             customer.CalculatedBonus.IsActive = true;
        //             customer.CalculatedBonus.BonusValidationDate = DateTime.Now.AddDays(3);
        //             customer.CalculatedBonus.Value = 45;
        //             rm.ReturnCode = ReturnCode.Unsuccess;
        //             break;
        //         case CustomerProcessState.IrregularPayments: //ProcessInvalid
        //         case CustomerProcessState.UnsufficentLimit: //ProcessInvalid
        //         case CustomerProcessState.Investigating: //ProcessInvalid
        //             customer.CalculatedBonus.IsActive = false;
        //             customer.CalculatedBonus.BonusValidationDate = DateTime.MinValue;
        //             customer.CalculatedBonus.Value = 0;
        //             rm.ReturnCode = ReturnCode.Unsuccess;
        //             postman.SendToManager("Müşteri ödeme ve limitleri düzensiz ya da incelemede.");
        //             break;
        //         case CustomerProcessState.Subscriber: //ProcessByCustomerType
        //         case CustomerProcessState.Unleashed:
        //             switch (customer.CustomerType)
        //             {
        //                 case CustomerType.Gold:
        //                 case CustomerType.Platinium:
        //                 case CustomerType.Basic:
        //                     customer.CalculatedBonus.IsActive = true;
        //                     customer.CalculatedBonus.BonusValidationDate = DateTime.Now.AddMonths(1);
        //                     customer.CalculatedBonus.Value = 990.99M;
        //                     postman.Send(customer);
        //                     postman.SendToManager($"{customer.Title.LastName}. Özel müşterimiz var.");
        //                     rm.ReturnCode = ReturnCode.Success;

        //                     break;
        //                 case CustomerType.Newbee:
        //                     customer.CalculatedBonus.IsActive = true;
        //                     customer.CalculatedBonus.BonusValidationDate = DateTime.Now.AddDays(7);
        //                     customer.CalculatedBonus.Value = 100;
        //                     rm.ReturnCode = ReturnCode.Unsuccess;
        //                     postman.Send(customer);
        //                     break;
        //             }
        //             break;
        //         default:
        //             break;
        //     }

        //     return rm;
        // }
    }
}