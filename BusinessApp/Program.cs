using BusinessApp.Business;
using BusinessApp.Contracts;
using BusinessApp.Model;
using BusinessApp.Shared;

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
            Email = "toni_stark@sipeys.y"
        };

        var process_state=CustomerProcessState.Unleashed;        
        var processor = new CustomerLogic();   
        //PROBLEM: Enum sabiti türünden kullanılması gereken Interface implementasyonunu nasıl çözümleyebiliriz 
        var result=processor.CalculateBonus(new ProcessInvalid(),toni_stark);
        //var result = processor.CalculateBonus(toni_stark, process_state);

        Console.WriteLine($"Bonus calculation result is '{result.ReturnCode}'");
    }
}