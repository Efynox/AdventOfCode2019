using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeApp.Day02
{
    class Main : AbstractMain
    {
        override public void Execute()
        {
            string input = GetSingleInput();

            List<int> array = ParseInput(input);

            Console.WriteLine(Intcode(array));
            Console.WriteLine("----");

            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    List<int> work = ParseInput(input);
                    work[1] = noun;
                    work[2] = verb;

                    if (Intcode(work) == 19690720)
                    {
                        Console.WriteLine(noun);
                        Console.WriteLine(verb);
                        break;
                    }
                }
            }
        }

        public static List<int> ParseInput(string input)
        {
            return new List<string>(input.Split(',')).ConvertAll<int>(p => int.Parse(p));
        }

        public static int Intcode(List<int> values)
        {

            bool next = true;
            int idx = 0;
            int aPos, bPos, cPos;
            while (idx < values.Count && next)
            {
                switch (values[idx])
                {
                    // adds 
                    case 1:
                        aPos = values[idx + 1];
                        bPos = values[idx + 2];
                        cPos = values[idx + 3];

                        values[cPos] = values[aPos] + values[bPos];

                        idx += 4;
                        break;

                    // multiplies 
                    case 2:
                        aPos = values[idx + 1];
                        bPos = values[idx + 2];
                        cPos = values[idx + 3];

                        values[cPos] = values[aPos] * values[bPos];

                        idx += 4;
                        break;

                    // end
                    case 99:
                        next = false;
                        break;
                }
            }

            return values[0];
        }
    }
}
