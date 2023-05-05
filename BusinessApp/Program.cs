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

class CustomerLogic
{
    //PROBLEM: Fonksiyon switch'lerden kurtularak yazılabilmeli
    public ReturnMessage<Customer> CalculateBonus(Customer customer, CustomerProcessState customerProcessState)
    {
        var postman = new Publisher();
        var rm = new ReturnMessage<Customer>();
        rm.ReturnCode = ReturnCode.Unsuccess;
        rm.Payload = customer;

        customer.ModifiedDate = DateTime.Now;
        customer.CalculatedBonus = new Bonus();
        customer.CalculatedBonus.CustomerId = customer.Id;

        switch (customerProcessState)
        {
            case CustomerProcessState.OnAcceptingPhase:
                customer.CalculatedBonus.IsActive = true;
                customer.CalculatedBonus.BonusValidationDate = DateTime.Now.AddDays(3);
                customer.CalculatedBonus.Value = 45;
                rm.ReturnCode = ReturnCode.Unsuccess;
                break;
            case CustomerProcessState.IrregularPayments:
            case CustomerProcessState.UnsufficentLimit:
            case CustomerProcessState.Investigating:
                customer.CalculatedBonus.IsActive = false;
                customer.CalculatedBonus.BonusValidationDate = DateTime.MinValue;
                customer.CalculatedBonus.Value = 0;
                rm.ReturnCode = ReturnCode.Unsuccess;
                postman.SendToManager("Müşteri ödeme ve limitleri düzensiz ya da incelemede.");
                break;
            case CustomerProcessState.Subscriber:
            case CustomerProcessState.Unleashed:
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
                break;
            default:
                break;
        }

        return rm;
    }
}

//BONUS PROBLEM: Gönderim kanalları bağımlılığı azaltılmalı
class Publisher
{
    public void Send(Customer customer)
    {
        Console.WriteLine($"{customer.Email} adresine bildirim yapılacak");
    }
    public void SendToManager(string state)
    {
        Console.WriteLine($"'{state}' durumu ilgili yönetici bilgilendirilecek");
    }
}

class Customer
{
    public int Id { get; set; }
    public FullName Title { get; set; } = new();
    public CustomerType CustomerType { get; set; }
    public decimal Balance { get; set; }
    public Bonus CalculatedBonus { get; set; } = new();
    public DateTime ModifiedDate { get; set; }
    public string Email { get; set; } = string.Empty;
}

class Bonus
{
    public int CustomerId { get; set; }
    public decimal Value { get; set; }
    public DateTime BonusValidationDate { get; set; }
    public bool IsActive { get; set; }
}

class FullName
{
    public string FirstName { get; set; } = string.Empty;
    public string? MidName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

class ReturnMessage<T>
{
    public ReturnCode ReturnCode { get; set; }
    public T? Payload { get; set; } = default;
}

enum CustomerType
{
    Gold,
    Platinium,
    Basic,
    Newbee
}

enum ReturnCode
{
    Success,
    Unsuccess,
    SystemError
}

enum CustomerProcessState
{
    Subscriber,
    Investigating,
    OnAcceptingPhase,
    UnsufficentLimit,
    IrregularPayments,
    Unleashed
}
