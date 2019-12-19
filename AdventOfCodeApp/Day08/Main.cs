using AdventOfCodeApp.Day05;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCodeApp.Day08
{
    class Main : AbstractMain
    {
        const int WIDTH = 25;
        const int HEIGHT = 6;

        private int rowsCount;
        private List<List<List<int>>> layers;


        override public void Execute()
        {
            string input = GetSingleInput();

            BuildImage(input);

            var layer = FindLayerWithMinimal(0);

            Console.WriteLine(CountDigitInLayer(1, layer) * CountDigitInLayer(2, layer));

            DisplayImage();
        }

        private void BuildImage(string input)
        {
            this.rowsCount = 0;
            int rowIdx = 0;
            int colIdx = 0;
            int layerIdx = 0;

            layers = new List<List<List<int>>>();

            for (int i = 0; i < input.Length; i++)
            {
                if (colIdx == WIDTH)
                {
                    rowIdx += 1;
                    colIdx = 0;
                    if (rowIdx == HEIGHT)
                    {
                        layerIdx += 1;
                        rowIdx = 0;
                    }
                }

                if (rowIdx == 0 && colIdx == 0)
                    layers.Add(new List<List<int>>());

                if (rowIdx == 0)
                    layers[layerIdx].Add(new List<int>());

                layers[layerIdx][rowIdx].Add(int.Parse(input[i].ToString()));
                colIdx += 1;
            }
        }

        private int CountDigitInLayer(int digit, List<List<int>> layer)
        {
            int count = 0;
            foreach (var row in layer)
            {
                foreach (var pixel in row)
                {
                    if (pixel == digit)
                        count += 1;
                }
            }
            return count;
        }

        private List<List<int>> FindLayerWithMinimal(int digit)
        {
            int min = int.MaxValue;
            int current;
            int index = 0;
            for (int i = 0; i < this.layers.Count; i++)
            {
                current = CountDigitInLayer(digit, layers[i]);
                if (current < min)
                {
                    min = current;
                    index = i;
                }
            }
            return this.layers[index];
        }

        private void DisplayImage()
        {
            bool pixelPrinted;
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    pixelPrinted = false;
                    for (int i = 0; i < this.layers.Count && !pixelPrinted; i++)
                    {
                        switch (this.layers[i][y][x])
                        {
                            case 0:
                                Console.Write(" ");
                                pixelPrinted = true;
                                break;
                            case 1:
                                Console.Write("X");
                                pixelPrinted = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
