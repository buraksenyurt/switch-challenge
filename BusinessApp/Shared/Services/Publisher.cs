using BusinessApp.Model;

namespace BusinessApp.Shared.Services
{
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
}