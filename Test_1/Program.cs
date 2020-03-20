
using Entities;
using System;

namespace Test_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string text = System.IO.File.ReadAllText("test.csv");
            Console.WriteLine(text);
            Menu menu = new Menu();
			Console.WriteLine(menu.AdminMenuI());
        }
	}
}
