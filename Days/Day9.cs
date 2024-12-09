using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Drawing;
using static advent2024.Helper;
using System.Globalization;

namespace advent2024.Days
{
    public static class Day9
    {
        private static readonly string InputFile = @"C:\aoc\2024\day9\input.txt";
        private static readonly string OutputFile = @"C:\aoc\2024\day9\output.txt";

        public static void SolvePart1()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string line = File.ReadAllText(InputFile);
            List<int> blocks = GetBlocksFromLine(line);
            blocks = CompactBlocks(blocks);
            long result = GetChecksum(blocks);
            Console.WriteLine($"9*1 -- {result}");

        }
        public static void SolvePart2()
        {
            File.WriteAllText(OutputFile, string.Empty);
            string line = File.ReadAllText(InputFile);
            List<int> blocks = GetBlocksFromLine(line);
            File.AppendAllLines(OutputFile, blocks.Select(x => x == -1 ? "." : x.ToString()));
            blocks = CompactBlocks(blocks, true);
            long result = GetChecksum(blocks);
            Console.WriteLine($"9*2 -- {result}");
        }

        private static List<int> GetBlocksFromLine(string line)
        {
            bool block = true;
            int id = 0;
            List<int> blocks = new List<int>();
            for (int i = 0; i < line.Length; i++)
            {
                int num = (int)char.GetNumericValue(line[i]);
                if (block)
                {
                    for (int b = 0; b < num; b++)
                    {
                        blocks.Add(id);
                    }
                    id++;
                }
                else
                {
                    for (int b = 0; b < num; b++)
                    {
                        blocks.Add(-1);
                    }
                }
                block = !block;
            }

            return blocks;
        }

        private static List<int> CompactBlocks(List<int> blocks, bool part2 = false)
        {
            int curIndex = 0;
            List<int> alreadyMoved = new List<int>();
            for (int j = blocks.Count - 1; j > 0; j--)
            {
                if (!part2)
                {
                    if (j <= curIndex)
                    {
                        break;
                    }
                }
                if (blocks[j] > 0 && !alreadyMoved.Contains(j))
                {
                    //Console.WriteLine($"Found candidate: {blocks[j]} @ {j}");
                    //File.AppendAllText(OutputFile, $"Found candidate: {blocks[j]} @ {j}\n");
                    if (!part2)
                        for (int i = curIndex; i < blocks.Count; i++)
                        {
                            curIndex++;
                            if (j <= curIndex)
                            {
                                break;
                            }
                            if (blocks[i] == -1)
                            {
                                blocks[i] = blocks[j];
                                blocks[j] = -1;
                                break;
                            }
                        }
                    else
                    {
                        int blockLength = 0;
                        int blockEnd = j;
                        int blockNum = blocks[j];
                        while (blockNum == blocks[j])
                        {
                            blockLength++;
                            j--;
                        }
                        j++;
                        for (int i = 0; i < blocks.Count; i++)
                        {

                            if (i >= j)
                                break;
                            if (blocks[i] == -1)
                            {
                                //Console.WriteLine($"Found empty space start @ {i}");
                                int emptyLength = 0;
                                int emptyLengthStart = i;
                                bool blockMoved = false;
                                while (-1 == blocks[i] && !blockMoved)
                                {
                                    emptyLength++;
                                    i++;
                                    if (emptyLength >= blockLength)
                                    {
                                        //File.AppendAllText(OutputFile, $"Moving {blockNum}({blockLength}) to {emptyLengthStart}\n");
                                        //Console.WriteLine($"Moving {blockNum}({blockLength}) to {emptyLengthStart}");

                                        for (int r = 0; r < blockLength; r++)
                                        {
                                            alreadyMoved.Add(emptyLengthStart);
                                            blocks[emptyLengthStart++] = blockNum;
                                            blocks[blockEnd--] = -1;
                                            blockMoved = true;
                                        }
                                    }
                                }
                                if (blockMoved)
                                    break;
                            }
                        }
                    }
                }
            }
            return blocks;
        }

        private static long GetChecksum(List<int> blocks)
        {
            long result = 0;
            for (int i = 0; i < blocks.Count - 1; i++)
            {
                if (blocks[i] < 0) continue;
                result += i * blocks[i];
                //Console.WriteLine(i + ": " + blocks[i] + " == " + result);
            }
            return result;
        }
    }
}
