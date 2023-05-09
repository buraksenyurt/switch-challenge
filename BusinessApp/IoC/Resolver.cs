using System.Reflection;
using BusinessApp.Business;
using BusinessApp.Contracts;
using BusinessApp.Shared.Services;

namespace BusinessApp.IoC
{
    public static class Resolver
    {
        private static Dictionary<Type, List<CustomerProcessState>> map = new Dictionary<Type, List<CustomerProcessState>>();

        static Resolver()
        {
            map.Add(typeof(ProcessOnAccept), new List<CustomerProcessState> { CustomerProcessState.OnAcceptingPhase });
            map.Add(typeof(ProcessInvalid), new List<CustomerProcessState> { CustomerProcessState.IrregularPayments, CustomerProcessState.UnsufficentLimit, CustomerProcessState.Investigating });
            map.Add(typeof(ProcessByCustomerType), new List<CustomerProcessState> { CustomerProcessState.Subscriber, CustomerProcessState.Unleashed });
        }

        public static IProcessContract? GetContract(CustomerProcessState processState)
        {
            Type? objectType = null;
            foreach (var (k, v) in map)
            {
                if (v.Contains(processState))
                {
                    objectType = k;
                    break;
                }
            }

            if (objectType == null)
                return null;

            //PROBLEM: ProcessOnAccept sınıfından EmailPublisher'ı enjekte ettiğimiz bir Constructor yok. Bu tip sınıflar için burada nasıl bir çözüm uygulayabiliriz.
            dynamic? result = Activator.CreateInstance(objectType, new EmailPublisher()) as IProcessContract;
            return result;
        }
    }
}