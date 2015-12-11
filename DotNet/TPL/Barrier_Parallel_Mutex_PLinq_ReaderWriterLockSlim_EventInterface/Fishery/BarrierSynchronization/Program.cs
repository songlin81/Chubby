using System;
using System.Threading;
using System.Threading.Tasks;

namespace BarrierSynchronization
{
    class Program
    {
        //Establish 4 tasks to use
        static Task[] tasks = new Task[4];

        static Barrier barrier = null;

        static void Main(string[] args)
        {
            barrier = new Barrier(tasks.Length, (i) =>	//PostPhaseAction
            {
                Console.WriteLine("**********************************************************");
                Console.WriteLine("\nBarrier Phase number:{0}\n", i.CurrentPhaseNumber);
                Console.WriteLine("**********************************************************");
            });

            for (int j = 0; j < tasks.Length; j++)
            {
                tasks[j] = Task.Factory.StartNew((obj) =>
                {
                    var single = Convert.ToInt32(obj);

                    LoadUser(single);
                    barrier.SignalAndWait();

                    LoadProduct(single);
                    barrier.SignalAndWait();

                    LoadOrder(single);
                    barrier.SignalAndWait();
                }, j);
            }

            Task.WaitAll(tasks);

            Console.WriteLine("Fully loaded!");
            Console.Read();
        }

        static void LoadUser(int num)
        {
            Console.WriteLine("Current task:{0} is loading up User data！", num);
        }

        static void LoadProduct(int num)
        {
            Console.WriteLine("Current task:{0} is loading up Product data！", num);
        }

        static void LoadOrder(int num)
        {
            Console.WriteLine("Current task:{0} is loading up Order data！", num);
        }
    }
}