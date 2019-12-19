using System;
using System.Collections.Generic;

namespace AdventOfCodeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Day: ");

            int day;
            if (!int.TryParse(Console.ReadLine(), out day))
                throw new Exception("Error: Invalid day format");

            var type = typeof(Program).Assembly.GetType(string.Format("AdventOfCodeApp.Day{0}.Main", day.ToString().PadLeft(2, '0')));
            if (type == null)
                throw new Exception("Error: Day not found");

            AbstractMain main = (AbstractMain)Activator.CreateInstance(type);
            main.Execute();
        }
    }
}
