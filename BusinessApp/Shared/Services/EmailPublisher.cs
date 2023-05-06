using BusinessApp.Contracts;
using BusinessApp.Model;

namespace BusinessApp.Shared.Services
{
    class EmailPublisher
        : IPublisher
    {
        public void Send(string email,string message)
        {
            Console.WriteLine($"{email} adresine bildirim yapılacak.\nMesaj: '{message}'");
        }
    }
}