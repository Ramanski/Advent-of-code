using System.Text;

namespace Advent_of_Code
{
    internal class Day3 : ITaskSolver
    {
        public string GetTaskName() => "Day 3: Binary Diagnostic";

        private string[]? buffer;
        private string pathToInputFile = @"input/task3.txt";

        public void SolvePartOne()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);

            var summary = AggregateZerosAndOnes(buffer);
            var gammaRate = GetGammaRate(summary);
            var epsilonRate = GetEpsilonRate(gammaRate);

            Console.WriteLine("Done");
            ConsoleExtensions.DisplayResult(gammaRate * epsilonRate, buffer!.Length, gammaRate, epsilonRate);
        }

        public void SolvePartTwo()
        {
            if (buffer == null) buffer =  File.ReadAllLines(pathToInputFile, Encoding.UTF8);

            var oxygenRating = GetOxygenGeneratorRating(buffer);
            var scrubberRating = GetCO2ScrubberRating(buffer);

            Console.WriteLine("Done\n");
            ConsoleExtensions.DisplayResult(oxygenRating * scrubberRating, buffer!.Length, oxygenRating, scrubberRating);
        }

        private int[,] AggregateZerosAndOnes(string[] lines)
        {
            int[,] summary = new int[2, 12];

            foreach (string line in lines)
            {
                int position = 0;

                foreach (char element in line)
                {
                    summary[element == '0' ? 0 : 1, position++]++;
                }
            }

            summary.DisplayMatrix();

            return summary;
        }

        private char GetMostCommonBit(IEnumerable<string> lines, int position)
        {
            int aim = lines.Count() / 2 + 1;
            var enumerator = lines.GetEnumerator();
            int onesSum = 0;
            int zerosSum = 0;

            while (enumerator.MoveNext() && onesSum < aim && zerosSum < aim)
            {
                if (enumerator.Current[position] == '0')
                    zerosSum++;
                else
                    onesSum++;
            }

            Console.WriteLine("Needed to reach {0} of {1}", aim, lines.Count());
            Console.WriteLine("Zeros = {0}", zerosSum);
            Console.WriteLine("Ones = {0}", onesSum);

            return onesSum >= zerosSum ? '1' : '0';
        }

        private char GetLessCommonBit(IEnumerable<string> lines, int position)
        {
            int aim = lines.Count() / 2 + 1;
            var enumerator = lines.GetEnumerator();
            int onesSum = 0;
            int zerosSum = 0;


            while (enumerator.MoveNext() && onesSum < aim && zerosSum < aim)
            {
                if (enumerator.Current[position] == '0')
                    zerosSum++;
                else
                    onesSum++;
            }

            Console.WriteLine("Needed to reach {0} of {1}", aim, lines.Count());
            Console.WriteLine("Zeros = {0}", zerosSum);
            Console.WriteLine("Ones = {0}", onesSum);

            return zerosSum <= onesSum ? '0' : '1';
        }

        private int GetOxygenGeneratorRating(string[] lines)
        {
            List<string> actualLines = new(lines);
            for (int i = 0; actualLines.Count > 1 && i < 12; i++)
            {
                Console.WriteLine("Considering bit position #{0}", i);
                char commonBit = GetMostCommonBit(actualLines, i);
                Console.WriteLine("Most common bit is {0}", commonBit);

                actualLines = actualLines.Where(line => line[i] == commonBit).ToList();
                Console.WriteLine("Filtered {0} numbers", actualLines.Count);
            }

            if (actualLines.Count == 1)
                return Convert.ToInt32(actualLines[0], 2);
            else
                throw new ArgumentException($"After filtering {string.Join(",", actualLines)} numbers is left");
        }

        private int GetCO2ScrubberRating(string[] lines)
        {
            List<string> actualLines = new(lines);
            for (int i = 0; actualLines.Count > 1 && i < 12; i++)
            {
                Console.WriteLine("Considering bit position #{0}", i);
                char commonBit = GetLessCommonBit(actualLines, i);
                Console.WriteLine("Less common bit is {0}", commonBit);

                actualLines = actualLines.Where(line => line[i] == commonBit).ToList();
                Console.WriteLine("Filtered {0} numbers", actualLines.Count);
            }

            if (actualLines.Count == 1)
                return Convert.ToInt32(actualLines[0], 2);
            else
                throw new ArgumentException($"After filtering {string.Join(",", actualLines)} numbers is left");
        }

        private int GetGammaRate(int[,] summary)
        {
            string gammaRate = "";

            for (int i = 0; i < summary.GetLength(1); i++)
            {
                gammaRate += summary[1, i] >= summary[0, i] ? "1" : "0";
            }

            Console.WriteLine("Gamma rate is " + gammaRate);

            return Convert.ToInt32(gammaRate, 2);
        }

        private int GetEpsilonRate(int gamma)
        {
            int epsilonRate = (~gamma & 0x00000FFF);

            Console.WriteLine("Epsilon rate is " + Convert.ToString(epsilonRate, 2));

            return epsilonRate;
        }
    }
}
