using System;
using System.Threading.Tasks;

namespace DataParallel
{
	class MainClass
	{
		public static void Main ()
		{
			Parallel.Invoke(
				ConvertPrimaryNumber1,
				ConvertPrimaryNumber2,
				ConvertPrimaryNumber3,
				ConvertPrimaryNumber4
			);
			Console.ReadLine();
		}

		static void ConvertPrimaryNumber1(){
			Console.WriteLine("Number 1 converted");
		}
		static void ConvertPrimaryNumber2(){
			Console.WriteLine("Number 2 converted");
		}
		static void ConvertPrimaryNumber3(){
			Console.WriteLine("Number 3 converted");
		}
		static void ConvertPrimaryNumber4(){
			Console.WriteLine("Number 4 converted");
		}
	}
}
