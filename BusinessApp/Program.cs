using BusinessApp.Business;
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

        var processor = new CustomerLogic();
        var result = processor.CalculateBonus(toni_stark, CustomerProcessState.Unleashed);

        Console.WriteLine($"Bonus calculation result is '{result.ReturnCode}'");
    }
}