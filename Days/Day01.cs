using static advent2024.Helper;

namespace advent2024.Days
{
    public static class Day01
    {
        private static readonly string InputFile = @"C:\aoc\2024\day1\input.txt";

        public static void SolvePart1()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var lines = File.ReadAllLines(InputFile);
            int result = 0;
            int[] l = new int[lines.Length];
            int[] r = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] nums = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                l[i] = int.Parse(nums[0]);
                r[i] = int.Parse(nums[1]);
            }
            Array.Sort(l);
            Array.Sort(r);

            for (int i = 0; i < l.Length; i++)
            {
                result +=  Math.Abs(l[i] - r[i]);
            }
            stopwatch.Stop();
            Console.WriteLine($"01*1 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }

        public static void SolvePart2()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var lines = File.ReadAllLines(InputFile);
            int result = 0;

            int[] l = new int[lines.Length];
            int[] r = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] nums = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                l[i] = (int.Parse(nums[0]));
                r[i] = (int.Parse(nums[1]));
            }

            for (int i = 0; i < l.Length; i++)
            {
                result += l[i] * r.Where(x => x == l[i]).Count();
            }
            stopwatch.Stop();
            Console.WriteLine($"01*2 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }
    }
}
