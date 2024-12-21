using advent2024.Days;

//Solver.RunAll();

Solver.RunDay(21);

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();

public static class Solver
{
    private static readonly Dictionary<int, Action[]> DaySolutions = new()
    {
        { 1, new Action[] { Day01.SolvePart1, Day01.SolvePart2 } },
        { 2, new Action[] { Day02.SolvePart1, Day02.SolvePart2 } },
        { 3, new Action[] { Day03.SolvePart1, Day03.SolvePart2 } },
        { 4, new Action[] { Day04.SolvePart1, Day04.SolvePart2 } },
        { 5, new Action[] { Day05.SolvePart1, Day05.SolvePart2 } },
        { 6, new Action[] { Day06.SolvePart1, Day06.SolvePart2 } },
        { 7, new Action[] { Day07.SolvePart1, Day07.SolvePart2 } },
        { 8, new Action[] { Day08.SolvePart1, Day08.SolvePart2 } },
        { 9, new Action[] { Day09.SolvePart1, Day09.SolvePart2 } },
        { 10, new Action[] { Day10.SolvePart1, Day10.SolvePart2 } },
        { 11, new Action[] { Day11.SolvePart1, Day11.SolvePart2 } },
        { 12, new Action[] { Day12.SolvePart1, Day12.SolvePart2 } },
        { 13, new Action[] { Day13.SolvePart1, Day13.SolvePart2 } },
        { 14, new Action[] { Day14.SolvePart1, Day14.SolvePart2 } },
        { 15, new Action[] { Day15.SolvePart1, Day15.SolvePart2 } },
        { 16, new Action[] { Day16.SolvePart1, Day16.SolvePart2 } },
        { 17, new Action[] { Day17.SolvePart1, Day17.SolvePart2 } },
        { 18, new Action[] { Day18.SolvePart1, Day18.SolvePart2 } },
        { 19, new Action[] { Day19.SolvePart1, Day19.SolvePart2 } },
        { 20, new Action[] { Day20.SolvePart1, Day20.SolvePart2 } },
        { 21, new Action[] { Day21.SolvePart1, Day21.SolvePart2 } },
        { 22, new Action[] { Day22.SolvePart1, Day22.SolvePart2 } },
        { 23, new Action[] { Day23.SolvePart1, Day23.SolvePart2 } },
        { 24, new Action[] { Day24.SolvePart1, Day24.SolvePart2 } },
        { 25, new Action[] { Day25.SolvePart1, Day25.SolvePart2 } }
    };

    public static void RunAll()
    {
        foreach (var day in DaySolutions.Keys.OrderBy(k => k))
        {
            RunDay(day, true);
        }
    }

    public static void RunDay(int day, bool both = true)
    {
        if (!DaySolutions.TryGetValue(day, out var solutions))
        {
            Console.WriteLine($"Day {day} solutions not found.");
            return;
        }

        //try
        //{
            if (both)
            {
                solutions[0]();
                solutions[1](); 
            }
            else
            {
                solutions[0]();
            }
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Error running Day {day}: {ex.Message}");
        //}
    }
}