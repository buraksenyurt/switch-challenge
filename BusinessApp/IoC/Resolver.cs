using BusinessApp.Business;
using BusinessApp.Contracts;
using BusinessApp.Shared;
using BusinessApp.Shared.Services;

namespace BusinessApp.IoC
{
    public static class Resolver
    {
        private static Dictionary<Type, List<Enum>> mapMain = new Dictionary<Type, List<Enum>>();

        static Resolver()
        {
            mapMain.Add(typeof(ProcessOnAccept), new List<Enum> { CustomerProcessState.OnAcceptingPhase });
            mapMain.Add(typeof(ProcessInvalid), new List<Enum> { CustomerProcessState.IrregularPayments, CustomerProcessState.UnsufficentLimit, CustomerProcessState.Investigating });
            mapMain.Add(typeof(ProcessByCustomerType), new List<Enum> { CustomerProcessState.Subscriber, CustomerProcessState.Unleashed });
            mapMain.Add(typeof(ProcessFirstStage), new List<Enum> { CustomerType.Gold, CustomerType.Platinium, CustomerType.Basic });
            mapMain.Add(typeof(ProcessSecondStage), new List<Enum> { CustomerType.Newbee });
        }

        public static IContract? GetContract(Enum enumValue)
        {
            Type? objectType = null;
            foreach (var (k, v) in mapMain)
            {
                if (v.Contains(enumValue))
                {
                    objectType = k;
                    break;
                }
            }

            if (objectType == null)
                return null;

            //PROBLEM Bazı nesne yapıcılarında IPublisher ihtiyacı var. Bazılarında yok. Opsiyonel. Nasıl çözeriz?
            dynamic? instance = Activator.CreateInstance(objectType, new EmailPublisher()) as IContract;
            return instance;
        }
    }
}