using System;
using System.Threading.Tasks;

namespace AsyncFirstExample
{
    class MyClass
    {
        public async Task<string> DoStuff()
        {
            string val = await Task.Run(() => LongRunningOperation());
            return val;
        }

        private static async Task<string> LongRunningOperation()
        {
            Task task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Returning value will be given as below...");
                return 123;
            }).ContinueWith((t) =>
            {
                Console.WriteLine("--->" + t.Result);
            });


            int counter;
            for (counter = 0; counter < 5000; counter++)
            {
                Console.WriteLine(counter);
            }
            return "Counter = " + counter;
        }
    }
}
