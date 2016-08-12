    *** AutoResetEvent ***

* ManualResetEvent
Set()方法将状态设置为有信号
Reset()将其设置为无信号
WaitOne()将阻塞到其有信号为止，若调用WaitOne的时刻就是有信号的，将不会阻塞

* AutoResetEvent
与ManualResetEvent的区别是，AutoResetEvent.WaitOne()会自动改变事件对象的状态，即AutoResetEvent.WaitOne()每执行一次，事件的状态就改变一次。
有信号-->无信号；无信号-->有信号

example:

  class MyMainClass
    {
        const int numIterations = 10;        
        // 初始的时候是没有信号的，这里的意思是指参数false
        static AutoResetEvent myResetEvent = new AutoResetEvent(false);
        static int number;

        static void Main()
        {
            Thread myReaderThread = new Thread(new ThreadStart(MyReadThreadProc));
            myReaderThread.Name = "ReaderThread";
            myReaderThread.Start();

            for (int i = 1; i <= numIterations; i++)
            {
                Console.WriteLine("Writer thread writing value: {0}", i);
                number = i;

                // 发信号，说明值已经被写进去了。这里的意思是说Set是一个发信号的方法。
                myResetEvent.Set();

                Thread.Sleep(10000);
            }

            myReaderThread.Abort();
        }

        static void MyReadThreadProc()
        {
            while (true)
            {
                //在数据被作者写入之前不会被读者读取
                myResetEvent.WaitOne();
                Console.WriteLine("{0} reading value: {1}", Thread.CurrentThread.Name, number);
            }
        }
    }

myResetEven.Set(),其实就相当于一个开关,如果没有执行set()方法,下面的waitOne()就等不到让它执行的通知,这样一来waitOne后面的语句也不会执行了.
