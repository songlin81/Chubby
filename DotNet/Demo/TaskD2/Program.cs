using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace TaskD2
{
    class Program
    {

        #region Task and factory

            /*
            static void Main(string[] args)
            {
                //One way
                var task1 = new Task(() =>
                {
                    Run1();
                });

                //Another way using factory
                var task2 = Task.Factory.StartNew(() =>
                {
                    Run2();
                });

                Console.WriteLine("Before start****************************");
                Console.WriteLine("task1: {0}", task1.Status);
                Console.WriteLine("task2: {0}", task2.Status);

                task1.Start();
                Console.WriteLine("\nAfter start****************************");
                Console.WriteLine("task1: {0}", task1.Status);
                Console.WriteLine("task2: {0}", task2.Status);

                //Main thread's waiting for all completion.
                Task.WaitAll(task1, task2);
                Console.WriteLine("\nAll task completed****************************");
                Console.WriteLine("task1: {0}", task1.Status);
                Console.WriteLine("task2: {0}", task2.Status);

                Console.Read();
            }

            static void Run1()
            {
                Thread.Sleep(1000);
                Console.WriteLine("Task 1");
            }

            static void Run2()
            {
                Thread.Sleep(2000);
                Console.WriteLine("Task 2");
            }
            */

        #endregion


        #region Task Cancellation

            /*
            static void Main(string[] args)
            {
                var cts = new CancellationTokenSource();
                var ct = cts.Token;

                Task task1 = new Task(() => { Run1(ct); }, ct);

                Task task2 = new Task(Run2);

                try
                {
                    task1.Start();
                    task2.Start();

                    Thread.Sleep(1000);

                    cts.Cancel();

                    Task.WaitAll(task1, task2);
                }
                catch (AggregateException ex)
                {
                    foreach (var e in ex.InnerExceptions)
                    {
                        Console.WriteLine("\nhi,I AM OperationCanceledException：{0}\n", e.Message);
                    }

                    Console.WriteLine("task1 cancelled? {0}", task1.IsCanceled);
                    Console.WriteLine("task2 cancelled? {0}", task2.IsCanceled);
                }

                Console.Read();
            }

            static void Run1(CancellationToken ct)
            {
                ct.ThrowIfCancellationRequested();
                Console.WriteLine("Task 1");
                Thread.Sleep(2000);
                ct.ThrowIfCancellationRequested();

                //Never get printed out.
                Console.WriteLine("Second part of the message from task 1");
            }

            static void Run2()
            {
                Console.WriteLine("Task 2");
            }
            */

        #endregion


        #region Task Result

            /*
            static void Main(string[] args)
            {
                var t1 = Task.Factory.StartNew<List<string>>(() => { return Run1(); });

                t1.Wait();

                var t2 = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Collection：" + string.Join(",", t1.Result));
                });

                Console.Read();
            }

            static List<string> Run1()
            {
                return new List<string> { "1", "4", "8" };
            }
            */

        #endregion


        #region Task ContinueWith

            /*
            static void Main(string[] args)
            {
                var t1 = Task.Factory.StartNew<List<string>>(() => { return Run1(); });

                var t2 = t1.ContinueWith((i) =>
                {
                    Console.WriteLine("Collection：" + string.Join(",", i.Result));
                });

                Console.Read();
            }

            static List<string> Run1()
            {
                return new List<string> { "1", "4", "8" };
            }
            */

        #endregion


        #region Task ContinueWith & WaitAll

            
            static void Main(string[] args)
            {
                ConcurrentStack<int> stack = new ConcurrentStack<int>();

                //Serial t1
                var t1 = Task.Factory.StartNew(() =>
                {
                    stack.Push(1);
                    stack.Push(2);
                });
                //Parellel t2 and t3
                var t2 = t1.ContinueWith(t =>
                {
                    int result;

                    stack.TryPop(out result);
                });
                var t3 = t1.ContinueWith(t =>
                {
                    int result;

                    stack.TryPop(out result);
                });
                Task.WaitAll(t2, t3);

                //Serial t4
                var t4 = Task.Factory.StartNew(() =>
                {
                    stack.Push(1);
                    stack.Push(2);
                });
                //Parallel t5 and t6
                var t5 = t4.ContinueWith(t =>
                {
                    int result;

                    stack.TryPop(out result);
                });
                var t6 = t4.ContinueWith(t =>
                {
                    int result;
                    stack.TryPeek(out result);
                });
                Task.WaitAll(t5, t6);

                //Serial t7
                var t7 = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Collection：" + stack.Count);
                });

                Console.Read();
            }
            

        #endregion
    }
}
