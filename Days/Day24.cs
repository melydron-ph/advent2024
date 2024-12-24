using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;
using static advent2024.Days.Day13;
using System.ComponentModel;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace advent2024.Days
{
    public static class Day24
    {
        private static readonly string InputFile = @"C:\aoc\2024\day24\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day24\output.txt";
        public static void SolvePart1()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            Dictionary<string, Wire> wires;
            List<Gate> gates;
            GetWiresAndGates(InputFile, out wires, out gates);
            stopwatch.Stop();
            long result = GetGatesResult(wires, gates);
            Console.WriteLine($"24*1 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }
        public static void SolvePart2()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            string[] lines = File.ReadAllLines(InputFile);
            int result = 0;
            stopwatch.Stop();
            Console.WriteLine($"24*2 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }

        private static void GetWiresAndGates(string inputFile, out Dictionary<string, Wire> wires, out List<Gate> gates)
        {
            string file = File.ReadAllText(inputFile);
            string[] fileBlocks = file.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] wireLines = fileBlocks[0].Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] gateLines = fileBlocks[1].Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            wires = new();
            foreach (string wireLine in wireLines)
            {
                string[] lineParts = wireLine.Split(':');
                string name = lineParts[0].Trim();
                bool value = lineParts[1].Trim() == "1";
                wires[name] = new Wire(name, value);
            }
            gates = new();
            foreach (string gateLine in gateLines)
            {
                string[] gateParts = gateLine.Split(' ');
                string inputName1 = gateParts[0].Trim();
                string inputName2 = gateParts[2].Trim();
                string type = gateParts[1].Trim();
                string outputName = gateParts[4].Trim();
                GateType gateType = type switch
                {
                    "AND" => GateType.AND,
                    "OR" => GateType.OR,
                    "XOR" => GateType.XOR
                };
                if (!wires.ContainsKey(inputName1))
                {
                    wires[inputName1] = new Wire(inputName1);
                }
                if (!wires.ContainsKey(inputName2))
                {
                    wires[inputName2] = new Wire(inputName2);
                }
                if (!wires.ContainsKey(outputName))
                {
                    wires[outputName] = new Wire(outputName);
                }
                gates.Add(new Gate(wires[inputName1], wires[inputName2], wires[outputName], gateType));
            }
        }

        private static long GetGatesResult(Dictionary<string, Wire> wires, List<Gate> gates)
        {
            bool evaluating = true;
            while (evaluating)
            {
                evaluating = false;
                foreach (Gate gate in gates)
                {
                    if (gate.CanEvaluate)
                    {
                        evaluating = true;
                        gate.Evaluate();
                    }
                }
            }
            string binaryString = string.Empty;
            foreach (var wire in wires.Values.Where(w => w.Name.StartsWith("z")).OrderBy(w => w.Name))
            {
                binaryString += wire.Value.Value ? "1" : "0";
            }
            string reverseString = new string(binaryString.Reverse().ToArray());
            return Convert.ToInt64(reverseString, 2);
        }


    }

}
