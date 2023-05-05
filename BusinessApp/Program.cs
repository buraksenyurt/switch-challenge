namespace BusinessApp;
class Program
{
    static void Main(string[] args)
    {

    }
}

class CustomerLogic
{
    public ReturnCode CalculateBonus(Customer customer,CustomerProcessState customerProcessState){
        throw new NotImplementedException();
    }
}

class Customer
{
    public int Id { get; set; }
    public FullName Title { get; set; } = new();
    public int MyProperty { get; set; }
    public CustomerType CustomerType { get; set; }
}

class FullName
{
    public string FirstName { get; set; } = string.Empty;
    public string? MidName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

enum CustomerType{
    Gold,
    Platinium,
    Basic,
    Newbee
}

enum ReturnCode{
    Success,
    Fail
}

enum CustomerProcessState{
    Invalid,
    Valid,

}
