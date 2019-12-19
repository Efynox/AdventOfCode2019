using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeApp.Day04
{
    class Main : AbstractMain
    {
        private const string INPUT = "128392-643281";

        override public void Execute()
        {
            int min = 128392;
            int max = 643281;

            string pass;
            int count = 0;
            for(int num = min; num <= max; num++)
            {
                pass = num.ToString();
                if (HasAdjacents(pass) && IsIncreasing(pass))
                {
                    Console.WriteLine(pass);
                    count++;
                }
            }
            Console.WriteLine("==>" + count);
        }

        public static bool HasAdjacents(string pass)
        {
            char before, current1, current2, after;
            for (int i = 0; i < pass.Length - 1; i++)
            {
                before = i > 0 ? pass[i - 1] : 'x';
                current1 = pass[i];
                current2 = pass[i + 1];
                after = i + 2 < pass.Length ? pass[i + 2] : 'x';

                if (current1 == current2 && current1 != before && current1 != after)
                    return true;
            }
            return false;
        }


        public static bool HasTwoAdjacent(string pass)
        {
            for(int i = 0; i < pass.Length - 1; i++)
            {
                if (pass.Substring(i, 1) == pass.Substring(i + 1, 1))
                    return true;
            }
            return false;
        }

        public static bool HasThreeAdjacents(string pass)
        {
            for (int i = 0; i < pass.Length - 2; i++)
            {
                if (pass.Substring(i, 1) == pass.Substring(i + 1, 1) && pass.Substring(i, 1) == pass.Substring(i + 2, 1))
                    return true;
            }
            return false;
        }


        public static bool IsIncreasing(string pass)
        {
            for (int i = 0; i < pass.Length - 1; i++)
            {
                if (int.Parse(pass.Substring(i, 1)) > int.Parse(pass.Substring(i + 1, 1)))
                    return false;
            }
            return true;
        }
    }
}
