using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var key = Console.ReadKey();
            Console.WriteLine((int)key.KeyChar);

            Console.ReadLine();
        }
    }
}