using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;
using static advent2024.Days.Day13;

namespace advent2024.Days
{
    public static class Day18
    {
        private static readonly string InputFile = @"C:\aoc\2024\day18\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day18\output.txt";
        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            List<Point> bytes = GetBytesFromFile(InputFile);
            int mapRows = 71;
            int mapCols = 71;
            int corrupt = 1024;
            if (InputFile.Contains("test.txt"))
            {
                mapRows = 7;
                mapCols = 7;
                corrupt = 12;
            }
            char[,] map = GetMapFromBytes(bytes, corrupt, mapRows, mapCols);
            //PrintMap(map);
            Point start = new Point(0, 0);
            Point end = new Point(mapRows - 1, mapCols - 1);
            int result = FindShortestPath(map, start, end, false);
            Console.WriteLine($"18*1 -- {result}");
        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);
            List<Point> bytes = GetBytesFromFile(InputFile);
            int mapRows = 71;
            int mapCols = 71;
            int corrupt = 1024;
            if (InputFile.Contains("test.txt"))
            {
                mapRows = 7;
                mapCols = 7;
                corrupt = 12;
            }
            char[,] map = GetMapFromBytes(bytes, corrupt, mapRows, mapCols);
            //PrintMap(map);
            Point start = new Point(0, 0);
            Point end = new Point(mapRows - 1, mapCols - 1);

            int left = 0;
            int right = bytes.Count();
            int result = -1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                map = GetMapFromBytes(bytes, mid, mapRows, mapCols);
                if (FindShortestPath(map, start, end, false) < 0)
                {
                    result = mid;
                    right = mid - 1;
                }
                else
                    left = mid + 1;
            }
            Point p = bytes[result-1];
            Console.WriteLine($"18*2 -- {p.X},{p.Y}");
        }

        private static List<Point> GetBytesFromFile(string inputFile)
        {
            List<Point> bytes = new List<Point>();
            string[] lines = File.ReadAllText(InputFile).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                var nums = Regex.Matches(line, @"\d+").Select(m => int.Parse(m.Value)).ToList();
                bytes.Add(new Point(nums[0], nums[1]));
            }
            return bytes;
        }

        private static char[,] GetMapFromBytes(List<Point> bytes, int corrupt, int mapRows, int mapCols)
        {
            char[,] map = new char[mapRows, mapCols];
            for (int i = 0; i < mapRows; i++)
            {
                for (int j = 0; j < mapCols; j++)
                {
                    map[i, j] = '.';
                }
            }
            for (int i = 0; i < corrupt; i++)
            {
                Point p = bytes[i];
                map[p.Y, p.X] = '#';
            }
            return map;

        }
    }
}
