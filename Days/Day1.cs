using static advent2024.Helper;

namespace advent2024.Days
{
    public static class Day1
    {
        private static readonly string InputFile = @"C:\aoc\2024\day1\input.txt";

        public static void SolvePart1()
        {
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
            Console.WriteLine($"1*1 -- {result}");
        }

        public static void SolvePart2()
        {
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
            Console.WriteLine($"1*2 -- {result}");
        }
    }
}
