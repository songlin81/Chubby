using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DataParallism
{
	class Program
	{
        #region Parallel Invoke

            /*
            static void Main(string[] args)
            {
                var watch = Stopwatch.StartNew();

                watch.Start();
                Run1();
                Run2();
                Console.WriteLine("Serial:{0}\n", watch.ElapsedMilliseconds);

                watch.Restart();
                Parallel.Invoke(Run1, Run2);
                watch.Stop();
                Console.WriteLine("Parallel:{0}", watch.ElapsedMilliseconds);

                Console.Read();
            }

            static void Run1()
            {
                Console.WriteLine("Task one is going on for 3s");
                Thread.Sleep(3000);
            }

            static void Run2()
            {
                Console.WriteLine("Task two is going on for 5s");
                Thread.Sleep(5000);
            }   
            */

        #endregion


        #region Parallel For

            /*
            static void Main(string[] args)
            {
                for (int j = 1; j < 4; j++)
                {
                    Console.WriteLine("\nRound{0}", j);
                    ConcurrentBag<int> bag = new ConcurrentBag<int>();
                    var watch = Stopwatch.StartNew();
                    watch.Start();
                    for (int i = 0; i < 20000000; i++)
                    {
                        bag.Add(i);
                    }
                    Console.WriteLine("Serial：Collection Bag:{0}, Processing time：{1}", bag.Count, watch.ElapsedMilliseconds);
                    GC.Collect();

                    bag = new ConcurrentBag<int>();
                    watch = Stopwatch.StartNew();
                    watch.Start();
                    Parallel.For(0, 20000000, i => bag.Add(i));
                    Console.WriteLine("Paralle：Collection Bag:{0}, Processing time：{1}", bag.Count, watch.ElapsedMilliseconds);
                    GC.Collect();
                }
            }
            */

        #endregion


        #region Parallel ForEach

            /*
            static void Main(string[] args)
            {
                for (int j = 1; j < 4; j++)
                {
                    Console.WriteLine("\nRound {0}", j);

                    ConcurrentBag<int> bag = new ConcurrentBag<int>();
                    var watch = Stopwatch.StartNew();
                    watch.Start();
                    for (int i = 0; i < 3000000; i++)
                    {
                        bag.Add(i);
                    }
                    Console.WriteLine("Serial：Collection Bag:{0}, Processing time：{1}", bag.Count, watch.ElapsedMilliseconds);
                    GC.Collect();

                    bag = new ConcurrentBag<int>();
                    watch = Stopwatch.StartNew();
                    watch.Start();
                    Parallel.ForEach(Partitioner.Create(0, 3000000), i =>
                    {
                        for (int m = i.Item1; m < i.Item2; m++)
                        {
                            bag.Add(m);
                        }
                    });
                    Console.WriteLine("Parallel: Collection Bag{0}, Processing time：{1}", bag.Count, watch.ElapsedMilliseconds);
                    GC.Collect();
                }
            }
            */

        #endregion


        #region Parallel Break

            /*
            static void Main(string[] args)
            {
                var watch = Stopwatch.StartNew();
                watch.Start();

                ConcurrentBag<int> bag = new ConcurrentBag<int>();

                Parallel.For(0, 20000000, (i, state) =>
                {
                    if (bag.Count == 1000)
                    {
                        state.Break();
                        return;
                    }
                    bag.Add(i);
                });

                Console.WriteLine("Collection Bag {0}", bag.Count);
            }
            */

        #endregion


        #region Parallel Exception

            
            static void Main(string[] args)
            {
                try
                {
                    Parallel.Invoke(Run1, Run2);
                }
                catch (AggregateException ex)
                {
                    foreach (var single in ex.InnerExceptions)
                    {
                        Console.WriteLine(single.Message);
                    }
                }

                Console.Read();
            }

            static void Run1()
            {
                Thread.Sleep(3000);
                throw new Exception("Exception from Task 1");
            }

            static void Run2()
            {
                Thread.Sleep(5000);

                throw new Exception("Exception from Task 2");
            }
            

        #endregion
	}
}