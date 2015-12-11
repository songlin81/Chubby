using System;
using System.Collections.Generic;
using System.Linq;

namespace PLinqMapReduce
{
	class Program
	{
            static void Main(string[] args){

                List<Student> list = new List<Student>()
                {
                    new Student(){ ID=1, Name="jack", Age=20},
                    new Student(){ ID=1, Name="mary", Age=25},
                    new Student(){ ID=1, Name="joe", Age=29},
                    new Student(){ ID=1, Name="Aaron", Age=25},
                };

                var map = list.AsParallel().ToLookup(i => i.Age, count=>1);

                var reduce = from IGrouping<int, int> singleMap
                             in map.AsParallel()
                             select new
                             {
                                 Age = singleMap.Key,
                                 Count = singleMap.Count()
                             };

                reduce.ForAll(i =>
                {
                    Console.WriteLine("Age={0} Count:{1}", i.Age, i.Count);
                });

                Console.Read();
            }

            public class Student
            {
                public int ID { get; set; }
                public string Name { get; set; }
                public int Age { get; set; }
                public DateTime CreateTime { get; set; }
            }
	}
}