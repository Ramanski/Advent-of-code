using System.Text;

namespace Advent_of_Code
{
    internal class Day1 : ITaskSolver
    {
        public string GetTaskName() => "Day 1: Sonar Sweep";

        private string[]? buffer;
        private string path = @"input/task1.txt";

        public void SolvePartOne()
        {
            if(buffer == null) buffer = File.ReadAllLines(path, Encoding.UTF8);

            int[] input = buffer.Select(number => int.Parse(number)).ToArray();
            int result = 0;

            for (int i = 3; i < input.Length; i++)
            {
                if (input[i] > input[i - 3])
                    result++;
            }

            Console.WriteLine("Done");
            Console.WriteLine("Result is: {0}", result);
        }

        public void SolvePartTwo()
        {
            if (buffer == null) buffer = File.ReadAllLines(path, Encoding.UTF8);

            int[] input = buffer.Select(number => int.Parse(number)).ToArray();
            int result = 0;

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] > input[i - 1])
                    result++;
            }

            Console.WriteLine("Done");
            Console.WriteLine("Result is: {0}", result);
        }
    }
}
