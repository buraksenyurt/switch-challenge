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

        //PROBLEM: Enum sabiti türünden kullanılması gereken Interface implementasyonunu nasıl çözümleyebiliriz 
        // Belki aşağıdaki gibi bir map sistemi ile hangi enum sabiti için hangi sınıf tipini kullanacağımızı belirtebiliriz.
        //SORU: Enum türüne göre ilgili sınıf adını yakaladık. Sınıfa ait nesne örneğini Runtime'da nasıl ayağa kaldırırız? Activator olabilir mi?
        Dictionary<String, List<CustomerProcessState>> map = new Dictionary<string, List<CustomerProcessState>>();
        map.Add("ProcessOnAccept", new List<CustomerProcessState> { CustomerProcessState.OnAcceptingPhase });
        map.Add("ProcessInvalid", new List<CustomerProcessState> { CustomerProcessState.IrregularPayments, CustomerProcessState.UnsufficentLimit, CustomerProcessState.Investigating });
        map.Add("ProcessByCustomerType", new List<CustomerProcessState> { CustomerProcessState.Subscriber, CustomerProcessState.Unleashed });

        var process_state = CustomerProcessState.Subscriber;
        foreach (var (k, v) in map)
        {
            if (v.Contains(process_state))
            {
                var type_name = k;
                Console.WriteLine($"Type name is {type_name}");
                break;
            }
        }

        var processor = new CustomerLogic();        
        var result = processor.CalculateBonus(new ProcessByCustomerType(new EmailPublisher()), toni_stark);

        Console.WriteLine($"Bonus calculation result is '{result.ReturnCode}'");
    }
}