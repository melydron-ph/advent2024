﻿using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;

namespace advent2024.Days
{
    public static class Day13
    {
        private static readonly string InputFile = @"C:\aoc\2024\day13\test.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day13\output.txt";
        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int result = 0;
            Console.WriteLine($"13*1 -- {result}");

        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int result = 0;
            Console.WriteLine($"13*2 -- {result}");
        }

    }
}