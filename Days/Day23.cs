using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;
using System.IO;
using static advent2024.Days.Day13;
using System.ComponentModel;
using System.Reflection;

namespace advent2024.Days
{
    public static class Day23
    {
        private static readonly string InputFile = @"C:\aoc\2024\day23\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day23\output.txt";
        public static void SolvePart1()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            Dictionary<string, HashSet<string>> connections = GetConnectionsFromFile(InputFile);
            HashSet<HashSet<string>> sets = FindSetsOfThreeForChar(connections, 't');
            stopwatch.Stop();
            Console.WriteLine($"23*1 -- {sets.Count()} ({stopwatch.ElapsedMilliseconds} ms)");
        }
        public static void SolvePart2()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            File.WriteAllText(OutputFile, string.Empty);
            Dictionary<string, HashSet<string>> connections = GetConnectionsFromFile(InputFile);
            HashSet<string> set = FindLargestCliqueWithBronKerbosch(connections);
            stopwatch.Stop();
            Console.WriteLine($"23*2 -- {string.Join(",", set.OrderBy(x => x))} ({stopwatch.ElapsedMilliseconds} ms)");
        }


        private static Dictionary<string, HashSet<string>> GetConnectionsFromFile(string inputFile)
        {
            string[] lines = File.ReadAllLines(InputFile);

            Dictionary<string, HashSet<string>> connections = new();

            foreach (string line in lines)
            {
                string[] pcs = line.Trim().Split('-');
                if (!connections.ContainsKey(pcs[0]))
                {
                    connections[pcs[0]] = new HashSet<string>();
                }
                connections[pcs[0]].Add(pcs[1]);
                if (!connections.ContainsKey(pcs[1]))
                {
                    connections[pcs[1]] = new HashSet<string>();
                }
                connections[pcs[1]].Add(pcs[0]);
            }
            return connections;
        }

        private static void PrintConnections(Dictionary<string, HashSet<string>> connections)
        {
            foreach (var node in connections.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{node.Key}: {string.Join(", ", node.Value.OrderBy(x => x))}");
            }
        }

        private static HashSet<HashSet<string>> FindSetsOfThreeForChar(Dictionary<string, HashSet<string>> connections, char c)
        {
            HashSet<HashSet<string>> sets = new HashSet<HashSet<string>>(HashSet<string>.CreateSetComparer());
            List<string> nodes = connections.Keys.ToList();
            foreach (string cNode in nodes.Where(n => n.StartsWith(c)))
            {
                HashSet<string> neighbors = connections[cNode];
                foreach (string n1 in neighbors)
                {
                    foreach (string n2 in neighbors)
                    {
                        if (n1 == n2)
                            continue;
                        if (connections[n1].Contains(n2))
                            sets.Add(new HashSet<string> { cNode, n1, n2 });
                    }
                }
            }
            return sets;
        }

        public static HashSet<string> FindLargestCliqueWithBronKerbosch(Dictionary<string, HashSet<string>> connections)
        {
            var maxClique = new HashSet<string>();
            var potentialNodes = new HashSet<string>(connections.Keys);
            var excludedNodes = new HashSet<string>();
            var currentClique = new HashSet<string>();

            BronKerbosch(currentClique, potentialNodes, excludedNodes, connections, ref maxClique);
            return maxClique;
        }

        private static void BronKerbosch(
            HashSet<string> currentClique,
            HashSet<string> potential,
            HashSet<string> excluded,
            Dictionary<string, HashSet<string>> connections,
            ref HashSet<string> maxClique)
        {
            if (potential.Count == 0 && excluded.Count == 0)
            {
                // Found a maximal clique
                if (currentClique.Count > maxClique.Count)
                {
                    maxClique = new HashSet<string>(currentClique);
                }
                return;
            }

            // Choose pivot from potential ∪ excluded that maximizes |neighbors ∩ potential|
            var pivotNode = potential.Union(excluded)
                .OrderByDescending(node =>
                    connections[node].Intersect(potential).Count())
                .FirstOrDefault();

            // For each vertex not connected to pivot
            var verticesToProcess = pivotNode != null
                ? potential.Except(connections[pivotNode])
                : potential;

            foreach (var vertex in verticesToProcess.ToList())
            {
                var vertexNeighbors = connections[vertex];

                // Recursive call
                currentClique.Add(vertex);
                BronKerbosch(
                    currentClique,
                    potential.Intersect(vertexNeighbors).ToHashSet(),
                    excluded.Intersect(vertexNeighbors).ToHashSet(),
                    connections,
                    ref maxClique
                );
                currentClique.Remove(vertex);

                potential.Remove(vertex);
                excluded.Add(vertex);
            }
        }
    }

}
