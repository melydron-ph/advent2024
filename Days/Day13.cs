using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;

namespace advent2024.Days
{
    public static class Day13
    {
        private static readonly string InputFile = @"C:\aoc\2024\day13\test.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day13\output.txt";
        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            int costA = 3;
            int costB = 1;
            List<ClawMachine> clawMachines = GetClawMachinesFromFile(InputFile, costA, costB);

            int result = 0;
            Console.WriteLine($"13*1 -- {result}");

        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int result = 0;
            Console.WriteLine($"13*2 -- {result}");
        }


        public class Move
        {
            public int DeltaX { get; set; }
            public int DeltaY { get; set; }
            public int Cost { get; set; }

            public Move(int deltaX, int deltaY, int cost)
            {
                DeltaX = deltaX;
                DeltaY = deltaY;
                Cost = cost;
            }
        }

        public class ClawMachine
        {
            public Move ButtonA { get; set; }
            public Move ButtonB { get; set; }
            public Point Prize { get; set; }

            public ClawMachine(Move buttonA, Move buttonB, Point prize)
            {
                ButtonA = buttonA;
                ButtonB = buttonB;
                Prize = prize;
            }
        }

        private static List<ClawMachine> GetClawMachinesFromFile(string inputFile, int costA, int costB)
        {
            List<ClawMachine> clawMachines = new List<ClawMachine>();
            string[] lines = File.ReadAllText(InputFile).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Count(); i += 3)
            {
                Match buttonAMatch = Regex.Match(lines[i], @"Button A: X\+(\d+), Y\+(\d+)");
                Move buttonA = new Move(int.Parse(buttonAMatch.Groups[1].Value), int.Parse(buttonAMatch.Groups[2].Value), costA);
                Match buttonBMatch = Regex.Match(lines[i + 1], @"Button B: X\+(\d+), Y\+(\d+)");
                Move buttonB = new Move(int.Parse(buttonBMatch.Groups[1].Value), int.Parse(buttonAMatch.Groups[2].Value), costB);
                Match prizeMatch = Regex.Match(lines[i + 2], @"Prize: X=(\d+), Y=(\d+)");
                Point prize = new Point(int.Parse(prizeMatch.Groups[1].Value), int.Parse(prizeMatch.Groups[2].Value));
                clawMachines.Add(new ClawMachine(buttonA, buttonB, prize));
            }
            return clawMachines;
        }

    }
}
