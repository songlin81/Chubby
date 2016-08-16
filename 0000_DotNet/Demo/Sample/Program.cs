using System;
using System.Collections.Generic;
using System.Threading;

namespace Sample
{
    class Program
    {
        private static void Main(string[] args)
        {
            TimerCustom timer = new TimerCustom();

            timer.Interval = 1500;

            timer.Elapsed += (obj, evt) =>
            {
                TimerCustom singleTimer = obj as TimerCustom;

                //Stop the timer.
                singleTimer.Stop();

                if (singleTimer != null)
                {
                    if (singleTimer.queue.Count != 0)
                    {
                        var item = singleTimer.queue.Dequeue();

                        Send(item);

                        //Start the timer once email's out
                        singleTimer.Start();
                    }
                }
            };

            timer.Start();

            int
            i = 100;
            while (i>=0)
            {
                Console.WriteLine("ongoing");
                Thread.Sleep(1000);
                i--;
            }

            Console.Read();
        }

        static void Send(int obj)
        {
            Thread.Sleep(new Random().Next(8000, 10000));

            Console.WriteLine("Current Time：{0}，Mail is out!", DateTime.Now);
        }
    }

    class TimerCustom : System.Timers.Timer
    {
        public Queue<int> queue = new Queue<int>();

        public TimerCustom()
        {
            //for (int i = 0; i < short.MaxValue; i++)
            for (int i = 0; i < 3; i++)
            {
                queue.Enqueue(i);
            }
        }
    }
}