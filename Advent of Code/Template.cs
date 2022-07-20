using System.Text;

namespace Advent_of_Code
{
    internal class Day : ITaskSolver
    {
        public string GetTaskName() => "Day 2: Dive!";

        private string[]? buffer;
        private string pathToInputFile = @"input/task2.txt";

        public void SolvePartOne()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);

            Console.WriteLine("Done");
            ConsoleExtensions.DisplayResult(0,0);
        }

        public void SolvePartTwo()
        {
            if (buffer == null) buffer =  File.ReadAllLines(pathToInputFile, Encoding.UTF8);

            Console.WriteLine("Done\n");
            ConsoleExtensions.DisplayResult(0,0);
        }
    }
}
