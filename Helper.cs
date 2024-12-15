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
                                Console.SetCursorPosition(j, i);
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
                    Console.Write('■');
                    break;
                case '.':
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(' ');
                    break;
                case '@':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write('*');
                    break;
                case 'O':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write('■');
                    break;
                case '[':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write('<');
                    break;
                case ']':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write('>');
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
            Right,
            Invalid
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
    }
}
