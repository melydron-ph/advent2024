using System.Drawing;
using static advent2024.Helper;

namespace advent2024.Days
{
    public static class Day21
    {
        private static readonly string InputFile = @"C:\aoc\2024\day21\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day21\output.txt";

        public static void SolvePart1()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);

            string numpadString = $"789\n456\n123\n#0A";
            string dirpadString = $"#^A\n<v>";
            var numKeypadMoves = GetPadPaths(numpadString.Split('\n'));
            var dirKeypadMoves = GetPadPaths(dirpadString.Split('\n'));
            var filteredNumMoves = numKeypadMoves.Where(kvp => !dirKeypadMoves.ContainsKey(kvp.Key));
            var allMoves = filteredNumMoves.Concat(dirKeypadMoves).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            int robotNum = 2;
            long result = GetResultForCodes(allMoves, robotNum);
            stopwatch.Stop();
            Console.WriteLine($"21*1 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }

        public static void SolvePart2()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            string numpadString = $"789\n456\n123\n#0A";
            string dirpadString = $"#^A\n<v>";
            var numKeypadMoves = GetPadPaths(numpadString.Split('\n'));
            var dirKeypadMoves = GetPadPaths(dirpadString.Split('\n'));

            var filteredNumMoves = numKeypadMoves.Where(kvp => !dirKeypadMoves.ContainsKey(kvp.Key));
            var allMoves = filteredNumMoves.Concat(dirKeypadMoves).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            var resultCache = new Dictionary<(int level, string key), long>();
            int robotNum = 25;
            long result = GetResultForCodes(allMoves, robotNum);
            stopwatch.Stop();
            Console.WriteLine($"21*2 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }

        private static long GetResultForCodes(Dictionary<(char from, char to), string[]> allMoves, int robotNum)
        {

            var resultCache = new Dictionary<(int level, string key), long>();
            long result = 0;

            string[] codes = File.ReadAllLines(InputFile);
            foreach (string code in codes)
            {
                int value = int.Parse(code.Substring(0, code.Length - 1));
                long seqLength = ShortestMoves(code, 0, robotNum, resultCache, allMoves);
                result += value * seqLength;
            }
            return result;
        }

        private static long ShortestMoves(string current, int level, int stopAtLevel,
            Dictionary<(int level, string key), long> cache, Dictionary<(char from, char to), string[]> moves)
        {
            if (string.IsNullOrEmpty(current)) return 0;
            if (cache.TryGetValue((level, current), out var cachedResult)) return cachedResult;

            if (level == stopAtLevel)
            {
                var result = GetSequences(current, moves).Select(a => a.Length).Min();
                cache.Add((level, current), result);
                return result;
            }

            var firstA = current.IndexOf('A');
            var firstPart = current.Substring(0, firstA + 1);
            var secondPart = current.Substring(firstA + 1);

            long shortest = -1;
            var possibilities = GetSequences(firstPart, moves);

            foreach (var seq in possibilities)
            {
                long count = ShortestMoves(seq, level + 1, stopAtLevel, cache, moves);
                if (shortest > count || shortest == -1) shortest = count;
            }

            if (!string.IsNullOrEmpty(secondPart))
            {
                shortest += ShortestMoves(secondPart, level, stopAtLevel, cache, moves);
            }

            cache.Add((level, current), shortest);
            return shortest;
        }

        private static List<string> GetSequences(string entryCode, Dictionary<(char from, char to), string[]> keypadMoves)
        {
            var sequence = new List<string>() { "" };
            char prevKey = 'A';

            foreach (var key in entryCode)
            {
                var newSequence = new List<string>();
                var moves = keypadMoves[(prevKey, key)];

                foreach (var prevStrokes in sequence)
                {
                    foreach (var nextStroke in moves)
                    {
                        newSequence.Add(prevStrokes + nextStroke + 'A');
                    }
                }

                prevKey = key;
                sequence = newSequence;
            }

            return sequence;
        }

        private static char ReverseDirection(char direction)
        {
            return direction switch
            {
                '>' => '<',
                '<' => '>',
                '^' => 'v',
                'v' => '^',
                _ => direction
            };
        }

        private static string GetReversePath(string path)
        {
            var reversedChars = path.Reverse();

            var oppositeDirections = reversedChars.Select(ReverseDirection);

            return string.Concat(oppositeDirections);
        }

        private static Dictionary<(char from, char to), string[]> GetPadPaths(string[] keypad)
        {
            var keypadMoves = new Dictionary<(char from, char to), string[]>();
            var charLocations = new Dictionary<char, (int x, int y)>();
            var validKeys = new HashSet<char>();

            for (int i = 0; i < keypad[0].Length; i++)
            {
                for (int j = 0; j < keypad.Length; j++)
                {
                    char currentKey = keypad[j][i];
                    if (currentKey != '#')
                    {
                        validKeys.Add(currentKey);
                        charLocations[currentKey] = (i, j);
                    }
                }
            }

            foreach (char fromKey in validKeys)
            {
                var startPos = charLocations[fromKey];

                foreach (char toKey in validKeys)
                {
                    if (keypadMoves.ContainsKey((fromKey, toKey)))
                        continue;

                    var state = new Dictionary<(int x, int y), (int cost, HashSet<string> options)>();
                    var work = new PriorityQueue<(int x, int y), int>();

                    work.Enqueue((startPos.x, startPos.y), 0);
                    state.Add(startPos, (0, new HashSet<string> { "" }));

                    while (work.Count > 0)
                    {
                        var (currentX, currentY) = work.Dequeue();
                        var currentState = state[(currentX, currentY)];

                        if (keypad[currentY][currentX] == toKey)
                        {
                            keypadMoves.Add((fromKey, toKey), currentState.options.ToArray());

                            if (fromKey != toKey)
                            {
                                var reverseOptions = new HashSet<string>();
                                foreach (string path in currentState.options)
                                {
                                    string reversePath = GetReversePath(path);
                                    reverseOptions.Add(reversePath);
                                }
                                keypadMoves.Add((toKey, fromKey), reverseOptions.ToArray());
                            }
                            break;
                        }

                        var moves = new[] {
                            (x: currentX + 1, y: currentY, dir: '>'),
                            (x: currentX - 1, y: currentY, dir: '<'),
                            (x: currentX, y: currentY + 1, dir: 'v'),
                            (x: currentX, y: currentY - 1, dir: '^')
                        };

                        foreach (var (moveX, moveY, direction) in moves)
                        {
                            if (moveX < 0 || moveY < 0 || moveX >= keypad[0].Length || moveY >= keypad.Length)
                                continue;
                            if (keypad[moveY][moveX] == ' ' || keypad[moveY][moveX] == '#')
                                continue;

                            int newCost = currentState.cost + 1;
                            bool seenBefore = state.TryGetValue((moveX, moveY), out var nextState);

                            if (!seenBefore)
                            {
                                nextState = (newCost, new HashSet<string>());
                                state[(moveX, moveY)] = nextState;
                            }

                            if (newCost == nextState.cost)
                            {
                                foreach (string path in currentState.options)
                                {
                                    nextState.options.Add(path + direction);
                                }

                                if (!seenBefore)
                                {
                                    work.Enqueue((moveX, moveY), newCost);
                                }
                            }
                        }
                    }
                }
            }
            return keypadMoves;
        }
    }
}