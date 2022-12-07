using System.Text;

namespace Advent_of_Code
{
    internal class Day6 : ITaskSolver
    {
        public string GetTaskName() => "Day 6: Tuning Trouble";

        private string[]? buffer;
        private string pathToInputFile = @"input/task6.txt";

        public void SolvePartOne()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);

            var chars = buffer[0].ToCharArray();

            int? result = null; 
            for (int i = 0; i < chars.Length - 3; i++)
            {
                var fourChars = buffer[0].Substring(i, 4).ToCharArray();
                var hashSet = new HashSet<char>(fourChars);
                
                if (hashSet.Count == 4)
                {
                    result = i+4;
                    break;
                }
            }

            Console.WriteLine("Done");
            ConsoleExtensions.DisplayResult(result ?? -1, chars.Length);
        }

        public void SolvePartTwo()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);

            var chars = buffer[0].ToCharArray();
            const int distinctCharsCount = 14;

            int? result = null; 
            for (int i = 0; i <= chars.Length - distinctCharsCount; i++)
            {
                var fourChars = buffer[0].Substring(i, distinctCharsCount).ToCharArray();
                var hashSet = new HashSet<char>(fourChars);
                
                if (hashSet.Count == distinctCharsCount)
                {
                    result = i + distinctCharsCount;
                    break;
                }
            }

            Console.WriteLine("Done");
            ConsoleExtensions.DisplayResult(result ?? -1, chars.Length);
        }
    }
}
