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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            int[] numbers = lines.Select(line => int.Parse(line.Trim())).ToArray();
            long result = BananaStuff(numbers, 2000);

            stopwatch.Stop();
            Console.WriteLine($"22*2 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }

        private static long BananaStuff(int[] numbers, long d)
        {
            //numbers[0] = 123;
            _ = SecretNumbersSum(numbers, d);
            var priceChanges = new Dictionary<(int number, int position), int>();
            for (int k = 0; k < numbers.Length; k++)
            {
                int number = numbers[k];
                int prevPrice = 0;

                //string lookup = ("-2,1,-1,3");
                //if (buyOptions.ContainsKey(lookup))
                //{
                //    int num = buyOptions[lookup];
                //    int index = buyOptions.Keys.ToList().IndexOf(lookup);
                //}
                for (int i = 0; i < d; i++)
                {
                    int newPrice = (int)(secretNumbers[(number, i)] % 10);
                    int priceChange = newPrice - prevPrice;
                    priceChanges[(number, i)] = priceChange;
                    prevPrice = newPrice;

                    if (i >= 4)
                    {
                        var sequence = new List<int>();
                        sequence.Add(priceChanges[(number, i - 3)]);
                        sequence.Add(priceChanges[(number, i - 2)]);
                        sequence.Add(priceChanges[(number, i - 1)]);
                        sequence.Add(priceChanges[(number, i)]);
                        string sequenceKey = string.Join(",", sequence);
                        if (!sequenceContributors.ContainsKey(sequenceKey))
                        {
                            sequenceContributors[sequenceKey] = new HashSet<int>();
                        }

                        if (!sequenceContributors[sequenceKey].Contains(number))
                        {
                            //if (sequenceKey == "-2,1,-1,3")
                            //    Console.WriteLine($"{sequenceKey} : {number} : {newPrice}");
                            if (!buyOptions.ContainsKey(sequenceKey))
                                buyOptions[sequenceKey] = newPrice;
                            else
                                buyOptions[sequenceKey] += newPrice;

                            sequenceContributors[sequenceKey].Add(number);
                        }

                    }
                }
            }
            //foreach (var kvp in buyOptions.OrderByDescending(x => x.Value))
            //{
            //    //Console.WriteLine($"Sequence {kvp.Key}: {kvp.Value}");
            //}
            var bestSequence = buyOptions.OrderByDescending(x => x.Value).First();
            return bestSequence.Value;

        }

        private static Dictionary<string, HashSet<int>> sequenceContributors = new();
        private static Dictionary<string, int> buyOptions = new();
        private static Dictionary<(long num, long d), long> secretNumbers = new();
        private static long SecretNumbersSum(int[] numbers, long d)
        {
            long result = 0;
            foreach (var number in numbers)
            {
                secretNumbers[(number, 0)] = number;
                long secNum = number;
                var key = (number, d);
                if (secretNumbers.ContainsKey(key))
                {
                    result += secretNumbers[key];
                    continue;
                }
                for (long i = 1; i <= d; i++)
                {
                    secNum = GetSecretNumber(secNum, i);
                    secretNumbers[(number, i)] = secNum;
                }
                result += secNum;
            }
            return result;
        }

        private static long GetSecretNumber(long num, long d)
        {

            long secNum = num;
            secNum = Mix(secNum, 64 * secNum);
            secNum = Prune(secNum);
            secNum = Mix(secNum, secNum / 32);
            secNum = Prune(secNum);
            secNum = Mix(secNum, secNum * 2048);
            secNum = Prune(secNum);

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
