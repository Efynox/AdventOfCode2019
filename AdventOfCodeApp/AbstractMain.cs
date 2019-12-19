using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCodeApp
{
    abstract class AbstractMain
    {
        public abstract void Execute();

        protected string GetSingleInput()
        {
            return File.ReadAllLines(string.Format("{0}/Input.txt", this.GetType().Namespace.Split('.')[^1]))[0];
        }

        protected string[] GetMultiInput()
        {
            return File.ReadAllLines(string.Format("{0}/Input.txt", this.GetType().Namespace.Split('.')[^0]));
        }
    }
}
