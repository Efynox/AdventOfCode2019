using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCodeApp.Day06
{
    class Main : AbstractMain
    {
        private List<string> objects = new List<string>();
        private Dictionary<string, List<string>> links = new Dictionary<string, List<string>>();

        override public void Execute()
        {
            string[] lines = GetMultiInput(); // Day6/Sample1 |  Day6/Sample2 | Day6/Input

            BuildLinks(lines);

            Console.WriteLine(CountLinks());

            Console.WriteLine(CountLinksBetween("YOU", "SAN"));
        }

        private void BuildLinks(string[] lines)
        {
            foreach (var line in lines)
            {
                var link = line.Split(')');

                if (!links.ContainsKey(link[1]))
                    links.Add(link[1], new List<string>());

                links[link[1]].Add(link[0]);

                if (!objects.Contains(link[0]))
                    objects.Add(link[0]);
                if (!objects.Contains(link[1]))
                    objects.Add(link[1]);
            }
        }

        private int CountLinks()
        {
            int size = 0;
            foreach (var obj in objects)
            {
                List<string> related = new List<string>();
                related.Add(obj);
                for (int i = 0; i < related.Count; i++)
                {
                    if (links.ContainsKey(related[i]))
                        related.AddRange(links[related[i]]);
                }
                size += related.Count - 1;
            }
            return size;
        }

        private int CountLinksBetween(string a, string b)
        {
            Dictionary<string, int> relatedA = getRelated(a);
            Dictionary<string, int> relatedB = getRelated(b); ;

            foreach (var relA in relatedA)
            {
                foreach (var relB in relatedB)
                {
                    if (relA.Key == relB.Key)
                        return relA.Value + relB.Value - 2;
                }
            }
            return -1;
        }

        private Dictionary<string, int> getRelated(string obj)
        {
            List<string> related = new List<string>();
            Dictionary<string, int> deepth = new Dictionary<string, int>();
            related.Add(obj);
            deepth.Add(obj, 0);
            for (int i = 0; i < related.Count; i++)
            {
                if (links.ContainsKey(related[i]))
                {
                    foreach (var rel in links[related[i]])
                    {
                        related.Add(rel);
                        deepth.Add(rel, deepth[related[i]] + 1);
                    }
                }
            }
            return deepth;
        }
    }
}
