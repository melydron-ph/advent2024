using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;

namespace advent2024.Days
{
    public static class Day10
    {
        private static readonly string InputFile = @"C:\aoc\2024\day10\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day10\output.txt";

        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int rows = lines.Count();
            int cols = lines[0].Length;
            int[,] map = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    map[i, j] = lines[i][j] - '0';
                }
            }
            List<List<(int, int)>> allPaths = FindAllPaths(map);
            Dictionary<(int, int), List<(int,int)>> trailHeads = new Dictionary<(int, int), List<(int,int)>>();
            foreach (var pathList in allPaths)
            {
                (int, int) firstP = pathList[0];
                (int, int) lastP = pathList[pathList.Count - 1];
                if (!trailHeads.ContainsKey(firstP))
                {
                    trailHeads[firstP] = new List<(int,int)>();
                }
                if (!trailHeads[firstP].Contains(lastP))
                {
                    trailHeads[firstP].Add(lastP);
                }
            }

            int result = 0;
            foreach (var trailHead in trailHeads)
            {
                result += trailHead.Value.Count();
            }
            Console.WriteLine($"10*1 -- {result}");

        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int rows = lines.Count();
            int cols = lines[0].Length;
            int[,] map = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    map[i, j] = lines[i][j] - '0';
                }
            }
            List<List<(int, int)>> allPaths = FindAllPaths(map);

            int result = allPaths.Count(); ;

            Console.WriteLine($"10*2 -- {result}");
        }

        private static List<List<(int, int)>> FindAllPaths(int[,] map)
        {
            List<List<(int, int)>> allPaths = new List<List<(int, int)>>();
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (map[i, j] == 0)
                    {
                        List<(int, int)> newPath = new List<(int, int)> { (i, j) };
                        DFS(i, j, map, newPath, allPaths);
                    }
                }
            }
            return allPaths;
        }

        private static void DFS(int row, int col, int[,] map, List<(int, int)> currentPath, List<List<(int, int)>> allPaths)
        {
            if (map[row, col] == 9)
            {
                allPaths.Add(new List<(int, int)>(currentPath));
                return;
            }

            int[] dx = { -1, 0, 1, 0 };  // up, right, down, left
            int[] dy = { 0, 1, 0, -1 };  // up, right, down, left

            int currentValue = map[row, col];

            for (int i = 0; i < 4; i++)
            {
                int newRow = row + dx[i];
                int newCol = col + dy[i];
                int nextValue = currentValue + 1;

                if (InBounds(newRow, newCol, map) && map[newRow, newCol] == nextValue && !currentPath.Contains((newRow, newCol)))
                {
                    currentPath.Add((newRow, newCol));
                    DFS(newRow, newCol, map, currentPath, allPaths);
                    currentPath.RemoveAt(currentPath.Count - 1);
                }
            }
        }

        private static bool InBounds(int row, int col, int[,] map)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            return row >= 0 && row < rows && col >= 0 && col < cols;
        }
    }
}
