using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeApp.Day05
{
    class Intcode
    {
        private readonly Queue<int> inputs = new Queue<int>();
        public readonly List<int> Outputs = new List<int>();
        public bool IsHalted = false;

        private readonly string codeString;
        private readonly List<int> codeInts;

        private int index = 0;
        private bool hasNext = true;
        private readonly List<string> executed = new List<string>();
        private readonly bool verbose = false;

        public Intcode(string code)
        {
            this.codeString = code;
            this.codeInts = new List<string>(code.Split(',')).ConvertAll<int>(p => int.Parse(p));
        }

        public List<int> Execute(List<int> inputs)
        {
            // For each execution, reinit outputs
            this.hasNext = true;
            this.Outputs.Clear();

            inputs.ForEach(input => this.inputs.Enqueue(input));
            int? output = 0;
            while (index < codeInts.Count && hasNext)
            {
                output = ExecuteInstruction();
                if (output.HasValue)
                    this.Outputs.Add(output.Value);
                if (verbose)
                {
                    if (output.HasValue)
                        Console.WriteLine(string.Format("{0} => output:{1}", executed[executed.Count - 1], output));
                    else
                        Console.WriteLine(string.Format("{0}", executed[executed.Count - 1]));
                }
            }
            if (verbose)
            {
                Console.WriteLine("----");
                Console.WriteLine(string.Join(',', codeInts));
            }

            return this.Outputs;
        }

        private int GetParam(string opeCode, int pos)
        {
            return opeCode[3 - pos] == '1' ? codeInts[index + pos] : codeInts[codeInts[index + pos]];
        }

        private int? ExecuteInstruction()
        {
            string opeCode = codeInts[index].ToString().PadLeft(5, '0'); // 00004
            int? output = null;
            int nbParams = 0;  
            int? jump = null;
            switch (int.Parse(opeCode.Substring(3, 2)))
            {
                // 1 => a+b=c
                case 1:
                    nbParams = 3;
                    this.codeInts[codeInts[index + 3]] = GetParam(opeCode, 1) + GetParam(opeCode, 2);
                    break;

                case 2:
                    nbParams = 3;
                    this.codeInts[codeInts[index + 3]] = GetParam(opeCode, 1) * GetParam(opeCode, 2);
                    break;

                case 3:
                    if (this.inputs.Count > 0)
                    {
                        nbParams = 1;
                        codeInts[codeInts[index + 1]] = this.inputs.Dequeue();
                    }
                    else
                    {
                        this.hasNext = false;
                    }
                    break;

                case 4:
                    nbParams = 1;
                    output = GetParam(opeCode, 1);
                    break;

                // jump-if-true
                case 5:
                    nbParams = 2;
                    if (GetParam(opeCode, 1) != 0)
                        jump = GetParam(opeCode, 2);
                    break;

                // jump-if-false
                case 6:
                    nbParams = 2;
                    if (GetParam(opeCode, 1) == 0)
                        jump = GetParam(opeCode, 2);
                    break;

                // less than
                case 7:
                    nbParams = 3;
                    this.codeInts[codeInts[index + 3]] = (GetParam(opeCode, 1) < GetParam(opeCode, 2)) ? 1 : 0;
                    break;

                // equals
                case 8:
                    nbParams = 3;
                    this.codeInts[codeInts[index + 3]] = (GetParam(opeCode, 1) == GetParam(opeCode, 2)) ? 1 : 0;
                    break;

                case 99:
                    this.IsHalted = true;
                    this.hasNext = false;
                    break;
            }

            this.executed.Add(string.Format("{0}:{1}", index, string.Join(',', codeInts.GetRange(index, nbParams + 1))));

            if (hasNext)
                this.index = jump.HasValue ? jump.Value : this.index + 1 + nbParams;

            return output;
        }
    }
}
