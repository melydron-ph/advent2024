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
            int rows = lines.Length;
            int cols = lines[0].Length;
            bool[,] visited = new bool[rows, cols];
            char[,] map = new char[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    map[i, j] = lines[i][j];
                    visited[i, j] = false;
                }
            }
            //PrintMap(map);

            List<List<Point>> areas = new List<List<Point>>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (!visited[i, j])
                    {
                        List<Point> area = new List<Point>();
                        FloodFill(map, i, j, map[i, j], area, visited);
                        if (area.Count > 0)
                        {
                            areas.Add(area);
                        }
                    }
                }
            }

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
            int result = 0;
            Console.WriteLine($"12*2 -- {result}");
        }


        private static void FloodFill(char[,] map, int row, int col, char target, List<Point> region, bool[,] visited)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            if (row < 0 || row >= rows || col < 0 || col >= cols ||
                visited[row, col] || map[row, col] != target)
            {
                return;
            }

            visited[row, col] = true;
            region.Add(new Point(row, col));

            FloodFill(map, row - 1, col, target, region, visited); // Up
            FloodFill(map, row + 1, col, target, region, visited); // Down
            FloodFill(map, row, col - 1, target, region, visited); // Left
            FloodFill(map, row, col + 1, target, region, visited); // Right
        }
    }
}
