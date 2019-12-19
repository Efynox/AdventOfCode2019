using System;
using System.Collections.Generic;

namespace AdventOfCodeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Day: ");

            string day = Console.ReadLine();

            var type = typeof(Program).Assembly.GetType(string.Format("AdventOfCodeApp.Day{0}.Main", day.PadLeft(2, '0')));
            if (type == null)
                throw new Exception("Error: Day not found");

            AbstractMain main = (AbstractMain)Activator.CreateInstance(type);
            main.Execute();
        }
    }
}
