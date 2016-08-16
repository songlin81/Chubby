using System.Threading.Tasks;
using System;
using System.Threading;

class Program
{
    //默认的容纳大小为“硬件线程“数
    static CountdownEvent cde = new CountdownEvent(Environment.ProcessorCount);

    static void Main(string[] args)
    {
        //加载User表需要5个任务
        var userTaskCount = 5;
        //重置信号
        cde.Reset(userTaskCount);

        for (int i = 0; i < userTaskCount; i++)
        {
            Task.Factory.StartNew(LoadUser, i);
        }
        //等待所有任务执行完毕
        cde.Wait();
        Console.WriteLine("\nUser表数据全部加载完毕！\n");

        //加载product需要8个任务
        var productTaskCount = 8;
        //重置信号
        cde.Reset(productTaskCount);
        for (int i = 0; i < productTaskCount; i++)
        {
            Task.Factory.StartNew((obj) =>
            {
                LoadProduct(obj);
            }, i);
        }
        cde.Wait();
        Console.WriteLine("\nProduct表数据全部加载完毕！\n");

        //加载order需要12个任务
        var orderTaskCount = 12;
        //重置信号
        cde.Reset(orderTaskCount);
        for (int i = 0; i < orderTaskCount; i++)
        {
            Task.Factory.StartNew((obj) =>
            {
                LoadOrder(obj);
            }, i);
        }
        cde.Wait();
        Console.WriteLine("\nOrder表数据全部加载完毕！\n");

        Console.WriteLine("\n(*^__^*) 嘻嘻，恭喜你，数据全部加载完毕\n");
        Console.Read();
    }

    static void LoadUser(object obj)
    {
        try
        {
            Console.WriteLine("当前任务:{0}正在加载User部分数据！", obj);
        }
        finally
        {
            cde.Signal();
        }
    }

    static void LoadProduct(object obj)
    {
        try
        {
            Console.WriteLine("当前任务:{0}正在加载Product部分数据！", obj);
        }
        finally
        {
            cde.Signal();
        }
    }

    static void LoadOrder(object obj)
    {
        try
        {
            Console.WriteLine("当前任务:{0}正在加载Order部分数据！", obj);
        }
        finally
        {
            cde.Signal();
        }
    }
}