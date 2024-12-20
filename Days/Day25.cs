using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;
using static advent2024.Days.Day13;
using System.ComponentModel;
using System.Reflection;

namespace advent2024.Days
{
    public static class Day25
    {
        private static readonly string InputFile = @"C:\aoc\2024\day25\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day25\output.txt";
        public static void SolvePart1()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int result = 0;
            stopwatch.Stop();
            Console.WriteLine($"25*1 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }
        public static void SolvePart2()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int result = 0;
            stopwatch.Stop();
            Console.WriteLine($"25*2 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }
    }

}
