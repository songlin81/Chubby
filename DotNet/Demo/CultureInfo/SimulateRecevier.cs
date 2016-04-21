using System;
using System.Threading;

namespace FS
{
    class SimulateRecevier
    {
        private string name;
        public string Name
        {
            get;
            set;
        }
        public void ReceiveMessage()
        {
            Console.WriteLine(string.Format("Thread {0} is distributed to handle it", Thread.CurrentThread.ManagedThreadId));
            Thread.Sleep(1000);
        }
    }
}
