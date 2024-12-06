using System;
using System.Collections.Generic;
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

        internal static void PrintMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
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

    }
}
