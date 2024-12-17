using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;

namespace advent2024.Days
{
    public static class Day17
    {
        private static readonly string InputFile = @"C:\aoc\2024\day17\test.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day17\output.txt";
        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            Threebit t = GetThreebitFromFile(InputFile);
            List<long> result = RunThreebit(t);
            Console.WriteLine($"17*1 -- {String.Join(',', result)}");

        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);
            Threebit t = GetThreebitFromFile(InputFile);
            long init = 0;
            t.RegA = 0;
            t.RegB = 0;
            t.RegC = 0;
            List<long> result = RunThreebit(t);
            //File.AppendAllText(OutputFile, $"[{init}] -- {String.Join(',', result)}\n");
            int programCount = t.Program.Count();
            while (!result.SequenceEqual(t.Program))
            {
                t.RegA = ++init;
                t.RegB = 0;
                t.RegC = 0;
                result = RunThreebit(t);
                if (result.Count() != programCount)
                {
                    List<long> subSeq = t.Program.Skip(programCount - result.Count()).ToList();
                    File.AppendAllText(OutputFile, $"[{init}] -- {String.Join(',', result)}\n");
                    if (result.SequenceEqual(subSeq))
                    {
                        init = init * 8;
                        t.RegA = init;
                        t.RegB = 0;
                        t.RegC = 0;
                        result = RunThreebit(t);
                    }
                }
            }
            Console.WriteLine($"17*2 -- {init}");
        }

        private class Threebit
        {
            public long RegA { get; set; }
            public long RegB { get; set; }
            public long RegC { get; set; }
            public List<long> Program { get; set; }

            public long combo(long v)
            {
                switch (v)
                {
                    case 0 or 1 or 2 or 3:
                        return v;
                    case 4:
                        return RegA;
                    case 5:
                        return RegB;
                    case 6:
                        return RegC;
                    case 7:
                        return -1;
                    default:
                        return -1;
                }
            }
        }

        private static Threebit GetThreebitFromFile(string inputFile)
        {
            Threebit t = new Threebit();
            string[] lines = File.ReadAllText(InputFile).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (line.StartsWith("Register A:"))
                {
                    t.RegA = long.Parse(line.Split(':')[1].Trim());
                }
                else if (line.StartsWith("Register B:"))
                {
                    t.RegB = long.Parse(line.Split(':')[1].Trim());
                }
                else if (line.StartsWith("Register C:"))
                {
                    t.RegC = long.Parse(line.Split(':')[1].Trim());
                }
                else if (line.StartsWith("Program:"))
                {
                    string programStr = line.Split(':')[1].Trim();
                    t.Program = programStr.Split(',').Select(x => long.Parse(x.Trim())).ToList();
                }
            }
            return t;
        }

        private static List<long> RunThreebit(Threebit t)
        {
            List<long> result = new List<long>();
            for (int i = 0; i < t.Program.Count(); i += 2)
            {
                long v = t.Program[i + 1];
                switch (t.Program[i])
                {
                    case 0:     //adv: regA / 2^combo[v] => trunc to int => A
                        t.RegA = t.RegA >> (int)t.combo(v);
                        //t.RegA = t.RegA / (int)Math.Pow(2, t.combo(v));
                        break;
                    case 1:     //bxl: regB XOR v => B
                        t.RegB = t.RegB ^ v;
                        break;
                    case 2:     //bst: combo[v] % 8 => B
                        t.RegB = t.combo(v) % 8;
                        break;
                    case 3:     //jnz: if regA == 0 do nothing else jump to v, do not i += 2
                        if (t.RegA == 0)
                            break;
                        else
                        {
                            i = (int)v - 2;
                            break;
                        }
                    case 4:     //bxc: regB XOR regC => B
                        t.RegB = t.RegB ^ t.RegC;
                        break;
                    case 5:     //out: combo[v] % 8 => output
                        result.Add(t.combo(v) % 8);
                        break;
                    case 6:     //bdv: regA / 2^combo[v] => trunc to int => B
                        t.RegB = t.RegA >> (int)t.combo(v);
                        break;
                    case 7:     //cdv: regA / 2^combo[v] => trunc to int => C
                        t.RegC = t.RegA >> (int)t.combo(v);
                        break;
                }
            }
            return result;
        }
    }
}
