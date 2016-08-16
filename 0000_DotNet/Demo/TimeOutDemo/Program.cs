using System;
using System.Diagnostics;
using System.Threading;                                     //Using Threading Timer

namespace TimeOutDemo
{
    public delegate void DoHandler();                       //Pointer to caller function

    public class Timeout
    {
        private ManualResetEvent mTimeoutObject;            //singal mechanism
        private bool mBoTimeout;
        public DoHandler Do;                                //Definition of delegate

        public Timeout()
        {
            mTimeoutObject = new ManualResetEvent(true);    //signal initial status is stop.
        }
        ///<summary>
        /// Pass in time out figure, async to execute delegate function
        ///</summary>
        ///<returns>Timeout or not</returns>
        public bool DoWithTimeout(TimeSpan timeSpan)
        {
            if (Do ==null)
            {
                return false;
            }
            mTimeoutObject.Reset();
            mBoTimeout =true;                               //Mark flag of no timeout
            Do.BeginInvoke(DoAsyncCallBack, null);
            if (!mTimeoutObject.WaitOne(timeSpan, false))   // Wait signal Set
            {
                mBoTimeout =true;
            }
            return mBoTimeout;
        }
        ///<summary>
        /// Asyn delegate callback function
        ///</summary>
        ///<param name="result"></param>
        private void DoAsyncCallBack(IAsyncResult result)
        {
            try
            {
                Do.EndInvoke(result);
                mBoTimeout = false;                         // invocation of delegate function's not timing out
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                mBoTimeout = true;
            }
            finally
            {
                mTimeoutObject.Set();
            }
        }
    }

    class Program
    {
        private static Stopwatch watch;
        private static System.Threading.Timer timer;

        [STAThread]
        static void Main(string[] args)
        {
            watch = new Stopwatch();

            Timeout timeout = new Timeout();
            timeout.Do = new Program().DoSomething;
            watch.Start();
            timer = new System.Threading.Timer(timerCallBack, null, 0, 500);
            Console.WriteLine("4秒超时开始执行");

            bool bo = timeout.DoWithTimeout(new TimeSpan(0, 0, 0, 4));
            Console.WriteLine(string.Format("4秒超时执行结果,是否超时：{0}", bo));
            Console.WriteLine("***************************************************");

            timeout = new Timeout();
            timeout.Do = new Program().DoSomething;
            Console.WriteLine("6秒超时开始执行");
            bo = timeout.DoWithTimeout(new TimeSpan(0, 0, 0, 6));
            Console.WriteLine(string.Format("6秒超时执行结果,是否超时：{0}", bo));

            timerCallBack(null);
            watch.Stop();
            timer.Dispose();
            Console.ReadLine();
        }
        static void timerCallBack(object obj)
        {
            Console.WriteLine(string.Format("运行时间:{0}秒", watch.Elapsed.TotalSeconds.ToString("F2")));
        }
        public void DoSomething()
        {
            // Sleep for 5 seconds
            System.Threading.Thread.Sleep(new TimeSpan(0, 0, 0, 5));
        }
    }
}
