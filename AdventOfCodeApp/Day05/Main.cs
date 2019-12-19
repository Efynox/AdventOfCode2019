using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCodeApp.Day05
{
    class Main : AbstractMain
    {
        //const int input = 5;
        const string Sample2a = "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9";
        const string Sample2b = "3,3,1105,-1,9,1101,0,0,12,4,12,99,1";
        const string Sample2c = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";

        override public void Execute()
        {
            string code = GetSingleInput();

            Console.Write("Input:");
            int input = int.Parse(Console.ReadLine());

            Console.WriteLine(string.Join(',', new Intcode(code).Execute(new List<int> { input })));
        }
    }
}
