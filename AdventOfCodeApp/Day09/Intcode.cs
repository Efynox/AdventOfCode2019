using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeApp.Day09
{
    class Intcode
    {
        public readonly List<long> Outputs = new List<long>();
        public bool IsHalted = false;

        private readonly string code;
        private readonly List<long> values;
        private readonly Queue<long> inputs = new Queue<long>();

        private int index = 0;
        private bool hasNext = true;
        private string currentInstruction;

        private int relativeBase = 0;

        private readonly bool verbose = false;
        private readonly List<string> executed = new List<string>();


        public Intcode(string code)
        {
            this.code = code;
            this.values = new List<string>(code.Split(',')).ConvertAll<long>(p => long.Parse(p));
        }

        public List<long> Execute(List<long> inputs)
        {
            // For each execution, reinit outputs
            this.hasNext = true;
            this.Outputs.Clear();

            inputs.ForEach(input => this.inputs.Enqueue(input));
            long? output = 0;
            while (index < values.Count && hasNext)
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
                Console.WriteLine(string.Join(',', values));
            }

            return this.Outputs;
        }

        private long? ExecuteInstruction()
        {
            this.currentInstruction = values[index].ToString().PadLeft(5, '0'); // 00004
            long? output = null;
            int nbParams = 0;
            int? jump = null;
            switch (int.Parse(this.currentInstruction.Substring(3, 2)))
            {
                // adds 
                case 1:
                    nbParams = 3;
                    WriteValue(3, ReadValue(1) + ReadValue(2));
                    break;

                // multiplies 
                case 2:
                    nbParams = 3;
                    WriteValue(3, ReadValue(1) * ReadValue(2));
                    break;

                // takes input 
                case 3:
                    if (this.inputs.Count > 0)
                    {
                        nbParams = 1;
                        WriteValue(1, this.inputs.Dequeue());
                    }
                    else
                    {
                        this.hasNext = false;
                    }
                    break;

                // outputs value
                case 4:
                    nbParams = 1;
                    output = ReadValue(1);
                    break;

                // jump-if-true
                case 5:
                    nbParams = 2;
                    if (ReadValue(1) != 0)
                        jump = (int)ReadValue(2);
                    break;

                // jump-if-false
                case 6:
                    nbParams = 2;
                    if (ReadValue(1) == 0)
                        jump = (int)ReadValue(2);
                    break;

                // less than
                case 7:
                    nbParams = 3;
                    WriteValue(3, (ReadValue(1) < ReadValue(2)) ? 1 : 0);
                    break;

                // equals
                case 8:
                    nbParams = 3;
                    WriteValue(3, (ReadValue(1) == ReadValue(2)) ? 1 : 0);
                    break;

                // adjusts the relative base
                case 9:
                    nbParams = 1;
                    this.relativeBase += (int)ReadValue(1);
                    break;

                case 99:
                    this.IsHalted = true;
                    this.hasNext = false;
                    break;
            }

            this.executed.Add(string.Format("{0}:{1}", index, string.Join(',', values.GetRange(index, nbParams + 1))));

            if (hasNext)
                this.index = jump.HasValue ? jump.Value : this.index + 1 + nbParams;

            return output;
        }
        private long ReadValue(int pos)
        {
            int position;
            switch (this.currentInstruction[3 - pos])
            {
                case '0':
                    position = (int)this.values[index + pos];
                    break;

                case '1':
                    position = index + pos;
                    break;

                case '2':
                    position = (int)this.values[index + pos] + relativeBase;
                    break;

                default:
                    throw new Exception("Invalid mode");
            }

            if (position >= this.values.Count)
                fillMemory(position);

            return this.values[position];
        }

        private void WriteValue(int pos, long value)
        {
            int position;
            switch (this.currentInstruction[3 - pos])
            {
                case '0':
                    position = (int)this.values[index + pos];
                    break;

                case '1':
                    throw new Exception("Can't write in immediate mode");

                case '2':
                    position = (int)this.values[index + pos] + relativeBase;
                    break;

                default:
                    throw new Exception("Invalid mode");
            }

            if (position >= this.values.Count)
                fillMemory(position);

            this.values[position] = value;
        }

        private void fillMemory(int pos)
        {
            while (this.values.Count <= pos)
                this.values.Add(0);
        }

    }
}
