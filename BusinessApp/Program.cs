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
        };

        var processor = new CustomerLogic();
        var result = processor.CalculateBonus(toni_stark, CustomerProcessState.Unleashed);

        Console.WriteLine($"Bonus calculation result is '{result.ReturnCode}'");
    }
}

class CustomerLogic
{
    public ReturnMessage<Customer> CalculateBonus(Customer customer, CustomerProcessState customerProcessState)
    {
        var rm = new ReturnMessage<Customer>();
        rm.ReturnCode = ReturnCode.Unsuccess;
        rm.Payload = customer;

        customer.ModifiedDate = DateTime.Now;

        switch (customerProcessState)
        {
            case CustomerProcessState.OnAcceptingPhase:
                rm.ReturnCode = ReturnCode.Unsuccess;
                break;
            case CustomerProcessState.IrregularPayments:
                rm.ReturnCode = ReturnCode.Unsuccess;
                break;
            case CustomerProcessState.UnsufficentLimit:
                rm.ReturnCode = ReturnCode.Unsuccess;
                break;
            case CustomerProcessState.Investigating:
                rm.ReturnCode = ReturnCode.Unsuccess;
                break;
            case CustomerProcessState.Subscriber:
            case CustomerProcessState.Unleashed:
                switch (customer.CustomerType)
                {
                    case CustomerType.Gold:
                    case CustomerType.Platinium:
                    case CustomerType.Basic:
                        rm.ReturnCode = ReturnCode.Success;
                        break;
                    case CustomerType.Newbee:
                        rm.ReturnCode = ReturnCode.Unsuccess;
                        break;
                }
                break;
            default:
                break;
        }

        return rm;
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
