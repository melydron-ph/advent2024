using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;

namespace advent2024.Days
{
    public static class Day16
    {
        private static readonly string InputFile = @"C:\aoc\2024\day16\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day16\output.txt";
        public static void SolvePart1()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int mapX = lines[0].Length;
            int mapY = lines.Count();
            char[,] map = new char[mapX, mapY];
            int startX = 0;
            int startY = 0;
            int destX = 0;
            int destY = 0;
            for (int i = 0; i < mapY; i++)
            {
                string line = lines[i];
                for (int j = 0; j < mapX; j++)
                {
                    map[i, j] = line[j];
                    if (map[i, j] == 'S')
                    {
                        startX = i;
                        startY = j;
                    }
                    else if (map[i, j] == 'E')
                    {
                        destX = i;
                        destY = j;
                    }
                }
            }
            Point startP = new Point(startX, startY);
            Point endP = new Point(destX, destY);
            int result = FindShortestPath(map, startP, endP, true);
            stopwatch.Stop();
            Console.WriteLine($"16*1 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");

        }
        public static void SolvePart2()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int mapX = lines[0].Length;
            int mapY = lines.Count();
            char[,] map = new char[mapX, mapY];
            int startX = 0;
            int startY = 0;
            int destX = 0;
            int destY = 0;
            for (int i = 0; i < mapY; i++)
            {
                string line = lines[i];
                for (int j = 0; j < mapX; j++)
                {
                    map[i, j] = line[j];
                    if (map[i, j] == 'S')
                    {
                        startX = i;
                        startY = j;
                    }
                    else if (map[i, j] == 'E')
                    {
                        destX = i;
                        destY = j;
                    }
                }
            }
            Point startP = new Point(startX, startY);
            Point endP = new Point(destX, destY);
            List<List<Point>> allPaths = FindAllShortestPaths(map, startP, endP, true);
            HashSet<Point> result = new HashSet<Point>();
            foreach (var path in allPaths)
            {
                foreach (var point in path)
                {
                    result.Add(point);
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"16*2 -- {result.Count} ({stopwatch.ElapsedMilliseconds} ms)");
        }

    }
}
