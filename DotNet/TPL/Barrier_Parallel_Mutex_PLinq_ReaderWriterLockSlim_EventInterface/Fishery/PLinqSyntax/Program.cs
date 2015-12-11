using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PLinqSyntax
{
	class Program
	{
		
        #region AsParallel
    
        	/*
            static void Main(string[] args)
            {
                var dic = LoadData();

                Stopwatch watch = new Stopwatch();
                watch.Start();

                var query1 = (from n in dic.Values
                              where n.Age > 20 && n.Age < 25
                              select n).ToList();
                watch.Stop();
                Console.WriteLine("Serial time consumption：{0}", watch.ElapsedMilliseconds);

                watch.Restart();
                var query2 = (from n in dic.Values.AsParallel()
                              where n.Age > 20 && n.Age < 25
                              select n).ToList();
                watch.Stop();
                Console.WriteLine("Parallel time consumption：{0}", watch.ElapsedMilliseconds);

                Console.Read();
            }

            public static ConcurrentDictionary<int, Student> LoadData()
            {
                ConcurrentDictionary<int, Student> dic = new ConcurrentDictionary<int, Student>();

                Parallel.For(0, 5000000, (i) =>
                {
                    var single = new Student()
                    {
                        ID = i,
                        Name = "hxc" + i,
                        Age = i % 151,
                        CreateTime = DateTime.Now.AddSeconds(i)
                    };
                    dic.TryAdd(i, single);
                });

                return dic;
            }

            public class Student
            {
                public int ID { get; set; }
                public string Name { get; set; }
                public int Age { get; set; }
                public DateTime CreateTime { get; set; }
            }
            
          */  
         
        #endregion


        #region Orderby

            /*
            static void Main(string[] args)
            {
                var dic = LoadData();

                var query1 = (from n in dic.Values.AsParallel()
                              where n.Age > 20 && n.Age < 25
                              select n).ToList();
                query1.Take(10).ToList().ForEach((i) =>
                {
                    Console.WriteLine(i.CreateTime);
                });

                var query2 = (from n in dic.Values.AsParallel()
                                    // Comment off below in requiring full load of CPU usage.
                                  .WithDegreeOfParallelism(Environment.ProcessorCount - 1) 
                              where n.Age > 20 && n.Age < 25
                              orderby n.CreateTime descending
                              select n).ToList();
                query2.Take(10).ToList().ForEach((i) =>
                {
                    Console.WriteLine(i.CreateTime);
                });

                Console.Read();
            }

            public static ConcurrentDictionary<int, Student> LoadData()
            {
                ConcurrentDictionary<int, Student> dic = new ConcurrentDictionary<int, Student>();

                Parallel.For(0, 5000000, (i) =>
                {
                    var single = new Student()
                    {
                        ID = i,
                        Name = "hxc" + i,
                        Age = i % 151,
                        CreateTime = DateTime.Now.AddSeconds(i)
                    };
                    dic.TryAdd(i, single);
                });

                return dic;
            }

            public class Student
            {
                public int ID { get; set; }
                public string Name { get; set; }
                public int Age { get; set; }
                public DateTime CreateTime { get; set; }
            }
           */ 

        #endregion


        #region Enumerable

            
            static void Main(string[] args)
            {
                ConcurrentBag<int> bag = new ConcurrentBag<int>();

                var list = ParallelEnumerable.Range(0, 10000);

                list.ForAll((i) =>
                {
                    bag.Add(i);
                });

                Console.WriteLine("bag collection:{0}", bag.Count);
                Console.WriteLine("Sum:{0}", list.Sum());
                Console.WriteLine("Max:{0}", list.Max());
                Console.WriteLine("FirstOrDefault:{0}", list.FirstOrDefault());

                Console.Read();
            }
            

        #endregion
        
	}
}