using System.Runtime.ExceptionServices;
using static advent2024.Helper;

namespace advent2024.Days
{
    public static class Day2
    {
        private static readonly string InputFile = @"C:\aoc\2024\day2\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day2\output.txt";
        public enum SequenceMode
        {
            Increase,
            Decrease,
            None
        }

        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);

            var lines = File.ReadAllLines(InputFile);
            int result = 0;

            foreach (var line in lines)
            {
                int[] report = Array.ConvertAll(line.Split(' ').ToArray(), int.Parse);
                if (IsSafeReport(report))
                {
                    result++;
                }

            }

            Console.WriteLine($"2*1 -- {result}");
        }

        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);

            var lines = File.ReadAllLines(InputFile);
            int result = 0;

            foreach (var line in lines)
            {
                int[] report = Array.ConvertAll(line.Split(' ').ToArray(), int.Parse);
                if (IsSafeReport(report, true))
                {
                    result++;
                }
            }
            Console.WriteLine($"2*2 -- {result}");
        }

        private static bool IsSafeReport(int[] report, bool dampener = false)
        {
            if (report.Length < 2) return false;
            if (report.Length == 2) return IsValidDifference(report[0], report[1]);
            if (report.Length > 2)
            {
                SequenceMode currentMode = report[0] > report[1] ? SequenceMode.Decrease : SequenceMode.Increase;
                if (!IsValidDifference(report[0], report[1]))
                {
                    return (dampener && SafeWithDampener(report));
                }

                bool safeReport = true;
                for (int j = 1; j < report.Length - 1; j++)
                {
                    if (!IsValidDifference(report[j], report[j + 1]))
                    {
                        return (dampener && SafeWithDampener(report));

                    }
                    if (currentMode == SequenceMode.Increase && report[j] > report[j + 1])
                    {
                        return (dampener && SafeWithDampener(report));
                    }
                    else if (currentMode == SequenceMode.Decrease && report[j + 1] > report[j])
                    {
                        return (dampener && SafeWithDampener(report));
                    }
                }
                return safeReport;
            }
            return true;
        }

        private static bool IsValidDifference(int num1, int num2)
        {
            return Math.Abs(num1 - num2) is >= 1 and <= 3 ? true : false;
        }
        private static bool SafeWithDampener(int[] report)
        {
            for (int i = 0; i < report.Length; i++)
            {
                List<int> tempList = new List<int>(report);
                tempList.RemoveAt(i);
                if (IsSafeReport(tempList.ToArray()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
