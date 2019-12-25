using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventOfCodeApp.Day24
{
    class Part1 : AbstractMain
    {
        private int size;
        private char[,] map;
        private int interval = 10;
        private int turn = 0;
        private bool stop = false;

        private List<char[,]> previous = new List<char[,]>();

        override public void Execute()
        {
            InitMap(5);
            Print();
            Run();

            Console.WriteLine(Rank());
        }

        private void InitMap(int size)
        {
            this.size = size;
            this.map = new char[size, size];

            int x = 0;
            int y = 0;
            foreach (string row in GetMultiInput()) // "Example"
            {
                foreach (char item in row)
                {
                    this.map[x, y] = item;
                    x += 1;
                }
                y += 1;
                x = 0;
            } 
        }

        private void Print()
        {
            Console.WriteLine(string.Format("- Turn {0}", this.turn));
            for (int y = 0; y < this.size; y++)
            {
                for (int x = 0; x < this.size; x++)
                {
                    Console.Write(this.map[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine(string.Empty.PadLeft(size, '-'));
        }

        private void Update()
        {
            char[,] newMap = new char[size, size];
            for (int y = 0; y < this.size; y++)
            {
                for (int x = 0; x < this.size; x++)
                {
                    int count = CountAround(x, y);
                    if (this.map[x, y] == '#' && count != 1)
                        newMap[x, y] = '.';
                    else if (this.map[x, y] == '.' && (count == 1 || count == 2))
                        newMap[x, y] = '#';
                    else
                        newMap[x, y] = map[x, y];
                }
            }
            this.previous.Add(this.map);
            this.map = newMap;
            this.turn += 1;
        }

        private int CountAround(int x, int y)
        {
            int count = 0;
            if (x > 0 && this.map[x - 1, y] == '#')
                count += 1;
            if (x < (this.size - 1) && this.map[x + 1, y] == '#')
                count += 1;
            if (y > 0 && this.map[x, y - 1] == '#')
                count += 1;
            if (y < (this.size - 1) && this.map[x, y + 1] == '#')
                count += 1;
            return count;
        }

        private bool MapEqual(char[,] array)
        {
            for (int y = 0; y < this.size; y++)
            {
                for (int x = 0; x < this.size; x++)
                {
                    if (this.map[x, y] != array[x, y])
                        return false;
                }
            }
            return true;
        }

        private void Run()
        {
            while(!stop)
            {
                Update();
                Print();

                foreach (var prev in this.previous)
                {
                    if (MapEqual(prev))
                        this.stop = true;
                }
                //Thread.Sleep(interval);
                //Console.Write("Stop ? ");
                //if (Console.ReadLine() == "O")
                //    stop = true;
                //Console.WriteLine();
            }
        }

        private int Rank()
        {
            int rank = 0;
            for (int y = 0; y < this.size; y++)
            {
                for (int x = 0; x < this.size; x++)
                {
                    if (this.map[x, y] == '#')
                        rank += (int)Math.Pow(2, y * this.size + x);
                }
            }
            return rank;
        }


    }
}
