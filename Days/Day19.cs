using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;
using static advent2024.Days.Day13;
using System.ComponentModel;

namespace advent2024.Days
{
    public static class Day19
    {
        private static readonly string InputFile = @"C:\aoc\2024\day19\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day19\output.txt";
        public static void SolvePart1()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            var (patterns, towels) = GetPatternsAndTowels(InputFile);
            int result = 0;

            foreach (string towel in towels)
            {
                //Console.WriteLine($"**{towel}");
                if (CanMakeTowel(towel, patterns))
                {
                    result++;
                }

            }
            stopwatch.Stop();
            Console.WriteLine($"19*1 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }
        public static void SolvePart2()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            var (patterns, towels) = GetPatternsAndTowels(InputFile);
            long result = 0;
            foreach (string towel in towels)
            {
                result += WaysToMakeTowel(towel, patterns);
                //Console.WriteLine($"**{towel} -- {result} ({stopwatch.ElapsedMilliseconds} ms)");

            }
            stopwatch.Stop();
            Console.WriteLine($"19*2 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }

        private static (List<string> patterns, List<string> towels) GetPatternsAndTowels(string inputFile)
        {
            List<string> patterns = new List<string>();
            List<string> towels = new List<string>();
            string[] lines = File.ReadAllLines(InputFile);
            patterns = lines[0].Split(',').Select(p => p.Trim()).ToList();
            towels = lines.Skip(1).Where(line => !string.IsNullOrWhiteSpace(line)).Select(t => t.Trim()).ToList();
            return (patterns, towels);
        }

        private static bool CanMakeTowel(string towel, List<string> patterns)
        {
            foreach (string pattern in patterns)
            {
                if (pattern.Length > towel.Length)
                    continue;
                if (towel.StartsWith(pattern))
                {
                    if (pattern.Length == towel.Length)
                        return true;
                    string remTowel = towel.Substring(pattern.Length);
                    if (CanMakeTowel(remTowel, patterns))
                        return true;
                }
            }
            return false;
        }


        private static Dictionary<string, long> towelsChecked = new Dictionary<string, long>();
        private static long WaysToMakeTowel(string towel, List<string> patterns)
        {
            long total = 0;

            if (towelsChecked.ContainsKey(towel))
                return towelsChecked[towel];

            foreach (string pattern in patterns)
            {

                if (pattern.Length > towel.Length)
                    continue;

                if (towel.StartsWith(pattern))
                {
                    if (pattern.Length == towel.Length)
                        total++;
                    else
                    {
                        string remTowel = towel.Substring(pattern.Length);
                        total += WaysToMakeTowel(remTowel, patterns);
                    }
                }
            }
            towelsChecked[towel] = total;
            return total;
        }
    }
}
