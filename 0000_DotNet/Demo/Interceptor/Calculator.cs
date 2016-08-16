using System;

namespace Interceptor
{
    public class Calculator : ICalculator
    {

        public int Add(int value1, int value2)
        {
            int res = value1 + value2;
            Console.WriteLine("The result is: " + res.ToString());
            return res;
        }

    }
}