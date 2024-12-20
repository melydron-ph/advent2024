using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using static advent2024.Helper;

namespace advent2024.Days
{
    public static class Day04
    {
        private static readonly string InputFile = @"C:\aoc\2024\day4\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day4\output.txt";

        public static void SolvePart1()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);

            string[] lines = File.ReadAllLines(InputFile);
            int result = 0;
            int linesLength = lines[0].Length;
            int linesCount = lines.Length;

            for (int i = 0; i < linesCount; i++)
            {
                for (int j = 0; j < linesLength; j++)
                {
                    char c = lines[i][j];
                    if (c == 'X')
                    {
                        //Console.WriteLine($"Found X at {i},{j}");
                        result += FindWord("XMAS", lines, i, j);
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"04*1 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }

        public static void SolvePart2()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);

            string[] lines = File.ReadAllLines(InputFile);
            int result = 0;
            int linesLength = lines[0].Length;
            int linesCount = lines.Length;

            for (int i = 0; i < linesCount; i++)
            {
                for (int j = 0; j < linesLength; j++)
                {
                    char c = lines[i][j];
                    if (c == 'A')
                    {
                        //Console.WriteLine($"Found X at {i},{j}");
                        result += FindXMAS(lines, i, j);
                    }
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"04*2 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }
    }

}
