using System.Reflection;
using BusinessApp.Business;
using BusinessApp.Contracts;
using BusinessApp.Shared.Services;

namespace BusinessApp.IoC
{
    public static class Resolver
    {
        private static Dictionary<String, List<CustomerProcessState>> map = new Dictionary<string, List<CustomerProcessState>>();

        static Resolver()
        {
            map.Add("ProcessOnAccept", new List<CustomerProcessState> { CustomerProcessState.OnAcceptingPhase });
            map.Add("ProcessInvalid", new List<CustomerProcessState> { CustomerProcessState.IrregularPayments, CustomerProcessState.UnsufficentLimit, CustomerProcessState.Investigating });
            map.Add("ProcessByCustomerType", new List<CustomerProcessState> { CustomerProcessState.Subscriber, CustomerProcessState.Unleashed });
        }

        public static IProcessContract? GetContract(CustomerProcessState processState)
        {
            var type_name = string.Empty;
            foreach (var (k, v) in map)
            {
                if (v.Contains(processState))
                {
                    type_name = k;
                    break;
                }
            }

            var objectType = Type.GetType($"BusinessApp.Contracts.{type_name},BusinessApp");
            if (objectType == null)
                return null;

            //PROBLEM: ProcessOnAccept sınıfından EmailPublisher'ı enjekte ettiğimiz bir Constructor yok. Bu tip sınıflar için burada nasıl bir çözüm uygulayabiliriz.
            dynamic? result = Activator.CreateInstance(objectType, new EmailPublisher()) as IProcessContract;
            return result;
        }
    }
}