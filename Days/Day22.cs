using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;
using static advent2024.Days.Day13;
using System.ComponentModel;
using System.Reflection;
using System;

namespace advent2024.Days
{
    public static class Day22
    {
        private static readonly string InputFile = @"C:\aoc\2024\day22\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day22\output.txt";
        public static void SolvePart1()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int[] numbers = lines.Select(line => int.Parse(line.Trim())).ToArray();

            long result = SecretNumbersSum(numbers, 2000);
            stopwatch.Stop();
            Console.WriteLine($"22*1 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }
        public static void SolvePart2()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int result = 0;
            stopwatch.Stop();
            Console.WriteLine($"22*2 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }


        private static Dictionary<(long num, long d), long> secretNumbers = new();
        private static long SecretNumbersSum(int[] numbers, long d)
        {
            long result = 0;
            foreach (var number in numbers)
            {
                long secNum = number;
                var key = (secNum, d);
                if (secretNumbers.ContainsKey(key))
                {
                    result += secretNumbers[key];
                    continue;
                }
                for (long i = 1; i <= d; i++)
                {
                    secNum = GetSecretNumber(secNum, i);
                }
                result += secNum;
            }
            return result;
        }

        private static long GetSecretNumber(long num, long d)
        {
            var key = (num, d);
            if (secretNumbers.ContainsKey(key))
                return secretNumbers[key];

            long secNum = num;
            secNum = Mix(secNum, 64 * secNum);
            secNum = Prune(secNum);
            secNum = Mix(secNum, secNum / 32);
            secNum = Prune(secNum);
            secNum = Mix(secNum, secNum * 2048);
            secNum = Prune(secNum);

            secretNumbers[(num, d)] = secNum;
            return secNum;
        }

        private static long Mix(long value, long num)
        {
            return (value ^ num);
        }

        private static long Prune(long num)
        {
            return (num % 16777216);
        }

    }
}
