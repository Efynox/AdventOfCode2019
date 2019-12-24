using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCodeApp.Day23
{
    class Paquet
    {
        public long X { get; set; }
        public long Y { get; set; }
    }

    class Main : AbstractMain
    {
        private List<Intcode> computers;
        private Dictionary<int, Queue<Paquet>> queues;
        private bool stop = false;

        private Paquet NATPaquet;
        private Paquet NATPrevious;

        override public void Execute()
        {
            Part1();

            Part2();
        }

        private void Part1()
        {
            InitComputers(50);

            Process();
        }

        private void Part2()
        {

        }

        private void InitComputers(int quantity)
        {
            string input = GetSingleInput();
            this.computers = new List<Intcode>();
            this.queues = new Dictionary<int, Queue<Paquet>>();
            for (int i = 0; i < quantity; i++)
            {
                computers.Add(new Intcode(input));
                computers[i].Execute(new List<long> { i });
                queues.Add(i, new Queue<Paquet>());
            }
        }

        private void Send(List<long> data)
        {
            if (data[0] == 255)
            {
                NATPrevious = NATPaquet;
                NATPaquet = new Paquet { X = data[1], Y = data[2] };
                //stop = true;
            }
            else
            {
                queues[(int)data[0]].Enqueue(new Paquet { X = data[1], Y = data[2] });
            }
        }

        private void Receive(int address)
        {
            if (queues[address].Count == 0)
            {
                computers[address].Execute(new List<long> { -1 });
            }
            else
            {
                Paquet paquet = queues[address].Dequeue();
                computers[address].Execute(new List<long> { paquet.X, paquet.Y });
            }
        }

        private void Process()
        {
            while(!stop)
            {
                for (int i = 0; i < this.computers.Count; i++)
                {
                    Receive(i);
                    for (int j = 0; j < computers[i].Outputs.Count  / 3; j++)
                    {
                        Send(computers[i].Outputs.GetRange(j * 3, 3));
                    }
                }
                NATCheck();
            }
        }

        private void NATCheck()
        {
            if (this.queues.Values.ToList().TrueForAll(q => q.Count == 0))
            {
                if (NATPrevious != null && NATPaquet.Y == NATPrevious.Y)
                {
                    Console.WriteLine(NATPaquet.Y);
                    stop = true;
                }
                Send(new List<long> { 0, NATPaquet.X, NATPaquet.Y });
            }
        }
    }
}
