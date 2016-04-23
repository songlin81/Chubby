using System;
using System.Diagnostics;
using System.Threading;

namespace ProducerAndConsumer
{
    public class Cell
    {
        private int cellContents;
        private bool readFlag = false;

        public int ReadFromCell()
        {
            lock (this)
            {
                if (!readFlag)
                {
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch (SynchronizationLockException e) { Console.WriteLine(e); }
                    catch (ThreadInterruptedException e) { Console.WriteLine(e);}
                }
                Console.WriteLine("Consume: {0} , threadID: {1}", cellContents, Thread.CurrentThread.ManagedThreadId);
                readFlag = false;
                Monitor.Pulse(this);
            }
            return cellContents;
        }

        public void WriteToCell(int n)
        {
            lock (this)
            {
                if (readFlag)
                {
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch (SynchronizationLockException e) { Console.WriteLine(e); }
                    catch (ThreadInterruptedException e) { Console.WriteLine(e); }
                }
                cellContents = n;
                Console.WriteLine("Produce: {0}", cellContents);
                readFlag = true;
                Monitor.Pulse(this);
            }
        }
    }

    public class CellCons
    {
        private Cell cell;
        private int quantity = 1;

        public CellCons(Cell box, int request)
        {
            cell = box;
            quantity = request;
        }

        public void ThreadRun()
        {
            int valReturned;

            for (int looper = 1; looper <= quantity; looper++)
            {
                valReturned = cell.ReadFromCell();
            }
        }
    }

    public class CellProd
    {
        private Cell cell;
        private int quantity = 1;

        public CellProd(Cell box, int request)
        {
            cell = box;
            quantity = request;
        }

        public void ThreadRun()
        {
            int valReturned;

            for (int looper = 1; looper <= quantity; looper++)
            {
                cell.WriteToCell(looper);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int result = 0;
            Cell cell=new Cell();
            int count = 20;

            CellProd prod=new CellProd(cell, count);
            CellCons cons = new CellCons(cell, count);

            Thread producer=new Thread(new ThreadStart(prod.ThreadRun));
            Thread consumer=new Thread(new ThreadStart(cons.ThreadRun));

            try
            {
                Stopwatch st=new Stopwatch();
                st.Start();

                producer.Start();
                consumer.Start();
                producer.Join();
                consumer.Join();

                st.Stop();
                Console.WriteLine(st.Elapsed);
                //Console.ReadLine();
            }
            catch (ThreadStateException e)
            {
                Console.WriteLine(e);
                result = 1;
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e);
                result = 1;
            }

            Environment.ExitCode = result;
        }
    }
}
