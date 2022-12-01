using System.Text;

namespace Advent_of_Code
{
    internal class Day1 : ITaskSolver
    {
        public string GetTaskName() => "Day 1: Calorie Counting";

        private string[]? buffer;
        private string pathToInputFile = @"input/task1.txt";

        public void SolvePartOne()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);

            var maximum = 0;
            var currentSum = 0;

            foreach(string currentLine in buffer)
            {
                if (!string.IsNullOrEmpty(currentLine))
                {
                    currentSum += int.Parse(currentLine);
                    continue;
                }

                maximum = Math.Max(currentSum, maximum);
                currentSum = 0;
            }

            maximum = Math.Max(currentSum, maximum);

            Console.WriteLine("Done");
            ConsoleExtensions.DisplayResult(maximum, buffer.Length);
        }

        public void SolvePartTwo()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);
            int topSumCount = 3;

            var sums = new SortedSet<int>();
            var currentSum = 0;

            foreach(string currentLine in buffer)
            {
                if (!string.IsNullOrEmpty(currentLine))
                {
                    currentSum += int.Parse(currentLine);                   
                    continue;
                }
                
                sums.Add(currentSum);
                currentSum = 0;
            }

            sums.Add(currentSum);

            var result = sums.Reverse().Take(topSumCount).Sum();

            Console.WriteLine("Done");
            ConsoleExtensions.DisplayResult(result, buffer.Length);
        }
    }
}
