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
            List<int[]> locks;
            List<int[]> keys;
            int targetSum;
            GetLocksAndKeys(InputFile, out locks, out keys, out targetSum);
            int result = 0;
            foreach (int[] l in locks)
            {
                foreach (int[] k in keys)
                {
                    if (LockKeyMatch(l, k, targetSum))
                        result++;
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"25*1 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }
        public static void SolvePart2()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            string result = "Merry Christmas!";
            stopwatch.Stop();
            Console.WriteLine($"25*2 -- {result}");
        }

        private static void GetLocksAndKeys(string inputFile, out List<int[]> locks, out List<int[]> keys, out int targetSum)
        {
            string file = File.ReadAllText(inputFile);
            string[] fileBlocks = file.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            locks = new();
            keys = new();
            targetSum = fileBlocks[0].Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).Count() - 2;
            foreach (string fileBlock in fileBlocks)
            {
                if (fileBlock[0] == '#')
                {
                    locks.Add(GetLock(fileBlock));
                }
                else
                {
                    keys.Add(GetKey(fileBlock));
                }
            }
        }

        private static int[] GetLock(string fileBlock)
        {
            string[] lines = fileBlock.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int length = lines[0].Length;
            int[] heights = new int[length];
            for (int i = 1; i < lines.Count(); i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (lines[i][j] == '#')
                        heights[j]++;
                }
            }
            return heights;
        }
        private static int[] GetKey(string fileBlock)
        {
            string[] lines = fileBlock.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int length = lines[0].Length;
            int[] heights = new int[length];
            for (int i = lines.Count() - 2; i >= 0; i--)
            {
                for (int j = 0; j < length; j++)
                {
                    if (lines[i][j] == '#')
                        heights[j]++;
                }
            }
            return heights;
        }

        private static bool LockKeyMatch(int[] l, int[] k, int target)
        {
            if (l.Length != k.Length) return false;
            for (int i = 0; i < l.Length; i++)
            {
                if (l[i] + k[i] > target) return false;
            }
            return true;
        }

    }

}
