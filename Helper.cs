using advent2024.Days;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent2024
{
    internal class Helper
    {

        public static int FindWord(string word, string[] lines, int posX, int posY)
        {
            int result = 0;
            int linesLength = lines[0].Length;
            int linesCount = lines.Length;
            int wordLength = word.Length;
            bool okUp = posX > (wordLength - 2) ? true : false;
            bool okDown = posX <= linesCount - wordLength ? true : false;
            bool okLeft = posY > (wordLength - 2) ? true : false;
            bool okRight = posY <= linesLength - wordLength ? true : false;
            if (okUp) // ↑↑↑↑
            {
                bool found = true;
                int x = posX;
                int y = posY;
                for (int i = 1; i < wordLength; i++)
                {
                    char c = lines[--x][y];
                    if (c != word[i])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    //Console.WriteLine($"Found Up");
                    result++;
                }

            }
            if (okDown) // ↓↓↓↓
            {
                bool found = true;
                int x = posX;
                int y = posY;
                for (int i = 1; i < wordLength; i++)
                {
                    char c = lines[++x][y];
                    if (c != word[i])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    //Console.WriteLine($"Found Down");
                    result++;
                }
            }
            if (okLeft) // ←←←←
            {
                bool found = true;
                int x = posX;
                int y = posY;
                for (int i = 1; i < wordLength; i++)
                {
                    char c = lines[x][--y];
                    if (c != word[i])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    //Console.WriteLine($"Found Left");
                    result++;
                }
            }
            if (okRight) // →→→→ 
            {
                bool found = true;
                int x = posX;
                int y = posY;
                for (int i = 1; i < wordLength; i++)
                {
                    char c = lines[x][++y];
                    if (c != word[i])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    //Console.WriteLine($"Found Right");
                    result++;
                }
            }
            if (okUp && okLeft) // ↖↖↖↖
            {
                bool found = true;
                int x = posX;
                int y = posY;
                for (int i = 1; i < wordLength; i++)
                {
                    char c = lines[--x][--y];
                    if (c != word[i])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    //Console.WriteLine($"Found UpLeft");
                    result++;
                }
            }
            if (okUp && okRight) // ↗↗↗↗
            {
                bool found = true;
                int x = posX;
                int y = posY;
                for (int i = 1; i < wordLength; i++)
                {
                    char c = lines[--x][++y];
                    if (c != word[i])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    //Console.WriteLine($"Found UpRight");
                    result++;
                }
            }
            if (okDown && okLeft) // ↙↙↙↙
            {
                bool found = true;
                int x = posX;
                int y = posY;
                for (int i = 1; i < wordLength; i++)
                {
                    char c = lines[++x][--y];
                    if (c != word[i])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    //Console.WriteLine($"Found DownLeft");
                    result++;
                }
            }
            if (okDown && okRight) // ↘↘↘↘
            {
                bool found = true;
                int x = posX;
                int y = posY;
                for (int i = 1; i < wordLength; i++)
                {
                    char c = lines[++x][++y];
                    if (c != word[i])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    //Console.WriteLine($"Found DownRight");
                    result++;
                }
            }
            return result;
        }
        public static int FindXMAS(string[] lines, int posX, int posY)
        {
            int linesLength = lines[0].Length;
            int linesCount = lines.Length;
            bool okUp = posX > 0 ? true : false;
            bool okDown = posX <= linesCount - 2 ? true : false;
            bool okLeft = posY > 0 ? true : false;
            bool okRight = posY <= linesLength - 2 ? true : false;
            bool okDiagonals = okUp && okDown && okLeft && okRight;
            bool firstOk = false;
            bool secondOk = false;
            if (okDiagonals)
            {
                // Check 1st Diagonal
                char a = lines[posX - 1][posY - 1];
                char b = lines[posX + 1][posY + 1];
                if ((a == 'S' && b == 'M') || (a == 'M' && b == 'S'))
                    firstOk = true;

                // Check 2nd Diagonal
                if (firstOk)
                {
                    a = lines[posX - 1][posY + 1];
                    b = lines[posX + 1][posY - 1];
                    if ((a == 'S' && b == 'M') || (a == 'M' && b == 'S'))
                        secondOk = true;
                }
            }
            if (firstOk && secondOk)
            {
                //Console.WriteLine($"Found XMAS centered @ {posX},{posY}");
                return 1;
            }
            return 0;
        }

        private static char[,] _previousMap;
        internal static void PrintMap(char[,] map, bool day15 = false)
        {
            if (day15)
            {
                if (_previousMap == null)
                {
                    _previousMap = new char[map.GetLength(0), map.GetLength(1)];
                    Console.SetCursorPosition(0, 0);
                    Console.CursorVisible = false;
                    for (int i = 0; i < map.GetLength(0); i++)
                    {
                        Console.SetCursorPosition(0, i);
                        for (int j = 0; j < map.GetLength(1); j++)
                        {
                            PrintDay15Char(map[i, j]);
                            _previousMap[i, j] = map[i, j];
                        }
                    }
                    return;
                }
                else
                    for (int i = 0; i < map.GetLength(0); i++)
                    {
                        for (int j = 0; j < map.GetLength(1); j++)
                        {
                            if (map[i, j] != _previousMap[i, j])
                            {
                                Console.SetCursorPosition(j * 2, i); //j*2 for emoji, otherwise switch to j
                                PrintDay15Char(map[i, j]);
                                _previousMap[i, j] = map[i, j];
                            }
                        }
                    }

            }
            else
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        Console.Write(map[i, j]);
                    }
                    Console.WriteLine();
                }
        }

        internal static void PrintMap(int[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j].ToString());
                }
                Console.WriteLine();
            }
        }

        private static void PrintDay15Char(char c)
        {
            switch (c)
            {
                case '#':
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("🧱");
                    break;
                case '.':
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("  ");
                    break;
                case '@':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("🤖");
                    break;
                case 'O':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("📦");
                    break;
                case '[':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("📦");
                    break;
                case ']':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("📦");
                    break;
                default:
                    Console.Write(c);
                    break;
            }
            Console.ResetColor();
        }

        public static char[,] LinesToCharMap(string[] lines)
        {

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

            return map;
        }

        internal static void PrintMap(char[][] map)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    Console.Write(map[i][j]);
                }
                Console.WriteLine();
            }
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public static List<List<Point>> CharMapToAreas(char[,] map)
        {
            List<List<Point>> areas = new List<List<Point>>();
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            bool[,] visited = new bool[rows, cols];
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
            return areas;
        }



        public static void FloodFill(char[,] map, int row, int col, char target, List<Point> region, bool[,] visited)
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


        public class State
        {
            public Point Position { get; }
            public Direction Direction { get; }
            public int Cost { get; }
            public List<Point> Path { get; }

            public State(Point position, Direction direction, int cost, List<Point> path = null)
            {
                Position = position;
                Direction = direction;
                Cost = cost;
                Path = path;
            }

            public override bool Equals(object obj)
            {
                if (obj is State other)
                {
                    return Position.Equals(other.Position) && Direction == other.Direction;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Position, Direction);
            }
        }

        public static int FindShortestPath(char[,] map, Point start, Point end, bool day16 = false)
        {
            PriorityQueue<State, int> queue = new PriorityQueue<State, int>();
            HashSet<(Point, Direction)> visited = new HashSet<(Point, Direction)>();

            State initialState = new State(start, Direction.Right, 0);
            queue.Enqueue(initialState, 0);

            while (queue.Count > 0)
            {
                State currentState = queue.Dequeue();
                (Point, Direction) stateKey = (currentState.Position, currentState.Direction);
                if (visited.Contains(stateKey))
                    continue;
                visited.Add(stateKey);
                if (currentState.Position.Equals(end))
                    return currentState.Cost;
                List<Direction> possibleMoves = GetPossibleMoves(currentState.Direction, day16);
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
                    if (day16) // Day 16
                        if (nextDir != currentState.Direction)
                            moveCost += 1000;
                    int nextCost = currentState.Cost + moveCost;
                    State nextState = new State(nextPos, nextDir, nextCost);
                    if (!visited.Contains((nextPos, nextDir)))
                        queue.Enqueue(nextState, nextState.Cost);
                }
            }
            return -1;
        }

        public static List<Point> FindShortestPath(char[,] map, Point start, Point end)
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
                    State initialState = new State(start, startDir, 0, initialPath);
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
                    return currentState.Path;

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

                    State nextState = new State(nextPos, nextDir, nextCost, nextPath);

                    if (!visited.Contains((nextPos, nextDir)))
                        queue.Enqueue(nextState, nextState.Cost);
                }
            }

            return new List<Point>();
        }

        private static List<Direction> GetPossibleMoves(Direction currentDir, bool day16 = false)
        {
            List<Direction> moves = new List<Direction>();
            switch (currentDir)
            {
                case Direction.Left or Direction.Right:
                    moves.Add(Direction.Up);
                    moves.Add(Direction.Down);
                    break;
                case Direction.Up or Direction.Down:
                    moves.Add(Direction.Left);
                    moves.Add(Direction.Right);
                    break;
            }
            moves.Add(currentDir);
            return moves;
        }

        public static List<List<Point>> FindAllShortestPaths(char[,] map, Point start, Point end, bool day16 = false)
        {
            PriorityQueue<State, int> queue = new PriorityQueue<State, int>();
            Dictionary<(Point, Direction), int> visited = new Dictionary<(Point, Direction), int>();
            List<List<Point>> shortestPaths = new List<List<Point>>();
            int minCostFound = int.MaxValue;
            List<Point> initialPath = new List<Point>() { start };
            State initialState = new State(start, Direction.Right, 0, initialPath);
            queue.Enqueue(initialState, 0);
            while (queue.Count > 0)
            {
                State currentState = queue.Dequeue();
                (Point, Direction) stateKey = (currentState.Position, currentState.Direction);
                if (visited.TryGetValue(stateKey, out int prevCost) && prevCost < currentState.Cost)
                    continue;
                visited[stateKey] = currentState.Cost;
                if (currentState.Position.Equals(end))
                {
                    if (currentState.Cost < minCostFound)
                    {
                        shortestPaths.Clear();
                        shortestPaths.Add(new List<Point>(currentState.Path));
                        minCostFound = currentState.Cost;
                    }
                    else if (currentState.Cost == minCostFound)
                        shortestPaths.Add(new List<Point>(currentState.Path));
                    continue;
                }
                if (currentState.Cost >= minCostFound)
                    continue;
                List<Direction> possibleMoves = GetPossibleMoves(currentState.Direction, day16);
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
                    if (day16)
                        if (nextDir != currentState.Direction)
                            moveCost += 1000;
                    int nextCost = currentState.Cost + moveCost;
                    List<Point> nextPath = new List<Point>(currentState.Path) { nextPos };
                    State nextState = new State(nextPos, nextDir, nextCost, nextPath);
                    (Point, Direction) nextStateKey = (nextPos, nextDir);
                    if (!visited.TryGetValue(nextStateKey, out prevCost) || nextCost <= prevCost)
                        queue.Enqueue(nextState, nextState.Cost);
                }
            }

            return shortestPaths;
        }

        public static Point GetNextPoint(Direction d)
        {
            Point nextP = new Point(0, 0);
            switch (d)
            {
                case Direction.Up:
                    nextP.X = -1;
                    nextP.Y = 0;
                    break;
                case Direction.Down:
                    nextP.X = 1;
                    nextP.Y = 0;
                    break;
                case Direction.Left:
                    nextP.X = 0;
                    nextP.Y = -1;
                    break;
                case Direction.Right:
                    nextP.X = 0;
                    nextP.Y = 1;
                    break;
            }
            return nextP;
        }

        private static bool IsValidMove(char[,] map, Point pos)
        {
            int height = map.GetLength(0);
            int width = map.GetLength(1);

            return pos.X >= 0 && pos.X < height &&
                   pos.Y >= 0 && pos.Y < width &&
                   map[pos.X, pos.Y] != '#';
        }
    }
}
