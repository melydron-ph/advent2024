using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;

namespace advent2024.Days
{
    public static class Day8
    {
        private static readonly string InputFile = @"C:\aoc\2024\day8\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day8\output.txt";

        private static int rows;
        private static int cols;
        private static char[,]? map;

        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            rows = lines.Count();
            cols = lines[0].Length;
            map = new char[rows, cols];
            Dictionary<char, List<Point>> charGroups = new Dictionary<char, List<Point>>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    char c = lines[i][j];
                    map[i, j] = c;

                    if (c != '.')
                    {
                        if (!charGroups.ContainsKey(c))
                        {
                            charGroups[c] = new List<Point>();
                        }
                        charGroups[c].Add(new Point(i, j));
                    }
                }
            }
            //PrintMap(map);
            List<Point> anticharLocations = new List<Point>();
            foreach (var charGroup in charGroups)
            {
                char c = charGroup.Key;
                //Console.WriteLine($"Checking for {c}");
                List<Point> points = charGroup.Value;
                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = i + 1; j < points.Count; j++)
                    {
                        CheckAndAddLocations(points[i], points[j], anticharLocations);
                    }
                }
            }
            int result = anticharLocations.Count();
            //anticharLocations = anticharLocations.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            //foreach (var point in anticharLocations)
            //{
            //    Console.WriteLine(point);
            //}
            Console.WriteLine($"8*1 -- {result}");
        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            rows = lines.Count();
            cols = lines[0].Length;
            map = new char[rows, cols];
            Dictionary<char, List<Point>> charGroups = new Dictionary<char, List<Point>>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    char c = lines[i][j];
                    map[i, j] = c;

                    if (c != '.')
                    {
                        if (!charGroups.ContainsKey(c))
                        {
                            charGroups[c] = new List<Point>();
                        }
                        charGroups[c].Add(new Point(i, j));
                    }
                }
            }
            //PrintMap(map);
            List<Point> anticharLocations = new List<Point>();
            foreach (var charGroup in charGroups)
            {
                char c = charGroup.Key;
                //Console.WriteLine($"Checking for {c}");
                List<Point> points = charGroup.Value;
                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = i + 1; j < points.Count; j++)
                    {
                        CheckAndAddLocations(points[i], points[j], anticharLocations, true);
                    }
                }
            }
            foreach (var charGroup in charGroups)
            {
                if (charGroup.Value.Count() > 1)
                {
                    foreach(var point in charGroup.Value)
                    {
                        if (!anticharLocations.Contains(point))
                        {
                            anticharLocations.Add(point);
                        }
                    }
                }
            }
            int result = anticharLocations.Count();
            //anticharLocations = anticharLocations.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            //foreach (var point in anticharLocations)
            //{
            //    Console.WriteLine(point);
            //}
            Console.WriteLine($"8*2 -- {result}");
        }

        private static void CheckAndAddLocations(Point p1, Point p2, List<Point> anticharLocations, bool part2 = false)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            Point forwardPoint = new Point(p2.X + dx, p2.Y + dy);
            if (part2)
            {
                while (true)
                {
                    CheckAndAddPoint(forwardPoint, anticharLocations);
                    forwardPoint.X += dx;
                    forwardPoint.Y += dy;
                    if (!WithinBounds(forwardPoint))
                        break;
                }
            }
            else
            {
                CheckAndAddPoint(forwardPoint, anticharLocations);
            }

            Point backwardPoint = new Point(p1.X - dx, p1.Y - dy);
            if (part2)
            {
                while (true)
                {
                    CheckAndAddPoint(backwardPoint, anticharLocations);
                    backwardPoint.X -= dx;
                    backwardPoint.Y -= dy;
                    if (!WithinBounds(backwardPoint))
                        break;
                }
            }
            else
            {
                CheckAndAddPoint(backwardPoint, anticharLocations);
            }
        }

        private static void CheckAndAddPoint(Point p, List<Point> anticharLocations)
        {

            if (WithinBounds(p) && !anticharLocations.Contains(p))
            {
                anticharLocations.Add(p);
            }
        }

        private static bool LinePossible(Point p1, Point p2)
        {
            if (p1.Y == p2.Y)
                return true;
            if (p1.X == p2.X)
                return true;
            int dx = Math.Abs(p2.X - p1.X);
            int dy = Math.Abs(p2.Y - p1.Y);
            if (dx == dy)
                return true;
            return false;
        }

        private static bool WithinBounds(Point point)
        {
            if (point.X >= 0 && point.Y >= 0 && point.X < rows && point.Y < cols)
                return true;
            return false;
        }

    }
}
