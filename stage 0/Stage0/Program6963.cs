// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
/// stage0 - get to know c#
using System;
namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome6963();
            Welcome3659();
            Console.ReadKey();
        }
        static partial void Welcome3659();
        private static void Welcome6963()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }

}
