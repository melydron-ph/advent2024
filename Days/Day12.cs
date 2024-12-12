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
        private static readonly string InputFile = @"C:\aoc\2024\day12\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day12\output.txt";
        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            char[,] map = LinesToCharMap(lines);
            //PrintMap(map);
            List<List<Point>> areas = CharMapToAreas(map);
            int result = 0;
            int rowMax = map.GetLength(0);
            int colMax = map.GetLength(1);
            foreach (List<Point> area in areas)
            {
                int borderCount = 0;
                char c = map[area[0].X, area[0].Y];
                foreach (Point p in area)
                {
                    int row = p.X;
                    int col = p.Y;

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
                //Console.WriteLine($"{c}: {area.Count()} * {borderCount} = {area.Count() * borderCount}");
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
            int rowMax = map.GetLength(0);
            int colMax = map.GetLength(1);
            foreach (List<Point> area in areas)
            {
                int cornerCount = 0;
                char c = map[area[0].X, area[0].Y];
                foreach (Point p in area)
                {
                    int row = p.X;
                    int col = p.Y;

                    int newCorners = 0;
                    bool hasUp = false;
                    bool hasDown = false;
                    bool hasLeft = false;
                    bool hasRight = false;

                    if (((row > 0) && (map[row - 1, col] != c)) || row == 0)
                    {
                        hasUp = true;
                    }
                    if (((col > 0) && (map[row, col - 1] != c)) || col == 0)
                    {
                        hasLeft = true;
                    }
                    if (((row < rowMax - 1) && (map[row + 1, col] != c)) || row == rowMax -1)
                    {
                        hasDown = true;
                    }
                    if (((col < colMax - 1) && (map[row, col + 1] != c)) || col == colMax-1)
                    {
                        hasRight = true;
                    }

                    if (hasUp && hasLeft)
                        newCorners++;
                    if (hasUp && hasRight)
                        newCorners++;
                    if (hasDown && hasLeft)
                        newCorners++;
                    if (hasDown && hasRight)
                        newCorners++;


                    if (row > 0 && col < colMax - 1 && !hasUp && !hasRight) // check above right
                    {
                        char check = map[row - 1, col + 1];
                        if (c != check)
                            newCorners++;
                    }
                    if (row > 0 && col > 0 && !hasUp && !hasLeft) // check above left
                    {
                        char check = map[row - 1, col - 1];
                        if (c != check)
                            newCorners++;
                    }
                    if (row < rowMax - 1 && col < colMax - 1 && !hasDown && !hasRight) // check bottom right
                    {
                        char check = map[row + 1, col + 1];
                        if (c != check)
                            newCorners++;
                    }
                    if (row < rowMax - 1 && col > 0 && !hasDown && !hasLeft) // check bottom left
                    {
                        char check = map[row + 1, col - 1];
                        if (c != check)
                            newCorners++;
                    }
                    if (newCorners > 0)
                        //Console.WriteLine($"Point: {p.X},{p.Y} has: {newCorners}");
                    cornerCount += newCorners;
                }
                //Console.WriteLine($"{c}: {area.Count()} * {cornerCount} = {area.Count() * cornerCount}");
                result += area.Count() * cornerCount;
            }

            Console.WriteLine($"12*2 -- {result}");
        }

    }
}
