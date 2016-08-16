using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Interceptor
{
    class MyBehavior : IInterceptionBehavior
    {

        /// <summary>
        /// Returns the interfaces required by the behavior for the objects it intercepts.
        /// </summary>
        /// <returns>
        /// The required interfaces.
        /// </returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        /// <summary>
        /// Implement this method to execute your behavior processing.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate
        /// in the behavior chain</param>
        /// <returns>
        /// Return value from the target.
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine("Before method execution");
            IMethodReturn msg = getNext()(input, getNext);
            Console.WriteLine("After method execution");
            return msg;
        }

        /// <summary>
        /// Optimization hint for proxy generation - will this behavior actually
        /// perform any operations when invoked?
        /// </summary>
        public bool WillExecute
        {
            get { return true; }
        }

    }
}
