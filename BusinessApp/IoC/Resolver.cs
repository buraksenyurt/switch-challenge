using BusinessApp.Business;
using BusinessApp.Contracts;
using BusinessApp.Shared;
using BusinessApp.Shared.Services;

namespace BusinessApp.IoC
{
    public static class Resolver
    {
        private static Dictionary<Type, List<CustomerProcessState>> mapMain = new Dictionary<Type, List<CustomerProcessState>>();
        private static Dictionary<Type, List<CustomerType>> mapSub = new Dictionary<Type, List<CustomerType>>();

        static Resolver()
        {
            mapMain.Add(typeof(ProcessOnAccept), new List<CustomerProcessState> { CustomerProcessState.OnAcceptingPhase });
            mapMain.Add(typeof(ProcessInvalid), new List<CustomerProcessState> { CustomerProcessState.IrregularPayments, CustomerProcessState.UnsufficentLimit, CustomerProcessState.Investigating });
            mapMain.Add(typeof(ProcessByCustomerType), new List<CustomerProcessState> { CustomerProcessState.Subscriber, CustomerProcessState.Unleashed });

            mapSub.Add(typeof(ProcessFirstStage), new List<CustomerType> { CustomerType.Gold, CustomerType.Platinium, CustomerType.Basic });
            mapSub.Add(typeof(ProcessSecondStage), new List<CustomerType> { CustomerType.Newbee });
        }

        //PROBLEM: İki map tanımlayıp her ikisi içinde iki Get... fonksiyonu oluştu. Nasıl tekleştirebiliriz?
        public static IProcessContract? GetContract(CustomerProcessState processState)
        {
            Type? objectType = null;
            foreach (var (k, v) in mapMain)
            {
                if (v.Contains(processState))
                {
                    objectType = k;
                    break;
                }
            }

            if (objectType == null)
                return null;

            //PROBLEM Bazı nesne yapıcılarında IPublisher ihtiyacı var. Bazılarında yok. Opsiyonel. Nasıl çözeriz?
            dynamic? instance = Activator.CreateInstance(objectType, new EmailPublisher()) as IProcessContract;
            return instance;
        }

        public static ITypedProcessContract? GetTypedContract(CustomerType customerType)
        {
            Type? objectType = null;
            foreach (var (k, v) in mapSub)
            {
                if (v.Contains(customerType))
                {
                    objectType = k;
                    break;
                }
            }

            if (objectType == null)
                return null;

            dynamic? instance = Activator.CreateInstance(objectType, new EmailPublisher()) as ITypedProcessContract;
            return instance;
        }
    }
}