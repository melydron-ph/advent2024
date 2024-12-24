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
            long result = GetGatesResult(wires, gates);
            stopwatch.Stop();
            Console.WriteLine($"24*1 -- {result} ({stopwatch.ElapsedMilliseconds} ms)");
        }
        public static void SolvePart2()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            Dictionary<string, Wire> wires;
            List<Gate> gates;
            GetWiresAndGates(InputFile, out wires, out gates);
            long num1 = GetWiresValue(wires, 'x');
            long num2 = GetWiresValue(wires, 'y');
            long expectedResult = num1 + num2;
            if (InputFile.Contains("test2.txt"))
            {
                expectedResult = num1 & num2;
            }
            int bitLength = wires.Count(w => w.Key.StartsWith("z"));
            long currentResult = GetGatesResult(wires, gates);
            AnalyzeBitDifferences(expectedResult, currentResult, bitLength);
            AnalyzeSwapEffects(wires, gates, expectedResult, bitLength);
            //if (expectedResult != currentResult)
            //{
            //    //Console.WriteLine($"Current circuit result: {currentResult}");
            //    //Console.WriteLine($"Expected result: {expectedResult}");
            //    for (int i = 0; i < gates.Count; i++)
            //    {
            //        for (int j = 0; j < gates.Count; j++)
            //        {
            //            if (j == i) continue;  // Skip same gate

            //            for (int k = 0; k < gates.Count; k++)
            //            {
            //                if (k == i || k == j) continue;  // Skip already used gates

            //                for (int l = 0; l < gates.Count; l++)
            //                {
            //                    if (l == i || l == j || l == k) continue;  // Skip already used gates
            //                    var testWires = CloneWires(wires);
            //                    var testGates = CloneGates(gates, testWires);
            //                    //Console.WriteLine($"Trying: {testGates[i].Output.Name} <-> {testGates[j].Output.Name} and {testGates[k].Output.Name} <-> {testGates[l].Output.Name}");
            //                    SwapGateOutputs(testGates[i], testGates[j]);
            //                    SwapGateOutputs(testGates[k], testGates[l]);
            //                    long result = GetGatesResult(testWires, testGates);
            //                    //Console.WriteLine($"Result after swap: {result}");
            //                    if (result == expectedResult)
            //                    {
            //                        Console.WriteLine($"\n!!");
            //                        Console.WriteLine($"Pair 1: {gates[i]} , {gates[j]}");
            //                        Console.WriteLine($"Pair 2: {gates[k]} , {gates[l]}");
            //                        return;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            stopwatch.Stop();
            //int result = 0;
            Console.WriteLine($"24*2 -- ({stopwatch.ElapsedMilliseconds} ms)");
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

        private static long GetWiresValue(Dictionary<string, Wire> wires, char c)
        {
            string binaryString = string.Empty;
            foreach (var wire in wires.Values.Where(w => w.Name.StartsWith(c)).OrderBy(w => w.Name))
            {
                binaryString += wire.Value.Value ? "1" : "0";
            }
            string reverseString = new string(binaryString.Reverse().ToArray());
            return Convert.ToInt64(reverseString, 2);
        }

        private static Dictionary<string, Wire> CloneWires(Dictionary<string, Wire> original)
        {
            var clone = new Dictionary<string, Wire>();
            foreach (var kvp in original)
            {
                clone[kvp.Key] = new Wire(kvp.Key, kvp.Value.Value);
            }
            return clone;
        }

        private static List<Gate> CloneGates(List<Gate> original, Dictionary<string, Wire> newWires)
        {
            return original.Select(g => new Gate(
                newWires[g.Input1.Name],
                newWires[g.Input2.Name],
                newWires[g.Output.Name],
                g.Type
            )).ToList();
        }

        private static void SwapGateOutputs(Gate gate1, Gate gate2)
        {
            //Wire tempOutput = gate1.Output;
            //gate1.Output = gate2.Output;
            //gate2.Output = tempOutput;

            string tempName = gate1.Output.Name;
            gate1.Output.Name = gate2.Output.Name;
            gate2.Output.Name = tempName;
        }

        private static void AnalyzeBitDifferences(long expected, long current, int bitLength)
        {
            string expectedBits = Convert.ToString(expected, 2).PadLeft(bitLength, '0');
            string currentBits = Convert.ToString(current, 2).PadLeft(bitLength, '0');

            Console.WriteLine($"\nExpected: {expectedBits}");
            Console.WriteLine($"Current:  {currentBits}");

            List<int> wrongPositions = new List<int>();
            for (int i = 0; i < bitLength; i++)
            {
                if (expectedBits[i] != currentBits[i])
                {
                    wrongPositions.Add(bitLength - 1 - i);
                    Console.WriteLine($"Bit position {bitLength - 1 - i} is wrong (z{(bitLength - 1 - i).ToString().PadLeft(2, '0')})");
                }
            }
        }

        private class SwapInfo
        {
            public string Wire1 { get; }
            public string Wire2 { get; }
            public int Improvements { get; }
            public int Regressions { get; }

            public SwapInfo(string wire1, string wire2, int improvements, int regressions)
            {
                Wire1 = wire1;
                Wire2 = wire2;
                Improvements = improvements;
                Regressions = regressions;
            }
        }

        private static void AnalyzeSwapEffects(Dictionary<string, Wire> wires, List<Gate> gates, long expectedResult, int bitLength)
        {
            string expectedBits = Convert.ToString(expectedResult, 2).PadLeft(bitLength, '0');
            var zGates = gates.Where(g => g.Output.Name.StartsWith("z")).ToList();
            var promisingSwaps = new List<SwapInfo>();

            // Try every possible single swap and collect the good ones
            for (int i = 0; i < zGates.Count; i++)
            {
                for (int j = i + 1; j < zGates.Count; j++)
                {
                    var testWires = CloneWires(wires);
                    var testGates = CloneGates(gates, testWires);

                    string wire1 = testGates[i].Output.Name;
                    string wire2 = testGates[j].Output.Name;
                    SwapGateOutputs(testGates[i], testGates[j]);

                    long resultAfterSwap = GetGatesResult(testWires, testGates);
                    string currentBits = Convert.ToString(resultAfterSwap, 2).PadLeft(bitLength, '0');

                    int improvements = 0;
                    int regressions = 0;
                    for (int bit = 0; bit < bitLength; bit++)
                    {
                        bool wasDifferent = expectedBits[bit] != Convert.ToString(GetGatesResult(wires, gates), 2).PadLeft(bitLength, '0')[bit];
                        bool isDifferent = expectedBits[bit] != currentBits[bit];

                        if (wasDifferent && !isDifferent) improvements++;
                        if (!wasDifferent && isDifferent) regressions++;
                    }

                    if (improvements > 0)
                    {
                        var swapInfo = new SwapInfo(wire1, wire2, improvements, regressions);
                        promisingSwaps.Add(swapInfo);
                    }
                }
            }

            // Sort by most improvements and least regressions
            var sortedSwaps = promisingSwaps
                .OrderByDescending(s => s.Improvements)
                .ThenBy(s => s.Regressions)
                .ToList();

            Console.WriteLine("\nMost promising swaps:");
            foreach (var swap in sortedSwaps)
            {
                Console.WriteLine($"{swap.Wire1} <-> {swap.Wire2}: Fixes {swap.Improvements}, Breaks {swap.Regressions}");
            }

            // Now try combinations of the most promising swaps
            Console.WriteLine("\nTrying combinations of promising swaps...");
            TrySwapCombinations(wires, gates, expectedResult, sortedSwaps, bitLength);
        }

        private static void TrySwapCombinations(Dictionary<string, Wire> wires, List<Gate> gates, long expectedResult, List<SwapInfo> swaps, int bitLength)
        {
            string expectedBits = Convert.ToString(expectedResult, 2).PadLeft(bitLength, '0');
            int bestDifference = int.MaxValue;
            string bestResult = "";
            var bestSwaps = new List<SwapInfo>();

            for (int i = 0; i < swaps.Count; i++)
            {
                for (int j = i + 1; j < swaps.Count; j++)
                {
                    for (int k = j + 1; k < swaps.Count; k++)
                    {
                        for (int l = k + 1; l < swaps.Count; l++)
                        {
                            var usedWires = new HashSet<string>
                    {
                        swaps[i].Wire1, swaps[i].Wire2,
                        swaps[j].Wire1, swaps[j].Wire2,
                        swaps[k].Wire1, swaps[k].Wire2,
                        swaps[l].Wire1, swaps[l].Wire2
                    };
                            if (usedWires.Count != 8) continue;

                            var testWires = CloneWires(wires);
                            var testGates = CloneGates(gates, testWires);

                            SwapGateOutputs(FindGate(testGates, swaps[i].Wire1), FindGate(testGates, swaps[i].Wire2));
                            SwapGateOutputs(FindGate(testGates, swaps[j].Wire1), FindGate(testGates, swaps[j].Wire2));
                            SwapGateOutputs(FindGate(testGates, swaps[k].Wire1), FindGate(testGates, swaps[k].Wire2));
                            SwapGateOutputs(FindGate(testGates, swaps[l].Wire1), FindGate(testGates, swaps[l].Wire2));

                            long result = GetGatesResult(testWires, testGates);
                            string resultBits = Convert.ToString(result, 2).PadLeft(bitLength, '0');

                            // Count different bits
                            int differences = 0;
                            for (int bit = 0; bit < bitLength; bit++)
                            {
                                if (expectedBits[bit] != resultBits[bit]) differences++;
                            }

                            if (differences < bestDifference)
                            {
                                bestDifference = differences;
                                bestResult = resultBits;
                                bestSwaps = new List<SwapInfo> { swaps[i], swaps[j], swaps[k], swaps[l] };

                                Console.WriteLine($"\nNew best combination found! Off by {differences} bits");
                                Console.WriteLine($"Expected: {expectedBits}");
                                Console.WriteLine($"Got:      {resultBits}");
                                Console.WriteLine("Swaps:");
                                Console.WriteLine($"1: {swaps[i].Wire1} <-> {swaps[i].Wire2}");
                                Console.WriteLine($"2: {swaps[j].Wire1} <-> {swaps[j].Wire2}");
                                Console.WriteLine($"3: {swaps[k].Wire1} <-> {swaps[k].Wire2}");
                                Console.WriteLine($"4: {swaps[l].Wire1} <-> {swaps[l].Wire2}");
                            }

                            if (differences == 0)
                            {
                                Console.WriteLine("\nFound exact solution!");
                                return;
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"\nBest attempt was off by {bestDifference} bits");
        }

        private static Gate FindGate(List<Gate> gates, string outputName)
        {
            return gates.First(g => g.Output.Name == outputName);
        }

    }

}
