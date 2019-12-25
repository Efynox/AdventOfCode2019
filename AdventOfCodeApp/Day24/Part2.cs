using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventOfCodeApp.Day24
{
    class Part2 : AbstractMain
    {
        private int size;
        private Dictionary<int, char[,]> maps;
        private Dictionary<int, char[,]> nextMaps;
        private int levelsCount = 0;
        private int turnMax = 200;
        private int turn = 0;

        override public void Execute()
        {
            InitMap(5);
            Run();

            for (int level = this.maps.Keys.Min(); level <= this.maps.Keys.Max(); level++)
            {
                Print(level);
            }

            Console.WriteLine(Count());
        }

        private void InitMap(int size)
        {
            this.size = size;
            this.maps = new Dictionary<int, char[,]>();
            this.maps[0] = new char[size, size];

            int x = 0;
            int y = 0;
            foreach (string row in GetMultiInput("Part2")) // "Example"
            {
                foreach (char item in row)
                {
                    this.maps[0][x, y] = item;
                    x += 1;
                }
                y += 1;
                x = 0;
            } 
        }

        private void Print(int level)
        {
            Console.WriteLine(string.Format("- Level {0}", level));
            for (int y = 0; y < this.size; y++)
            {
                for (int x = 0; x < this.size; x++)
                {
                    Console.Write(this.maps[level][x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine(string.Empty.PadLeft(size, '-'));
        }

        private void Update()
        {
            nextMaps = new Dictionary<int, char[,]>();
            foreach(var level in this.maps)
            {
                for (int y = 0; y < this.size; y++)
                {
                    for (int x = 0; x < this.size; x++)
                    {
                        int count = CountAround(level.Key, x, y);
                        if (this.maps[level.Key][x, y] == '#' && count != 1)
                            Set(level.Key, x, y, '.');
                        else if ((this.maps[level.Key][x, y] == '.' || this.maps[level.Key][x, y] == ' ') && (count == 1 || count == 2))
                            Set(level.Key, x, y, '#');
                        else
                            Set(level.Key, x, y, level.Value[x, y]);
                    }
                }
            }
            this.maps = this.nextMaps;
            this.turn += 1;
        }

        private int CountAround(int level, int x, int y)
        {
            int count = 0;
            count += CountAt(level, x - 1, y, "right");
            count += CountAt(level, x + 1, y, "left");
            count += CountAt(level, x, y - 1, "bottom");
            count += CountAt(level, x, y + 1, "top");
            return count;
        }

        private int CountAt(int level, int x, int y, string side = "")
        {
            if (!this.maps.ContainsKey(level))
                return 0;

            if (y < 0 && x < 0)
                return CountAt(level - 1, 1, 1);
            else if (y < 0 && x >= size)
                return CountAt(level - 1, 3, 1);
            else if (y < 0)
                return CountAt(level - 1, 2, 1);
            else if (y >= size && x < 0)
                return CountAt(level - 1, 1, 3);
            else if (y >= size && x >= size)
                return CountAt(level - 1, 3, 3);
            else if (y >= size)
                return CountAt(level - 1, 2, 3);
            else if (x < 0)
                return CountAt(level - 1, 1, 2);
            else if (x >= size)
                return CountAt(level - 1, 3, 2);
            else if (x == size / 2 && y == size / 2)
            {
                switch (side)
                {
                    case "top":
                        return CountRow(level + 1, 0);
                    case "bottom":
                        return CountRow(level + 1, size - 1);
                    case "left":
                        return CountCol(level + 1, 0);
                    case "right":
                        return CountCol(level + 1, size - 1);
                    default:
                        break;
                }
            }
            else
                return this.maps[level][x, y] == '#' ? 1 : 0;
            return 0;
        }

        private int CountRow(int level, int y)
        {
            int count = 0;
            for (int x = 0; x < size; x++)
            {
                count += CountAt(level, x, y);
            }
            return count;
        }

        private int CountCol(int level, int x)
        {
            int count = 0;
            for (int y = 0; y < size; y++)
            {
                count += CountAt(level, x, y);
            }
            return count;
        }

        private void Set(int level, int x, int y, char value)
        {
            if (!this.nextMaps.Keys.Contains(level))
                this.nextMaps.Add(level, new char[size, size]);
            this.nextMaps[level][x, y] = value;
        }

        private void Run()
        {
            for (int i = 0; i < turnMax; i++)
            {
                if (i % (size / 2) == 0)
                {
                    levelsCount += 1;
                    AddLevel(levelsCount);
                    AddLevel(-1 * levelsCount);
                }
                Update();
                //Console.WriteLine("iteration " + i);
                //for (int level = this.maps.Keys.Min(); level <= this.maps.Keys.Max(); level++)
                //{
                //    Print(level);
                //}
                //Thread.Sleep(interval);
                //Console.Write("Stop ? ");
                //if (Console.ReadLine() == "O")
                //    stop = true;
                //Console.WriteLine();
            }
        }

        private void AddLevel(int level)
        {
            this.maps.Add(level, new char[size, size]);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (x == 2 && y == 2)
                        this.maps[level][x, y] = '?';
                    else
                        this.maps[level][x, y] = '.';
                }
            }
        }

        private int Count()
        {
            int count = 0;
            foreach (var level in this.maps)
            {
                for (int y = 0; y < this.size; y++)
                {
                    for (int x = 0; x < this.size; x++)
                    {
                        if (level.Value[x, y] == '#')
                            count += 1; ;
                    }
                }
            }
            return count;
        }

    }
}
