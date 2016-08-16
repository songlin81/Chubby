//ManualResetEvent 允许线程通过发信号互相通信。
//通常，此通信涉及一个线程在其他线程进行之前必须完成的任务。
//当一个线程开始一个活动（此活动必须完成后，其他线程才能开始）时，
//它调用 Reset 以将 ManualResetEvent 置于非终止状态，此线程可被视为控制 ManualResetEvent。
//调用 ManualResetEvent 上的 WaitOne 的线程将阻止，并等待信号。
//当控制线程完成活动时，它调用 Set 以发出等待线程可以继续进行的信号。并释放所有等待线程。
//一旦它被终止，ManualResetEvent 将保持终止状态（即对 WaitOne 的调用的线程将立即返回，并不阻塞），
//直到它被手动重置。可以通过将布尔值传递给构造函数来控制 ManualResetEvent 的初始状态，
//如果初始状态处于终止状态，为 true；否则为 false。

using System;
using System.Threading;
 
namespace ManualResetEventDemo
{
    class MREDemo
    {
        private ManualResetEvent _mre;
 
        public MREDemo()
        {
            this._mre = new ManualResetEvent(true);
        }
 
        public void CreateThreads()
        {
            Thread t1 = new Thread(new ThreadStart(Run));
            t1.Start();
 
            Thread t2 = new Thread(new ThreadStart(Run));
            t2.Start();
        }
 
        public void Set()
        {
            this._mre.Set();
        }
 
        public void Reset()
        {
            this._mre.Reset();
        }
 
        private void Run()
        {
            string strThreadID = string.Empty;
            try
            {
                while (true)
                {
                    // 阻塞当前线程
                    this._mre.WaitOne();
 
                    strThreadID = Thread.CurrentThread.ManagedThreadId.ToString();
                    Console.WriteLine("Thread(" + strThreadID + ") is running...");
 
                    Thread.Sleep(2000);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("线程(" + strThreadID + ")发生异常！错误描述：" + ex.Message.ToString());
            }
        }
 
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****************************");
            Console.WriteLine("输入\"stop\"停止线程运行...");
            Console.WriteLine("输入\"run\"开启线程运行...");
            Console.WriteLine("****************************\r\n");
 
            MREDemo objMRE = new MREDemo();
            objMRE.CreateThreads();
 
            while (true)
            {
                string input = Console.ReadLine();
                if (input.Trim().ToLower() == "stop")
                {
                    Console.WriteLine("线程已停止运行...");
                    objMRE.Reset();
                }
                else if (input.Trim().ToLower() == "run")
                {
                    Console.WriteLine("线程开启运行...");
                    objMRE.Set();
                }
            }
             
        }
    }
}