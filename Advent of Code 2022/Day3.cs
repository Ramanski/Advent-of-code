using System.Text;
using System.Linq;

namespace Advent_of_Code
{
    internal class Day3 : ITaskSolver
    {
        public string GetTaskName() => "Day 3: Rucksack Reorganization";

        private string[]? buffer;
        private string pathToInputFile = @"input/task3.txt";

        public void SolvePartOne()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);

            var items = new List<char>();

            foreach(var rucksack in buffer)
            {
                var firstCompartment = rucksack.Substring(0, rucksack.Length / 2);
                var secondCompartment = rucksack.Substring(rucksack.Length / 2);
                items.Add(firstCompartment.Intersect(secondCompartment).First());
            }

            var prioritiesSum = items.Sum(item => (int)item + 1 - (char.IsLower(item) ? (int)'a' : (int)'A' - 26));

            Console.WriteLine("Done");
            ConsoleExtensions.DisplayResult(prioritiesSum, buffer.Length);
        }

        public void SolvePartTwo()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);

            var items = new List<char>();
            var firstRucksackIndex = 0;
            var potentialItems = new List<char>();
            
            while(firstRucksackIndex + 3 <= buffer.Length)
            {
                var groupRucksack = buffer.Skip(firstRucksackIndex).Take(3).Select(x => x.ToCharArray()).ToList();

                var item = groupRucksack.Aggregate((prev, curr) => prev.Intersect(curr).ToArray()).Single();
                items.Add(item);
                firstRucksackIndex += 3;
            }

            var prioritiesSum = items.Sum(item => (int)item + 1 - (char.IsLower(item) ? (int)'a' : (int)'A' - 26));

            Console.WriteLine("Done");
            ConsoleExtensions.DisplayResult(prioritiesSum, buffer.Length);
        }
    }
}
