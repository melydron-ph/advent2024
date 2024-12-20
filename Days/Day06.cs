using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using static advent2024.Helper;

namespace advent2024.Days
{
    public static class Day06
    {
        private static readonly string InputFile = @"C:\aoc\2024\day6\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day6\output.txt";
        private static List<((int, int), Direction)> BlocksVisited;

        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int mapX = lines[0].Length;
            int mapY = lines.Count();
            char[,] map = new char[mapX, mapY];
            int startX = 0;
            int startY = 0;
            Direction d = new Direction();
            List<(int, int)> visitedPos = new List<(int, int)>();
            for (int i = 0; i < mapY; i++)
            {
                string line = lines[i];
                for (int j = 0; j < mapX; j++)
                {
                    map[i, j] = line[j];
                    if (map[i, j] == '^' || map[i, j] == '>' || map[i, j] == 'v' || map[i, j] == '<')
                    {
                        startX = i;
                        startY = j;
                        switch (map[i, j])
                        {
                            case '^':
                                d = Direction.Up;
                                break;
                            case '>':
                                d = Direction.Right;
                                break;
                            case 'v':
                                d = Direction.Down;
                                break;
                            case '<':
                                d = Direction.Left;
                                break;
                        }
                        map[i, j] = '.';
                    }
                }
            }
            visitedPos.Add((startX, startY));
            MoveUntilExit(startX, startY, map, ref d, ref visitedPos);
            int result = visitedPos.Count();
            Console.WriteLine($"06*1 -- {result}");
        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int mapX = lines[0].Length;
            int mapY = lines.Count();
            char[,] map = new char[mapX, mapY];
            int startX = 0;
            int startY = 0;
            Direction d = new Direction();
            List<(int, int)> visitedPos = new List<(int, int)>();
            for (int i = 0; i < mapY; i++)
            {
                string line = lines[i];
                for (int j = 0; j < mapX; j++)
                {
                    map[i, j] = line[j];
                    if (map[i, j] == '^' || map[i, j] == '>' || map[i, j] == 'v' || map[i, j] == '<')
                    {
                        startX = i;
                        startY = j;
                        switch (map[i, j])
                        {
                            case '^':
                                d = Direction.Up;
                                break;
                            case '>':
                                d = Direction.Right;
                                break;
                            case 'v':
                                d = Direction.Down;
                                break;
                            case '<':
                                d = Direction.Left;
                                break;
                        }
                        map[i, j] = '.';
                    }
                }
            }
            visitedPos.Add((startX, startY));

            BlocksVisited = new List<((int, int), Direction)>();
            int result = 0;
            for (int i = 0;i < mapX; i++)
            {
                Console.WriteLine($"{i} / {mapX}");

                for (int j = 0;j < mapY; j++)
                {
                    int originX = startX;
                    int originY = startY;
                    Direction originD = d;
                    if ( (i,j) != (originX, originY) )
                    {
                        if (map[i, j] == '.'){
                            map[i, j] = '#';
                            result += MoveUntilExit(originX, originY, map, ref d, ref visitedPos, true);
                            BlocksVisited.Clear();
                            visitedPos.Clear();
                            visitedPos.Add((startX, startY));
                            d = originD;
                            map[i, j] = '.';
                        }
                    }
                }
            }

            Console.WriteLine($"06*2 -- {result}");
        }


        internal static int MoveUntilExit(int startX, int startY, char[,] map, ref Direction d, ref List<(int, int)> visitedPos, bool loopSearch = false)
        {
            Direction prevD = d;
            int move = MoveLine(ref startX, ref startY, map, ref d, ref visitedPos, loopSearch);
            while (move != -1)
            {
                //Console.WriteLine($"{prevD,-4:G}\t({startX,3},{startY,3}) :{visitedPos.Count}");
                prevD = d;
                move = MoveLine(ref startX, ref startY, map, ref d, ref visitedPos, loopSearch);
                if (move == 1)
                    break;
            }
            return move > 0 ? move : 0;
        }

        internal static int MoveLine(ref int startX, ref int startY, char[,] map, ref Direction d, ref List<(int, int)> visitedPos, bool loopSearch = false)
        {
            int mapX = map.GetLength(0);
            int mapY = map.GetLength(1);
            char nextStep = '0';
            switch (d)
            {
                case Direction.Up:
                    if (startX > 0)
                    {
                        startX = startX - 1;
                        nextStep = map[startX, startY];
                        while (nextStep == '.')
                        {
                            if (!visitedPos.Contains((startX, startY)))
                                visitedPos.Add((startX, startY));
                            startX = startX - 1;
                            if (startX == -1)
                                return -1;
                            nextStep = map[startX, startY];
                        }
                        startX++;
                        if (loopSearch)
                        {
                            if (BlocksVisited.Contains(((startX, startY), d)))
                            {
                                return 1;
                            }
                            BlocksVisited.Add(((startX, startY), d));
                        }
                        break;
                    }
                    else return -1;
                case Direction.Down:
                    if (startX < mapX - 1)
                    {
                        startX = startX + 1;
                        nextStep = map[startX, startY];
                        while (nextStep == '.')
                        {
                            if (!visitedPos.Contains((startX, startY)))
                                visitedPos.Add((startX, startY));
                            startX = startX + 1;
                            if (startX == mapX)
                                return -1;
                            nextStep = map[startX, startY];
                        }

                        startX--;
                        if (loopSearch)
                        {
                            if (BlocksVisited.Contains(((startX, startY), d)))
                            {
                                return 1;
                            }
                            BlocksVisited.Add(((startX, startY), d));
                        }
                        break;
                    }
                    else return -1;
                case Direction.Right:
                    if (startY < mapY - 1)
                    {
                        startY = startY + 1;
                        nextStep = map[startX, startY];
                        while (nextStep == '.')
                        {
                            if (!visitedPos.Contains((startX, startY)))
                                visitedPos.Add((startX, startY));
                            startY = startY + 1;
                            if (startY == mapY)
                                return -1;
                            nextStep = map[startX, startY];

                        }
                        startY--;
                        if (loopSearch)
                        {
                            if (BlocksVisited.Contains(((startX, startY), d)))
                            {
                                return 1;
                            }
                            BlocksVisited.Add(((startX, startY), d));
                        }
                        break;
                    }
                    else return -1;
                case Direction.Left:
                    if (startY > 0)
                    {
                        startY = startY - 1;
                        nextStep = map[startX, startY];
                        while (nextStep == '.')
                        {
                            if (!visitedPos.Contains((startX, startY)))
                                visitedPos.Add((startX, startY));
                            startY = startY - 1;
                            if (startY == -1)
                                return -1;
                            nextStep = map[startX, startY];
                        }
                        startY++;
                        if (loopSearch)
                        {
                            if (BlocksVisited.Contains(((startX, startY), d)))
                            {
                                return 1;
                            }
                            BlocksVisited.Add(((startX, startY), d));
                        }
                        break;
                    }
                    else return -1;
            }
            switch (d)
            {
                case Direction.Left:
                    d = Direction.Up;
                    break;
                case Direction.Up:
                    d = Direction.Right;
                    break;
                case Direction.Right:
                    d = Direction.Down;
                    break;
                case Direction.Down:
                    d = Direction.Left;
                    break;
            }
            return 0;
        }
    }
}
