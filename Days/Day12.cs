using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;

namespace advent2024.Days
{
    public static class Day12
    {
        private static readonly string InputFile = @"C:\aoc\2024\day12\test.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day12\output.txt";
        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            char[,] map = LinesToCharMap(lines);
            //PrintMap(map);
            List<List<Point>> areas = CharMapToAreas(map);
            int result = 0;
            foreach (List<Point> area in areas)
            {
                int borderCount = 0;
                char c = map[area[0].X, area[0].Y];
                foreach (Point p in area)
                {
                    int row = p.X;
                    int col = p.Y;
                    int rowMax = map.GetLength(0);
                    int colMax = map.GetLength(1);
                    if (row == 0 || row == rowMax - 1)
                        borderCount++;
                    if (col == 0 || col == colMax - 1)
                        borderCount++;
                    if ((row > 0) && (map[row - 1, col] != c))
                        borderCount++;
                    if ((col > 0) && (map[row, col - 1] != c))
                        borderCount++;
                    if ((row < rowMax - 1) && (map[row + 1, col] != c))
                        borderCount++;
                    if ((col < colMax - 1) && (map[row, col + 1] != c))
                        borderCount++;
                }
                //Console.WriteLine($"{map[area[0].X, area[0].Y]}: {area.Count()}: {borderCount}");
                result += (area.Count() * borderCount);

            }
            Console.WriteLine($"12*1 -- {result}");

        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            char[,] map = LinesToCharMap(lines);
            //PrintMap(map);
            List<List<Point>> areas = CharMapToAreas(map);
            int result = 0;
            foreach (List<Point> area in areas)
            {
                int borderCount = 0;
                char c = map[area[0].X, area[0].Y];
                var pointsGroupedByX = area.GroupBy(x => x.X);

                foreach (var g in pointsGroupedByX)
                {
                    Console.WriteLine($"Group with X = {g.Key}:");
                    foreach (var point in g)
                    {
                        Console.WriteLine($"  Point: [{point.X}, {point.Y}]");
                    }
                    Console.WriteLine();
                }

            }


            Console.WriteLine($"12*2 -- {result}");
        }

    }
}
