using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;

namespace advent2024.Days
{
    public static class Day11
    {
        private static readonly string InputFile = @"C:\aoc\2024\day11\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day11\output.txt";

        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            int result = ProcessStones(InputFile, 25);
            Console.WriteLine($"11*1 -- {result}");

        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);
            long result = ProcessStonesWithDictionary(InputFile, 75);
            Console.WriteLine($"11*2 -- {result}");
        }

        private static int ProcessStones(string inputFile, int blinks)
        {
            string line = File.ReadAllText(InputFile);
            List<long> stones = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            //Console.WriteLine($"Initial: ");
            File.AppendAllText(OutputFile, $"Initial: \n");
            //Console.WriteLine(String.Join(" ", stones));
            File.AppendAllText(OutputFile, $"{String.Join(" ", stones)}\n");
            for (int i = 0; i < blinks; i++)
            {
                List<long> newStones = new List<long>();
                for (int j = 0; j < stones.Count; j++)
                {
                    if (stones[j] == 0)
                    {
                        newStones.Add(1);
                        continue;
                    }
                    int stoneLength = stones[j].ToString().Length;
                    if (stoneLength % 2 == 0)
                    {
                        long left = long.Parse(stones[j].ToString().Substring(0, stoneLength / 2));
                        long right = long.Parse(stones[j].ToString().Substring(stoneLength / 2));
                        newStones.Add(left);
                        newStones.Add(right);
                    }
                    else
                    {
                        newStones.Add(stones[j] * 2024);
                    }
                }
                stones = newStones;
                //Console.WriteLine($"Blink {i + 1}: {stones.Count()}");
                //File.AppendAllText(OutputFile, $"Blink {i + 1}: \n");
                //Console.WriteLine(String.Join(" ", stones) + "\n");
                //File.AppendAllText(OutputFile, $"{String.Join(" ", stones)}\n");
            }
            return stones.Count();
        }

        private static long ProcessStonesWithDictionary(string inputFile, int blinks)
        {
            string line = File.ReadAllText(InputFile);
            List<long> stones = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            //Console.WriteLine($"Initial: ");
            //File.AppendAllText(OutputFile, $"Initial: \n");
            //Console.WriteLine(String.Join(" ", stones));
            //File.AppendAllText(OutputFile, $"{String.Join(" ", stones)}\n");

            Dictionary<long, long> stoneDictionary = new Dictionary<long, long>();
            foreach (long stone in stones)
            {
                if (stoneDictionary.ContainsKey(stone))
                {
                    stoneDictionary[stone]++;
                }
                else
                {
                    stoneDictionary.Add(stone, 1);
                }
            }
            for (int i = 0; i < blinks; i++)
            {
                Dictionary<long, long> newDictionary = new Dictionary<long, long>();
                foreach (KeyValuePair<long, long> groupedStones in stoneDictionary)
                {
                    long stoneNum = groupedStones.Key;
                    long count = groupedStones.Value;
                    if (stoneNum == 0)
                    {
                        if (newDictionary.ContainsKey(1))
                        {
                            newDictionary[1] += count;
                        }
                        else
                        {
                            newDictionary.Add(1, count);
                        }
                        continue;
                    }
                    int stoneLength = stoneNum.ToString().Length;
                    if (stoneLength % 2 == 0)
                    {
                        long left = long.Parse(stoneNum.ToString().Substring(0, stoneLength / 2));
                        long right = long.Parse(stoneNum.ToString().Substring(stoneLength / 2));
                        if (newDictionary.ContainsKey(left))
                        {
                            newDictionary[left] += count;
                        }
                        else
                        {
                            newDictionary.Add(left, count);
                        }
                        if (newDictionary.ContainsKey(right))
                        {
                            newDictionary[right] += count;
                        }
                        else
                        {
                            newDictionary.Add(right, count);
                        }
                    }
                    else
                    {
                        long newNum = stoneNum * 2024;
                        if (newDictionary.ContainsKey(newNum))
                        {
                            newDictionary[newNum] += count;
                        }
                        else
                        {
                            newDictionary.Add(newNum, count);
                        }
                    }
                }
                stoneDictionary = newDictionary;

                //Console.WriteLine($"Blink {i + 1}: {stoneDictionary.Values.Sum()}");
                //File.AppendAllText(OutputFile, $"Blink {i + 1}: \n");
                //Console.WriteLine(String.Join(" ", stones) + "\n");
                //File.AppendAllText(OutputFile, $"Blink {i + 1}: {stoneDictionary.Values.Sum()}\n");
            }


            return stoneDictionary.Values.Sum();
        }
    }
}
