# Cognitive Complexity - Switch Challenge

Çok fazla switch-case ve hatta if içerdiği için cognitive complexity değeri yüksek fonksiyonları nasıl iyileştirebiliriz? Bu soruya cevap bulmaya çalıştığım C# kod reposudur.

## Başlangıç Konumu

Ana odak noktamız aşağıdaki fonksiyon. Deneysel amaçlı olarak yazıldığından az sayıda switch içeriyor belki ama CustomerProcessState'in en az 100 case'e konu olacak enum değerine sahip olduğunu düşünerek hareket edelim.

```csharp
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
```
