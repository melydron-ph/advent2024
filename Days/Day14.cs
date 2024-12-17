using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;
using static advent2024.Days.Day13;

namespace advent2024.Days
{
    public static class Day14
    {
        private static readonly string InputFile = @"C:\aoc\2024\day14\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day14\output.txt";
        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            List<Robot> robots = GetRobotsFromFile(InputFile);

            int mapRows = 103;
            int mapCols = 101;
            if (InputFile.Contains("test.txt"))
            {
                mapRows = 7;
                mapCols = 11;
            }
            int seconds = 100;

            for (int i = 0; i < seconds; i++)
            {
                foreach (Robot robot in robots)
                {
                    robot.Move(mapRows, mapCols);
                }
            }
            List<int> robotsInQuadrants = RobotsInQuadrants(robots, mapRows, mapCols);
            int result = robotsInQuadrants[1] * robotsInQuadrants[2] * robotsInQuadrants[3] * robotsInQuadrants[4];
            Console.WriteLine($"14*1 -- {result}");

        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int result = 0;
            Console.WriteLine($"14*2 -- {result}");
        }

        private static List<Robot> GetRobotsFromFile(string inputFile)
        {
            List<Robot> robots = new List<Robot>();
            string[] lines = File.ReadAllText(InputFile).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                var nums = Regex.Matches(line, @"-?\d+").Select(m => int.Parse(m.Value)).ToList();
                Point p = new Point(nums[0], nums[1]);
                int deltaX = nums[2];
                int deltaY = nums[3];
                robots.Add(new Robot(p, deltaX, deltaY));
            }
            return robots;
        }

        public class Robot
        {
            public Point Position { get; set; }
            public int DeltaX { get; set; }
            public int DeltaY { get; set; }

            public Robot(Point position, int deltaX, int deltaY)
            {
                Position = position;
                DeltaX = deltaX;
                DeltaY = deltaY;
            }

            public void Move(int mapRows, int mapCols)
            {
                int newX = Position.X + DeltaX;
                int newY = Position.Y + DeltaY;

                if (newX >= mapCols)
                    newX = newX % mapCols;
                else if (newX < 0)
                    newX = ((newX % mapCols) + mapCols) % mapCols;

                if (newY >= mapRows)
                    newY = newY % mapRows;
                else if (newY < 0)
                    newY = ((newY % mapRows) + mapRows) % mapRows;

                Position = new Point(newX, newY);
            }
        }


        public static List<int> RobotsInQuadrants(List<Robot> robots, int mapRows, int mapCols)
        {
            List<int> robotsinQuadrants = new List<int>() { 0, 0, 0, 0, 0 };

            int midCol = mapCols / 2;
            int midRow = mapRows / 2;

            foreach (Robot robot in robots)
            {
                if (mapCols % 2 != 0 && robot.Position.X == midCol || mapRows % 2 != 0 && robot.Position.Y == midRow)
                {
                    robotsinQuadrants[0]++; // middle row or col
                    continue;
                }
                if (robot.Position.X < midCol)
                {
                    if (robot.Position.Y < midRow)
                        robotsinQuadrants[1]++; // top left
                    else
                        robotsinQuadrants[3]++; // bot left

                }
                else
                {
                    if (robot.Position.Y < midRow)
                        robotsinQuadrants[2]++; // top right

                    else
                        robotsinQuadrants[4]++; // bot right

                }

            }
            return robotsinQuadrants;
        }

        public static void PrintQuadrantGrid(List<Robot> robots, int mapRows, int mapCols)
        {
            int midRow = mapRows / 2;
            int midCol = mapCols / 2;

            var robotCounts = new Dictionary<Point, int>();
            foreach (var robot in robots)
            {
                var position = robot.Position;
                if (!robotCounts.ContainsKey(position))
                    robotCounts[position] = 0;
                robotCounts[position]++;
            }

            Console.WriteLine($"Grid {mapRows}x{mapCols} with quadrants:");
            for (int y = 0; y < mapRows; y++)
            {
                for (int x = 0; x < mapCols; x++)
                {
                    var position = new Point(x, y);
                    int robotCount = robotCounts.ContainsKey(position) ? robotCounts[position] : 0;

                    if ((mapCols % 2 != 0 && x == midCol) ||
                        (mapRows % 2 != 0 && y == midRow))
                    {
                        Console.Write(robotCount > 0 ? robotCount.ToString() : "+");
                    }
                    else
                    {
                        Console.Write(robotCount > 0 ? robotCount.ToString() : ".");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
