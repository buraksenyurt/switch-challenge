using BusinessApp.Shared;

namespace BusinessApp.Model
{
    public class Manager
    {
        public int Id { get; set; }
        public FullName Title { get; set; } = new();
        public string Email { get; set; } = string.Empty;
    }

}