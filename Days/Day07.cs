using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using static advent2024.Helper;

namespace advent2024.Days
{
    public static class Day07
    {
        private static readonly string InputFile = @"C:\aoc\2024\day7\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day7\output.txt";

        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            long result = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
                long resultValue = long.Parse(parts[0]);
                int[] numbers = Array.ConvertAll(parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray(), int.Parse);
                if (NumbersProduceValue(numbers, resultValue))
                {
                    result += resultValue;
                }
            }
            Console.WriteLine($"07*1 -- {result}");
        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            long result = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
                long resultValue = long.Parse(parts[0]);
                int[] numbers = Array.ConvertAll(parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray(), int.Parse);
                if (NumbersProduceValue(numbers, resultValue, true))
                {
                    result += resultValue;
                }
            }
            Console.WriteLine($"07*2 -- {result}");
        }
        internal static bool NumbersProduceValue(int[] numbers, long resultValue, bool part2 = false)
        {
            int operations = part2 ? 3 : 2;
            int totalCombinations = (int)Math.Pow(operations, numbers.Length - 1);
            int operationsSize = numbers.Length - 1;
            char[] currentOperators = new char[operationsSize];
            for (int i = 0; i < totalCombinations; i++)
            {
                int temp = i;
                for (int j = 0; j < operationsSize; j++)
                {
                    currentOperators[j] = temp % operations == 1 ? '+' : (temp % operations == 0 ? '*' : '|');
                    temp = temp / operations;
                }

                long result = numbers[0];
                for (int j = 0; j < operationsSize; j++)
                {
                    switch (currentOperators[j])
                    {
                        case '+':
                            result += numbers[j + 1];
                            break;
                        case '*':
                            result *= numbers[j + 1];
                            break;
                        case '|':
                            result = long.Parse(result.ToString() + numbers[j + 1].ToString());
                            break;
                    }
                }

                if (result == resultValue)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
