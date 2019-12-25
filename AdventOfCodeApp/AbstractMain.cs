using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCodeApp
{
    abstract public class AbstractMain
    {
        private FileStream outputStream;
        private StreamWriter writer;

        public abstract void Execute();

        protected string GetSingleInput()
        {
            return File.ReadAllLines(string.Format("{0}/Input.txt", this.GetType().Namespace.Split('.')[^1]))[0];
        }

        protected string[] GetMultiInput(string filename = "Input")
        {
            return File.ReadAllLines(string.Format("{0}/{1}.txt", this.GetType().Namespace.Split('.')[^1], filename));
        }

        private void prepareStream()
        {
            if (outputStream == null)
            {
                outputStream = new FileStream(string.Format("{0}/Output.txt", this.GetType().Namespace.Split('.')[^1]), FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(outputStream);
            }
        }

        protected void WriteOutput(char c)
        {
            prepareStream(); 
            writer.Write(c);
        }
        protected void WriteOutput(string c)
        {
            prepareStream();
            writer.Write(c);
        }

        protected void WriteOutputLine()
        {
            prepareStream();
            writer.WriteLine();
        }

    }
}
