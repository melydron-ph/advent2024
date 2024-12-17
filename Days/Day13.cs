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
        private static readonly string InputFile = @"C:\aoc\2024\day13\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day13\output.txt";
        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            List<ClawMachine> clawMachines = GetClawMachinesFromFile(InputFile);
            long result = 0;

            foreach (ClawMachine cm in clawMachines) {
                var s = Solve(cm.ButtonA, cm.ButtonB, cm.Prize);
                if (s.HasValue) {
                    result += s.Value.buttonACount * 3 + s.Value.buttonBCount;
                }
            }
            Console.WriteLine($"13*1 -- {result}");

        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);
            List<ClawMachine> clawMachines = GetClawMachinesFromFile(InputFile);
            long result = 0;

            foreach (ClawMachine cm in clawMachines)
            {
                var s = Solve(cm.ButtonA, cm.ButtonB, cm.Prize, true);
                if (s.HasValue)
                {
                    result += s.Value.buttonACount * 3 + s.Value.buttonBCount;
                }
            }
            Console.WriteLine($"13*2 -- {result}");
        }


        public class Move
        {
            public int DeltaX { get; set; }
            public int DeltaY { get; set; }

            public Move(int deltaX, int deltaY)
            {
                DeltaX = deltaX;
                DeltaY = deltaY;
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

        private static List<ClawMachine> GetClawMachinesFromFile(string inputFile)
        {
            List<ClawMachine> clawMachines = new List<ClawMachine>();
            string[] lines = File.ReadAllText(InputFile).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Count(); i += 3)
            {
                Match buttonAMatch = Regex.Match(lines[i], @"Button A: X\+(\d+), Y\+(\d+)");
                Move buttonA = new Move(int.Parse(buttonAMatch.Groups[1].Value), int.Parse(buttonAMatch.Groups[2].Value));
                Match buttonBMatch = Regex.Match(lines[i + 1], @"Button B: X\+(\d+), Y\+(\d+)");
                Move buttonB = new Move(int.Parse(buttonBMatch.Groups[1].Value), int.Parse(buttonBMatch.Groups[2].Value));
                Match prizeMatch = Regex.Match(lines[i + 2], @"Prize: X=(\d+), Y=(\d+)");
                Point prize = new Point(int.Parse(prizeMatch.Groups[1].Value), int.Parse(prizeMatch.Groups[2].Value));
                clawMachines.Add(new ClawMachine(buttonA, buttonB, prize));
            }
            return clawMachines;
        }


        private static (long buttonACount, long buttonBCount)? Solve(Move buttonA, Move buttonB, Point prize, bool part2 = false)
        {
            long determinant = buttonA.DeltaX * buttonB.DeltaY - buttonA.DeltaY * buttonB.DeltaX;
            if (determinant == 0)
                return null;

            long incr = !part2 ? 0 : 10000000000000;
            long buttonANumerator = (prize.X + incr) * buttonB.DeltaY - (prize.Y + incr) * buttonB.DeltaX;
            long buttonBNumerator = (prize.Y + incr) * buttonA.DeltaX - (prize.X + incr) * buttonA.DeltaY;

            if (buttonANumerator % determinant != 0 || buttonBNumerator % determinant != 0)
                return null;

            long pressesA = buttonANumerator / determinant;
            long pressesB = buttonBNumerator / determinant;

            return (pressesA < 0 || pressesB < 0) ? null : (pressesA, pressesB);
        }

    }
}
