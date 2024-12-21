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
    public static class Day21
    {
        private static readonly string InputFile = @"C:\aoc\2024\day21\test.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day21\output.txt";
        public static void SolvePart1()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            string numpadString = $"789\n456\n123\n#0A";
            string[] rows = numpadString.Split('\n');
            char[,] numpad = new char[rows.Length, rows[0].Length];
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rows[i].Length; j++)
                {
                    numpad[i, j] = rows[i][j];
                }
            }
            Dictionary<char, Point> charLocations = new Dictionary<char, Point>();
            for (int i = 0; i < numpad.GetLength(0); i++)
            {
                for (int j = 0; j < numpad.GetLength(1); j++)
                {
                    charLocations[numpad[i, j]] = new Point(i, j);
                }
            }
            HashSet<char> allChars = new HashSet<char>();
            for (int i = 0; i < numpad.GetLength(0); i++)
            {
                for (int j = 0; j < numpad.GetLength(1); j++)
                {
                    if (numpad[i, j] != '#')
                        allChars.Add(numpad[i, j]);
                }
            }

            Dictionary<(char, char), List<Direction>> numpadPaths = new();

            foreach (char c1 in allChars)
            {
                foreach (char c2 in allChars)
                {
                    if (c1 != c2)
                    {
                        Point p1 = charLocations[c1];
                        Point p2 = charLocations[c2];
                        List<Direction> directionalSteps = FindShortestPath(numpad, p1, p2);
                        numpadPaths[(c1, c2)] = directionalSteps;
                    }
                }
            }

            foreach (var path in numpadPaths)
            {
                var (from, to) = path.Key;
                var directions = path.Value.Select(d => d switch
                {
                    Direction.Left => "<",
                    Direction.Right => ">",
                    Direction.Up => "^",
                    Direction.Down => "v",
                    _ => "?"
                });

                Console.WriteLine($"From {from} to {to}: {string.Join("", directions)}");
            }

            int result = 0;
            stopwatch.Stop();
            Console.WriteLine($"21*1 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }
        public static void SolvePart2()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int result = 0;
            stopwatch.Stop();
            Console.WriteLine($"21*2 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }

        //private class ShortestPath
        //{
        //    public char Orig { get; set; }
        //    public char Dest { get; set; }
        //    public int Steps { get; set; }
        //    public List<Direction>? DirectionalSteps { get; set; }

        //}


        private static List<Direction>? FindShortestPath(char[,] map, Point start, Point end)
        {
            PriorityQueue<State, int> queue = new PriorityQueue<State, int>();
            HashSet<(Point, Direction)> visited = new HashSet<(Point, Direction)>();

            foreach (Direction startDir in Enum.GetValues<Direction>())
            {
                Point offset = GetNextPoint(startDir);
                Point nextPos = new Point(
                    start.X + offset.X,
                    start.Y + offset.Y
                );

                if (IsValidMove(map, nextPos))
                {
                    var initialPath = new List<Point> { start };
                    var initialDirections = new List<Direction>();
                    State initialState = new State(start, startDir, 0, initialPath, initialDirections);
                    queue.Enqueue(initialState, 0);
                }
            }

            while (queue.Count > 0)
            {
                State currentState = queue.Dequeue();
                (Point, Direction) stateKey = (currentState.Position, currentState.Direction);

                if (visited.Contains(stateKey))
                    continue;

                visited.Add(stateKey);

                if (currentState.Position.Equals(end))
                    return currentState.DirectionalPath;

                List<Direction> possibleMoves = GetPossibleMoves(currentState.Direction);
                foreach (Direction nextDir in possibleMoves)
                {
                    Point offset = GetNextPoint(nextDir);
                    Point nextPos = new Point(
                        currentState.Position.X + offset.X,
                        currentState.Position.Y + offset.Y
                    );

                    if (!IsValidMove(map, nextPos))
                        continue;

                    int moveCost = 1;

                    int nextCost = currentState.Cost + moveCost;

                    var nextPath = new List<Point>(currentState.Path) { nextPos };
                    var nextDirectionalPath = new List<Direction>(currentState.DirectionalPath) { nextDir };

                    State nextState = new State(nextPos, nextDir, nextCost, nextPath, nextDirectionalPath);

                    if (!visited.Contains((nextPos, nextDir)))
                        queue.Enqueue(nextState, nextState.Cost);
                }
            }

            return new List<Direction>();
        }

    }

}
