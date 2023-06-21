using BusinessApp.Business;
using BusinessApp.Contracts;
using BusinessApp.Shared;
using BusinessApp.Shared.Services;

namespace BusinessApp.IoC
{
    public static class Resolver
    {
        private static Dictionary<Enum, Type> mapMain = new Dictionary<Enum, Type>();

        static Resolver()
        {
            mapMain.Add(CustomerProcessState.OnAcceptingPhase, typeof(ProcessOnAccept));
            mapMain.Add(CustomerProcessState.IrregularPayments, typeof(ProcessInvalid));
            mapMain.Add(CustomerProcessState.UnsufficentLimit, typeof(ProcessInvalid));
            mapMain.Add(CustomerProcessState.Investigating, typeof(ProcessInvalid));
            mapMain.Add(CustomerProcessState.Subscriber, typeof(ProcessByCustomerType));
            mapMain.Add(CustomerProcessState.Unleashed, typeof(ProcessByCustomerType));
            mapMain.Add(CustomerType.Gold, typeof(ProcessFirstStage));
            mapMain.Add(CustomerType.Platinium, typeof(ProcessFirstStage));
            mapMain.Add(CustomerType.Basic, typeof(ProcessFirstStage));
            mapMain.Add(CustomerType.Newbee, typeof(ProcessSecondStage));
        }

        public static IContract? GetContract(Enum enumValue)
        {
            if (mapMain.TryGetValue(enumValue, out Type? objectType))
            {
                //PROBLEM: Bazı nesne yapıcılarında IPublisher ihtiyacı var. Bazılarında yok. Opsiyonel. Nasıl çözeriz?
                dynamic? instance = Activator.CreateInstance(objectType, new EmailPublisher()) as IContract;
                return instance;

            }
            return null;
        }
    }
}