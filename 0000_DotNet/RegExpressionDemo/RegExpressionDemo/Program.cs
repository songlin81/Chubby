using System;
using System.Text.RegularExpressions;

namespace RegExpressionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Regex phoneExp = new Regex(@"^\(\d{3}\)\s\d{3}-\d{4}$");
            Regex phoneExp = new Regex(@"^\(\d{3}\)\s\d{3}-\d{4}$");    //(022) 123-4321

            string input;

            Console.Write("Enter a phone number: ");

            input = Console.ReadLine();

            while (phoneExp.Match(input).Success == false)
            {

                Console.WriteLine("Invalid input. Try again.");

                Console.Write("Enter a phone number: ");

                input = Console.ReadLine();
            }

            Console.WriteLine("Validated!");
        }
    }
}
