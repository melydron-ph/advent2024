using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;
using System.ComponentModel.Design;

namespace advent2024.Days
{
    public static class Day15
    {
        private static readonly string InputFile = @"C:\aoc\2024\day15\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day15\output.txt";

        private static int _tickRate = 50;
        public static void SolvePart1()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            File.WriteAllText(OutputFile, string.Empty);
            string file = File.ReadAllText(InputFile);
            string[] fileBlocks = file.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] mapLines = fileBlocks[0].Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int mapX = mapLines[0].Length;
            int mapY = mapLines.Count();
            char[,] map = new char[mapY, mapX];
            int startX = 0;
            int startY = 0;
            for (int i = 0; i < mapY; i++)
            {
                string line = mapLines[i];
                for (int j = 0; j < mapX; j++)
                {
                    map[i, j] = line[j];
                    if (map[i, j] == '@')
                    {
                        startX = i;
                        startY = j;
                    }
                }
            }
            PrintMap(map, true);
            Console.SetCursorPosition(0, map.GetLength(0) + 1);
            Console.WriteLine("Press any key to start...");
            Console.ReadKey(true);
            Console.SetCursorPosition(0, map.GetLength(0) + 1);
            Console.WriteLine($"Tick Rate: {_tickRate.ToString().PadRight(3)}ms[+/- 10]");
            string[] moveLines = fileBlocks[1].Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            List<Direction> directions = new List<Direction>();
            for (int i = 0; i < moveLines.Length; i++)
            {
                for (int j = 0; j < moveLines[i].Length; j++)
                {
                    char c = moveLines[i][j];
                    switch (c)
                    {
                        case '^':
                            directions.Add(Direction.Up);
                            break;
                        case '>':
                            directions.Add(Direction.Right);
                            break;
                        case 'v':
                            directions.Add(Direction.Down);
                            break;
                        case '<':
                            directions.Add(Direction.Left);
                            break;
                    }
                }
            }
            foreach (Direction direction in directions)
            {
                while (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Add:
                            _tickRate = Math.Min(_tickRate + 10, 100);
                            break;
                        case ConsoleKey.Subtract:
                            _tickRate = Math.Max(_tickRate - 10, 0);
                            break;
                    }
                    Console.SetCursorPosition(0, map.GetLength(0) + 1);
                    Console.WriteLine($"Tick Rate: {_tickRate.ToString().PadRight(3)}ms[+/- 10]");
                }
                Thread.Sleep(_tickRate);
                TryRobotMove(direction, ref startX, ref startY, map);
                //Console.Clear();
                PrintMap(map, true);
            }
            Console.SetCursorPosition(0, map.GetLength(0) + 3);
            int result = GetMapValue(map);
            Console.SetCursorPosition(0, map.GetLength(0) + 5);
            Console.WriteLine($"15*1 -- {result}");

        }
        public static void SolvePart2()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool showStuff = true;
            File.WriteAllText(OutputFile, string.Empty);
            string file = File.ReadAllText(InputFile);
            file = file.Replace("#", "##");
            file = file.Replace(".", "..");
            file = file.Replace("O", "[]");
            file = file.Replace("@", "@.");
            string[] fileBlocks = file.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] mapLines = fileBlocks[0].Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int mapX = mapLines[0].Length;
            int mapY = mapLines.Count();
            char[,] map = new char[mapY, mapX];
            int startX = 0;
            int startY = 0;
            for (int i = 0; i < mapY; i++)
            {
                string line = mapLines[i];
                for (int j = 0; j < mapX; j++)
                {
                    map[i, j] = line[j];
                    if (map[i, j] == '@')
                    {
                        startX = i;
                        startY = j;
                    }
                }
            }
            if (showStuff)
            {
                PrintMap(map, true);
                Console.SetCursorPosition(0, mapY + 1);
                Console.WriteLine("Press any key to start...");
                Console.ReadKey(true);
                Console.SetCursorPosition(0, map.GetLength(0) + 1);
                Console.WriteLine($"Tick Rate: {_tickRate.ToString().PadRight(3)}ms[+/- 10]");
            }
            string[] moveLines = fileBlocks[1].Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            List<Direction> directions = new List<Direction>();
            for (int i = 0; i < moveLines.Length; i++)
            {
                for (int j = 0; j < moveLines[i].Length; j++)
                {
                    char c = moveLines[i][j];
                    switch (c)
                    {
                        case '^':
                            directions.Add(Direction.Up);
                            break;
                        case '>':
                            directions.Add(Direction.Right);
                            break;
                        case 'v':
                            directions.Add(Direction.Down);
                            break;
                        case '<':
                            directions.Add(Direction.Left);
                            break;
                    }
                }
            }
            foreach (Direction direction in directions)
            {
                while (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Add:
                            _tickRate = Math.Min(_tickRate + 10, 100);
                            break;
                        case ConsoleKey.Subtract:
                            _tickRate = Math.Max(_tickRate - 10, 0);
                            break;
                    }
                    if (showStuff)
                    {
                        Console.SetCursorPosition(0, map.GetLength(0) + 1);
                        Console.WriteLine($"Tick Rate: {_tickRate.ToString().PadRight(3)}ms[+/- 10]");
                    }
                }
                if (showStuff)
                    Thread.Sleep(_tickRate);
                TryRobotMove2(direction, ref startX, ref startY, ref map);
                if (showStuff)
                {
                    PrintMap(map, true);
                }
            }
            if (showStuff)
                Console.SetCursorPosition(0, map.GetLength(0) + 3);
            int result = GetMapValue(map);
            if (showStuff)
                Console.SetCursorPosition(0, map.GetLength(0) + 5);
            Console.WriteLine($"15*2 -- {result}");
        }

        public static void Day15Play()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string file = File.ReadAllText(InputFile);
            string[] fileBlocks = file.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] mapLines = fileBlocks[0].Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int mapX = mapLines[0].Length;
            int mapY = mapLines.Count();
            char[,] map = new char[mapX, mapY];
            int startX = 0;
            int startY = 0;
            for (int i = 0; i < mapY; i++)
            {
                string line = mapLines[i];
                for (int j = 0; j < mapX; j++)
                {
                    map[i, j] = line[j];
                    if (map[i, j] == '@')
                    {
                        startX = i;
                        startY = j;
                    }
                }
            }
            PrintMap(map, false);

            bool continuePlaying = true;
            while (continuePlaying)
            {
                var key = Console.ReadKey(true);
                Direction? direction = null;

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        direction = Direction.Up;
                        break;
                    case ConsoleKey.RightArrow:
                        direction = Direction.Right;
                        break;
                    case ConsoleKey.DownArrow:
                        direction = Direction.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        direction = Direction.Left;
                        break;
                    case ConsoleKey.Escape:
                        continuePlaying = false;
                        break;
                }

                if (direction.HasValue)
                {
                    Console.WriteLine($"{direction.ToString().PadRight(5)}");
                    //Thread.Sleep(500);
                    TryRobotMove(direction.Value, ref startX, ref startY, map);
                    PrintMap(map, false);
                }
            }

            int result = GetMapValue(map);
            Console.WriteLine($"15*1 -- {result}");

        }

        public static void Day15Play2()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            File.WriteAllText(OutputFile, string.Empty);
            string file = File.ReadAllText(InputFile);
            file = file.Replace("#", "##");
            file = file.Replace(".", "..");
            file = file.Replace("O", "[]");
            file = file.Replace("@", "@.");
            string[] fileBlocks = file.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] mapLines = fileBlocks[0].Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int mapX = mapLines[0].Length;
            int mapY = mapLines.Count();
            char[,] map = new char[mapY, mapX];
            int startX = 0;
            int startY = 0;
            for (int i = 0; i < mapY; i++)
            {
                string line = mapLines[i];
                for (int j = 0; j < mapX; j++)
                {
                    map[i, j] = line[j];
                    if (map[i, j] == '@')
                    {
                        startX = i;
                        startY = j;
                    }
                }
            }
            PrintMap(map, true);

            bool continuePlaying = true;
            while (continuePlaying)
            {
                var key = Console.ReadKey(true);
                Direction? direction = null;

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        direction = Direction.Up;
                        break;
                    case ConsoleKey.RightArrow:
                        direction = Direction.Right;
                        break;
                    case ConsoleKey.DownArrow:
                        direction = Direction.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        direction = Direction.Left;
                        break;
                    case ConsoleKey.Escape:
                        continuePlaying = false;
                        break;
                }

                if (direction.HasValue)
                {
                    //Console.WriteLine($"{direction.ToString().PadRight(5)}");
                    //Thread.Sleep(500);
                    TryRobotMove2(direction.Value, ref startX, ref startY, ref map);
                    PrintMap(map, true);
                }
            }

            int result = GetMapValue(map);
            Console.WriteLine($"15*1 -- {result}");

        }

        internal static void TryRobotMove(Direction d, ref int startX, ref int startY, char[,] map)
        {
            char nextMove = '.';
            int nextX = startX;
            int nextY = startY;
            switch (d)
            {
                case Direction.Up:
                    nextX = startX - 1;
                    break;
                case Direction.Right:
                    nextY = startY + 1;
                    break;
                case Direction.Down:
                    nextX = startX + 1;
                    break;
                case Direction.Left:
                    nextY = startY - 1;
                    break;
            }
            nextMove = map[nextX, nextY];
            switch (nextMove)
            {
                case '.':
                    map[startX, startY] = '.';
                    map[nextX, nextY] = '@';
                    startX = nextX;
                    startY = nextY;
                    return;
                case '#':
                    return;
                case 'O':
                    if (TryBoxMove(d, nextX, nextY, map))
                    {
                        map[startX, startY] = '.';
                        map[nextX, nextY] = '@';
                        startX = nextX;
                        startY = nextY;
                    }
                    return;
            }
        }

        internal static bool TryBoxMove(Direction d, int x, int y, char[,] map)
        {

            char nextPos = '.';
            int nextX = x;
            int nextY = y;
            switch (d)
            {
                case Direction.Up:
                    nextX = x - 1;
                    break;
                case Direction.Right:
                    nextY = y + 1;
                    break;
                case Direction.Down:
                    nextX = x + 1;
                    break;
                case Direction.Left:
                    nextY = y - 1;
                    break;
            }
            nextPos = map[nextX, nextY];
            switch (nextPos)
            {
                case '.':
                    map[x, y] = '.';
                    map[nextX, nextY] = 'O';
                    return true;
                case '#':
                    return false;
                case 'O':
                    if (TryBoxMove(d, nextX, nextY, map))
                    {
                        map[x, y] = '.';
                        map[nextX, nextY] = 'O';
                        return true;
                    }
                    else return false;
            }
            return false;
        }

        internal static void TryRobotMove2(Direction d, ref int startX, ref int startY, ref char[,] map)
        {
            char nextMove = '.';
            char[,] tempMap = (char[,])map.Clone();
            int nextX = startX;
            int nextY = startY;
            switch (d)
            {
                case Direction.Up:
                    nextX = startX - 1;
                    break;
                case Direction.Right:
                    nextY = startY + 1;
                    break;
                case Direction.Down:
                    nextX = startX + 1;
                    break;
                case Direction.Left:
                    nextY = startY - 1;
                    break;
            }
            nextMove = map[nextX, nextY];
            switch (nextMove)
            {
                case '.':
                    map[startX, startY] = '.';
                    map[nextX, nextY] = '@';
                    startX = nextX;
                    startY = nextY;
                    return;
                case '#':
                    return;
                case '[':
                    if (TryBoxMove2(d, nextX, nextY, map, nextMove))
                    {
                        map[startX, startY] = '.';
                        map[nextX, nextY] = '@';
                        startX = nextX;
                        startY = nextY;
                    }
                    else
                    {
                        map = (char[,])tempMap.Clone();
                    }
                    return;
                case ']':
                    if (TryBoxMove2(d, nextX, nextY, map, nextMove))
                    {
                        map[startX, startY] = '.';
                        map[nextX, nextY] = '@';
                        startX = nextX;
                        startY = nextY;
                    }
                    else
                    {
                        map = (char[,])tempMap.Clone();
                    }
                    return;
            }
        }

        internal static bool TryBoxMove2(Direction d, int x, int y, char[,] map, char blockSide)
        {

            char nextPos = '.';
            int nextX = x;
            int nextY = y;
            switch (d)
            {
                case Direction.Up:
                    nextX = x - 1;
                    break;
                case Direction.Right:
                    nextY = y + 2;
                    break;
                case Direction.Down:
                    nextX = x + 1;
                    break;
                case Direction.Left:
                    nextY = y - 2;
                    break;
            }
            if (nextX < 0 || nextX > map.GetLength(0) - 1 || nextY < 0 || nextY > map.GetLength(1) - 1)
            {
                return false;
            }
            nextPos = map[nextX, nextY];
            switch (nextPos)
            {
                case '.':
                    switch (d)
                    {
                        case Direction.Up or Direction.Down:
                            if (blockSide == ']')
                            {
                                if (map[nextX, nextY - 1] == '.')
                                {
                                    map[x, y] = '.';
                                    map[x, y - 1] = '.';
                                    map[nextX, nextY] = ']';
                                    map[nextX, nextY - 1] = '[';
                                    return true;
                                }
                                else if (map[nextX, nextY - 1] == '#')
                                    return false;
                                else
                                {
                                    char newBlockSide = map[nextX, nextY - 1];
                                    if (TryBoxMove2(d, nextX, nextY - 1, map, newBlockSide))
                                    {
                                        map[x, y] = '.';
                                        map[x, y - 1] = '.';
                                        map[nextX, nextY] = ']';
                                        map[nextX, nextY - 1] = '[';
                                        return true;
                                    }
                                    else return false;
                                }
                            }
                            else if (blockSide == '[')
                            {
                                if (map[nextX, nextY + 1] == '.')
                                {
                                    map[x, y] = '.';
                                    map[x, y + 1] = '.';
                                    map[nextX, nextY] = '[';
                                    map[nextX, nextY + 1] = ']';
                                    return true;
                                }
                                else if (map[nextX, nextY + 1] == '#')
                                    return false;
                                else
                                {
                                    char newBlockSide = map[nextX, nextY + 1];
                                    if (TryBoxMove2(d, nextX, nextY + 1, map, newBlockSide))
                                    {
                                        map[x, y] = '.';
                                        map[x, y + 1] = '.';
                                        map[nextX, nextY] = '[';
                                        map[nextX, nextY + 1] = ']';
                                        return true;
                                    }
                                    else return false;
                                }
                            }
                            break;
                        case Direction.Right:
                            map[x, y] = '.';
                            map[nextX, nextY] = ']';
                            map[nextX, nextY - 1] = '[';
                            return true;
                        case Direction.Left:
                            map[x, y] = '.';
                            map[nextX, nextY] = '[';
                            map[nextX, nextY + 1] = ']';
                            return true;
                    }
                    break;
                case '#':
                    return false;
                case '[' or ']':
                    if (TryBoxMove2(d, nextX, nextY, map, nextPos))
                    {
                        switch (d)
                        {

                            case Direction.Up or Direction.Down:
                                if (blockSide == ']')
                                {
                                    if (map[nextX, nextY - 1] == '.')
                                    {
                                        map[x, y] = '.';
                                        map[x, y - 1] = '.';
                                        map[nextX, nextY] = ']';
                                        map[nextX, nextY - 1] = '[';
                                        return true;
                                    }
                                    else if (map[nextX, nextY - 1] == '#')
                                        return false;
                                    else
                                    {
                                        char newBlockSide = map[nextX, nextY - 1];
                                        if (TryBoxMove2(d, nextX, nextY - 1, map, newBlockSide))
                                        {
                                            map[x, y] = '.';
                                            map[x, y - 1] = '.';
                                            map[nextX, nextY] = ']';
                                            map[nextX, nextY - 1] = '[';
                                            return true;
                                        }
                                        else return false;
                                    }
                                }
                                else if (blockSide == '[')
                                {
                                    if (map[nextX, nextY + 1] == '.')
                                    {
                                        map[x, y] = '.';
                                        map[x, y + 1] = '.';
                                        map[nextX, nextY] = '[';
                                        map[nextX, nextY + 1] = ']';
                                        return true;
                                    }
                                    else if (map[nextX, nextY + 1] == '#')
                                        return false;
                                    else
                                    {
                                        char newBlockSide = map[nextX, nextY + 1];
                                        if (TryBoxMove2(d, nextX, nextY + 1, map, newBlockSide))
                                        {
                                            map[x, y] = '.';
                                            map[x, y + 1] = '.';
                                            map[nextX, nextY] = '[';
                                            map[nextX, nextY + 1] = ']';
                                            return true;
                                        }
                                        else return false;
                                    }
                                }
                                break;
                            case Direction.Right:
                                map[x, y] = '.';
                                map[nextX, nextY] = ']';
                                map[nextX, nextY - 1] = '[';
                                return true;
                            case Direction.Left:
                                map[x, y] = '.';
                                map[nextX, nextY] = '[';
                                map[nextX, nextY + 1] = ']';
                                return true;
                        }
                        return true;
                    }
                    else return false;
            }
            return false;
        }

        internal static int GetMapValue(char[,] map)
        {
            int result = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 'O' || map[i, j] == '[')
                        result += 100 * i + j;
                }
            }
            return result;
        }
    }
}
