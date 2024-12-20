﻿using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;
using static advent2024.Days.Day13;
using System.ComponentModel;

namespace advent2024.Days
{
    public static class Day20
    {
        private static readonly string InputFile = @"C:\aoc\2024\day20\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day20\output.txt";
        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int mapX = lines[0].Length;
            int mapY = lines.Count();
            char[,] map = new char[mapX, mapY];
            Point start = new Point(0, 0);
            Point end = new Point(0, 0);
            for (int i = 0; i < mapY; i++)
            {
                string line = lines[i];
                for (int j = 0; j < mapX; j++)
                {
                    map[i, j] = line[j];
                    if (map[i, j] == 'S')
                    {
                        start.X = i;
                        start.Y = j;
                    }
                    if (map[i, j] == 'E')
                    {
                        end.X = i;
                        end.Y = j;
                    }
                }
            }

            //PrintMap(map);
            //Console.WriteLine($"Start: {start}, End: {end}");
            List<Point> raceTrack = FindShortestPath(map, start, end);

            foreach (Point p in raceTrack)
            {
                TryCheat(raceTrack, p);
            }


            int result = 0;

            foreach (KeyValuePair<int, int> pair in cheats)
            {
                if (pair.Key >= 100)
                {
                    result += pair.Value;
                    //Console.WriteLine($"There are {pair.Value} cheats that save {pair.Key} seconds.");
                }
            }

            Console.WriteLine($"20*1 -- {result}");
        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);
            long result = 0;
            Console.WriteLine($"20*2 -- {result}");
        }

        private static SortedList<int, int> cheats = new SortedList<int, int>();
        private static void TryCheat(List<Point> raceTrack, Point p)
        {
            int cheatStartIndex = raceTrack.IndexOf(p);
            foreach (Direction dir in Enum.GetValues<Direction>())
            {
                Point offset = GetNextPoint(dir);
                Point step1 = new Point(
                    p.X + offset.X,
                    p.Y + offset.Y
                );
                Point step2 = new Point(
                    p.X + 2 * offset.X,
                    p.Y + 2 * offset.Y
                );
                int cheatEndIndex = raceTrack.IndexOf(step2);

                if (!raceTrack.Contains(step1) && raceTrack.Contains(step2) && cheatEndIndex > cheatStartIndex)
                {
                    int secondsSaved = cheatEndIndex - cheatStartIndex - 2;
                    if (secondsSaved > 0)
                        if (!cheats.ContainsKey(secondsSaved))
                            cheats[secondsSaved] = 1;
                        else
                            cheats[secondsSaved]++;
                }
            }
        }
    }
}
