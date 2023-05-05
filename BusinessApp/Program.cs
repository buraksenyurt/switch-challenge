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
        var result = processor.CalculateBonus(toni_stark, CustomerProcessState.Vip);

        Console.WriteLine($"{result.ReturnCode}");
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
            case CustomerProcessState.Valid:
                break;
            case CustomerProcessState.NewSubscriber:
                break;
            case CustomerProcessState.IrregularPayments:
                break;
            case CustomerProcessState.UnsufficentLimit:
                break;
            case CustomerProcessState.Vip:
                switch (customer.CustomerType)
                {
                    case CustomerType.Gold:
                    case CustomerType.Platinium:
                    case CustomerType.Basic:
                        break;
                    case CustomerType.Newbee:
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
    Invalid,
    Valid,
    NewSubscriber,
    UnsufficentLimit,
    IrregularPayments,
    Vip
}
