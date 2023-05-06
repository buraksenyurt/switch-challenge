namespace BusinessApp.Model
{
    public class Bonus
    {
        public int CustomerId { get; set; }
        public decimal Value { get; set; }
        public DateTime BonusValidationDate { get; set; }
        public bool IsActive { get; set; }
    }
}