using System;
using Microsoft.Practices.Unity;

namespace Interceptor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Loads the container
            IUnityContainer container = new UnityContainer();
            container = Microsoft.Practices.Unity.Configuration.UnityContainerExtensions.LoadConfiguration(container);

            // Resolve the proxy-sample
            ICalculator calc = Microsoft.Practices.Unity.UnityContainerExtensions.Resolve<ICalculator>(container);

            int res = calc.Add(3, 5);

            Console.WriteLine("Press <ENTER> to quit.");
            Console.ReadLine();
        }
    }
}

