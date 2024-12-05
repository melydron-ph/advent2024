using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using static advent2024.Helper;

namespace advent2024.Days
{
    public static class Day5
    {
        private static readonly string InputFile = @"C:\aoc\2024\day5\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day5\output.txt";

        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);

            string file = File.ReadAllText(InputFile);
            string[] fileBlocks = file.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<(int, int)> rules = new List<(int, int)>();
            foreach (string line in fileBlocks[0].Split("\n"))
            {
                string[] ruleSplit = line.Split("|");
                rules.Add((int.Parse(ruleSplit[0]), int.Parse(ruleSplit[1])));
            }

            List<List<int>> updates = new List<List<int>>();
            foreach (string line in fileBlocks[1].Split("\n", StringSplitOptions.RemoveEmptyEntries))
            {
                List<int> update = new List<int>();
                string[] numSplit = line.Split(",");
                foreach (string num in numSplit)
                {
                    update.Add(int.Parse(num));
                }
                updates.Add(update);
            }

            int result = 0;
            foreach (var update in updates)
            {
                bool rightOrder = true;
                foreach (var rule in rules)
                {
                    if (update.Contains(rule.Item1) && update.Contains(rule.Item2))
                    {
                        if (update.IndexOf(rule.Item2) < update.IndexOf(rule.Item1))
                        {
                            rightOrder = false;
                            break;
                        }
                    }
                }
                if (rightOrder)
                    result += update[update.Count / 2];
            }
            Console.WriteLine($"5*1 -- {result}");
        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);

            string file = File.ReadAllText(InputFile);
            string[] fileBlocks = file.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<(int, int)> rules = new List<(int, int)>();
            foreach (string line in fileBlocks[0].Split("\n"))
            {
                string[] ruleSplit = line.Split("|");
                rules.Add((int.Parse(ruleSplit[0]), int.Parse(ruleSplit[1])));
            }

            List<List<int>> updates = new List<List<int>>();
            foreach (string line in fileBlocks[1].Split("\n", StringSplitOptions.RemoveEmptyEntries))
            {
                List<int> update = new List<int>();
                string[] numSplit = line.Split(",");
                foreach (string num in numSplit)
                {
                    update.Add(int.Parse(num));
                }
                updates.Add(update);
            }

            int result = 0;
            foreach (var update in updates)
            {
                bool rightOrder = true;
                foreach (var rule in rules)
                {
                    if (update.Contains(rule.Item1) && update.Contains(rule.Item2))
                    {
                        if (update.IndexOf(rule.Item2) < update.IndexOf(rule.Item1))
                        {
                            rightOrder = false;
                            break;
                        }
                    }
                }
                if (!rightOrder)
                {
                    result += FixedUpdateResult(update, rules);
                }
            }

            Console.WriteLine($"5*2 -- {result}");
        }

        private static int FixedUpdateResult(List<int> update, List<(int, int)> rules)
        {
            update = FixUpdate(update, rules);
            return update[update.Count / 2];
        }

        private static List<int> FixUpdate(List<int> update, List<(int, int)> rules)
        {
            bool fixing = true;
            while (fixing)
            {
                fixing = false;
                foreach (var rule in rules)
                {
                    if (update.Contains(rule.Item1) && update.Contains(rule.Item2))
                    {
                        if (update.IndexOf(rule.Item2) < update.IndexOf(rule.Item1))
                        {
                            fixing = true;
                            int index1 = update.IndexOf(rule.Item1);
                            int index2 = update.IndexOf(rule.Item2);

                            int temp = update[index1];
                            update[index1] = update[index2];
                            update[index2] = temp;
                        }
                    }
                }
            }

            return update;
        }

    }

}
