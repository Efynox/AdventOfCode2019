using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCodeApp.Day09
{
    class Main : AbstractMain
    {
        const long InputPart1 = 1;
        const long InputPart2 = 2;
        const string Sample1a = "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99";
        const string Sample1b = "1102,34915192,34915192,7,4,7,99,0";
        const string Sample1c = "104,1125899906842624,99";

        override public void Execute()
        {
            string code = GetSingleInput();

            // Part 1
            Intcode boost = new Intcode(code);
            boost.Execute(new List<long> { InputPart1 });
            boost.Outputs.ForEach(o => Console.WriteLine(o));

            // Part 2
            Intcode boost2 = new Intcode(code);
            boost2.Execute(new List<long> { InputPart2 });
            boost2.Outputs.ForEach(o => Console.WriteLine(o));
        }
    }
}
