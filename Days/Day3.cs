using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using static advent2024.Helper;

namespace advent2024.Days
{
    public static class Day3
    {
        private static readonly string InputFile = @"C:\aoc\2024\day3\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day3\output.txt";
        public enum SequenceMode
        {
            Increase,
            Decrease,
            None
        }

        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);

            var file = File.ReadAllText(InputFile);
            int result = 0;

            string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";

            MatchCollection matches = Regex.Matches(file, pattern);

            foreach (Match match in matches)
            {
                result += (int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
            }

            Console.WriteLine($"3*1 -- {result}");
        }

        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);

            var file = File.ReadAllText(InputFile);
            int result = 0;

            string pattern = @"(?:do\(\)|don't\(\))";

            MatchCollection matches = Regex.Matches(file, pattern);

            List<int> enabledStarts = new List<int>();
            List<int> disabledStarts = new List<int>();

            enabledStarts.Add(0);
            bool isEnabled = true;
            foreach (Match match in matches)
            {
                if ((match.Value == "do()" && isEnabled) || (match.Value == "don't()" && !isEnabled))
                    continue;

                if (isEnabled)
                {
                    disabledStarts.Add(match.Index);
                }
                else
                {
                    enabledStarts.Add(match.Index);
                }
                isEnabled = !isEnabled;
            }

            if (enabledStarts.Count > disabledStarts.Count)
            {
                disabledStarts.Add(int.MaxValue);
            }
            pattern = @"mul\((\d{1,3}),(\d{1,3})\)";

            matches = Regex.Matches(file, pattern);

            foreach (Match match in matches)
            {
                int mulIndex = match.Index;
                int belowStart = enabledStarts.Where(x => x <= mulIndex).Max();
                int belowStartIndex = enabledStarts.FindIndex(x => x == belowStart);

                if (mulIndex < disabledStarts[belowStartIndex])
                {
                    result += (int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
                }
            }

            Console.WriteLine($"3*2 -- {result}");
        }
    }

}
