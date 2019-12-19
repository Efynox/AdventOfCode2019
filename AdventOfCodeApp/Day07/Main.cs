using AdventOfCodeApp.Day05;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCodeApp.Day07
{
    class Main : AbstractMain
    {
        static List<int> Sequences1 = new List<int> { 0, 1, 2, 3, 4 };
        const string Program1a = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
        const string Program1b = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0";
        const string Program1c = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";

        static List<int> Sequences2 = new List<int> { 5, 6, 7, 8, 9 };
        const string Program2a = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
        const string Program2b = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";

        override public void Execute()
        {
            string code = GetSingleInput();
            //code = Program2b;

            var seqs = Sequences2;
            List<List<int>> inputs = new List<List<int>>();

            var combos = GetAllCombinaisons(seqs);
            int max = int.MinValue;
            foreach (List<int> comb in combos)
            {
                List<Intcode> amps = new List<Intcode>();
                seqs.ForEach(seq => amps.Add(new Intcode(code)));
                seqs.ForEach(seq => inputs.Add(new List<int>()));

                // Settings phase
                for (int i = 0; i < amps.Count; i++)
                {
                    inputs[(i + 1) % seqs.Count] = amps[i].Execute(new List<int> { comb[i] });
                    //inputs[i].AddRange(amps[ampIdx].Execute(inputs[ampIdx]));
                }

                // Signal phase
                inputs[0].Add(0);
                int ampIdx = 0;
                for (int iter = 0; !amps.TrueForAll(amp => amp.IsHalted); iter++)
                {
                    ampIdx = iter % seqs.Count;
                    if (!amps[ampIdx].IsHalted)
                        inputs[(iter + 1) % seqs.Count] = amps[ampIdx].Execute(inputs[ampIdx]);
                    else
                        Console.WriteLine("amps " + ampIdx + " halted");
                }

                if (amps[amps.Count - 1].Outputs[0] > max)
                    max = amps[amps.Count - 1].Outputs[0];
            }
            Console.WriteLine(max);
        }

        private static List<List<int>> GetAllCombinaisons(List<int> list)
        {
            List<List<int>> result = new List<List<int>>();

            if (list.Count == 1)
            {
                result.Add(list);
                return result;
            }

            for (int i = 0; i < list.Count; i++)
            {
                var subList = new List<int>(list);
                subList.RemoveAt(i);
                result.AddRange(GetAllCombinaisons(subList).Select(p => p.Prepend(list[i]).ToList()));
            }

            return result;
        }
    }
}
