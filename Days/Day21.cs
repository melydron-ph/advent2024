using System.Drawing;
using static advent2024.Helper;

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
            var numpadPaths = GetPadPaths(numpadString);
            string dirpadString = $"#^A\n<v>";
            var dirpadPaths = GetPadPaths(dirpadString);
            long result = 0;

            string[] codes = File.ReadAllLines(InputFile);
            foreach (string code in codes)
            {
                List<Direction> firstSteps = numpadPaths[('A', code[0])];
                string shortSeq = string.Empty;
                shortSeq += DirectionsToString(firstSteps) + "A";
                for (int i = 0; i < code.Length - 1; i++)
                {
                    char c1 = code[i];
                    char c2 = code[i + 1];
                    if (c1 == c2)
                    {
                        shortSeq += "A";
                    }
                    else
                    {
                        List<Direction> steps = numpadPaths[(c1, c2)];
                        shortSeq += DirectionsToString(steps) + "A";
                    }
                }
                int robotCount = 2;
                string finalSeq = string.Empty;
                for (int j = 0; j < robotCount; j++)
                {
                    firstSteps = dirpadPaths[('A', shortSeq[0])];
                    finalSeq = string.Empty;
                    finalSeq += DirectionsToString(firstSteps) + "A";
                    for (int k = 0; k < shortSeq.Length - 1; k++)
                    {
                        char k1 = shortSeq[k];
                        char k2 = shortSeq[k + 1];
                        if (k1 == k2)
                        {
                            finalSeq += "A";
                        }
                        else
                        {
                            List<Direction> dirSteps = dirpadPaths[(k1, k2)];
                            finalSeq += DirectionsToString(dirSteps) + "A";
                        }
                    }
                    shortSeq = finalSeq;
                }
                int value = int.Parse(code.Substring(0, code.Length - 1));
                int seqLength = finalSeq.Length;
                Console.WriteLine($"{code}: {finalSeq}");
                Console.WriteLine($"{code}: {seqLength} * {value}");
                //Console.WriteLine($"{code}: {result} = {result} + {seqLength} * {value} = {result} + {seqLength * value} = {result + seqLength * value}");
                result += (value * seqLength);
            }
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

        private static Dictionary<(char, char), List<Direction>> GetPadPaths(string padString)
        {
            string[] rows = padString.Split('\n');
            char[,] pad = new char[rows.Length, rows[0].Length];
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rows[i].Length; j++)
                {
                    pad[i, j] = rows[i][j];
                }
            }
            Dictionary<char, Point> charLocations = new Dictionary<char, Point>();
            for (int i = 0; i < pad.GetLength(0); i++)
            {
                for (int j = 0; j < pad.GetLength(1); j++)
                {
                    charLocations[pad[i, j]] = new Point(i, j);
                }
            }
            HashSet<char> allChars = new HashSet<char>();
            for (int i = 0; i < pad.GetLength(0); i++)
            {
                for (int j = 0; j < pad.GetLength(1); j++)
                {
                    if (pad[i, j] != '#')
                        allChars.Add(pad[i, j]);
                }
            }
            Dictionary<(char, char), List<Direction>> padPaths = new();
            foreach (char c1 in allChars)
            {
                foreach (char c2 in allChars)
                {
                    if (c1 != c2)
                    {
                        Point p1 = charLocations[c1];
                        Point p2 = charLocations[c2];
                        List<Direction> directionalSteps = FindShortestPath(pad, p1, p2);
                        padPaths[(c1, c2)] = directionalSteps;
                    }
                }
            }
            return padPaths;
        }

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

                    int moveCost = 2;
                    if (nextDir == currentState.Direction)
                    {
                        moveCost = 1;
                    }
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

        private static string DirectionsToString(List<Direction> directions)
        {
            return string.Join("", directions.Select(d => d switch
            {
                Direction.Left => "<",
                Direction.Right => ">",
                Direction.Up => "^",
                Direction.Down => "v",
            }));
        }

    }

}
