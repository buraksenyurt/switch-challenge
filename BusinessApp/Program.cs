using BusinessApp.Business;
using BusinessApp.Contracts;
using BusinessApp.Model;
using BusinessApp.Shared;
using BusinessApp.Shared.Services;

namespace BusinessApp;
class Program
{
    static void Main(string[] args)
    {
        var toni_stark = new Customer
        {
            Id = 1,
            CustomerType = CustomerType.Gold,
            Balance = 1500.50M,
            Title = new FullName
            {
                FirstName = "Toni",
                MidName = "Cunyir",
                LastName = "Stark"
            },
            Email = "toni_stark@sipeys.y",
            Owner = new Manager
            {
                Id = 76,
                Title = new FullName
                {
                    FirstName = "John",
                    LastName = "Doe"
                },
                Email = "john@doe.com"
            }
        };

        var process_state = CustomerProcessState.Subscriber;
        var processor = new CustomerLogic();
        //PROBLEM: Enum sabiti türünden kullanılması gereken Interface implementasyonunu nasıl çözümleyebiliriz 
        var result = processor.CalculateBonus(new ProcessByCustomerType(new EmailPublisher()), toni_stark);
        //var result = processor.CalculateBonus(toni_stark, process_state);

        Console.WriteLine($"Bonus calculation result is '{result.ReturnCode}'");
    }
}