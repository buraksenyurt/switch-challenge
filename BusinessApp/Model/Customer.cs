using BusinessApp.Shared;

namespace BusinessApp.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public FullName Title { get; set; } = new();
        public CustomerType CustomerType { get; set; }
        public decimal Balance { get; set; }
        public Bonus CalculatedBonus { get; set; } = new();
        public DateTime ModifiedDate { get; set; }
        public string Email { get; set; } = string.Empty;
    }

}