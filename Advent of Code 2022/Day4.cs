using System.Text;
using System.Linq;

namespace Advent_of_Code
{
    internal class Day4 : ITaskSolver
    {
        public string GetTaskName() => "Day 4: Camp Cleanup";

        private string[]? buffer;
        private string pathToInputFile = @"input/task4.txt";

        public void SolvePartOne()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);
            var overlappingPairsCount = 0;

            foreach (var pair in buffer)
            {
                var boundaries = pair.Split(',','-').Select(x => int.Parse(x)).ToArray();
                if (CheckFullContain(boundaries)) overlappingPairsCount ++;
            }
            
            Console.WriteLine("Done");
            ConsoleExtensions.DisplayResult(overlappingPairsCount, buffer.Length);
        }

        public void SolvePartTwo()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);
            var overlappingPairsCount = 0;

            foreach (var pair in buffer)
            {
                var boundaries = pair.Split(',','-').Select(x => int.Parse(x)).ToArray();
                if (CheckOverlapping(boundaries)) overlappingPairsCount ++;
            }
            
            Console.WriteLine("Done");
            ConsoleExtensions.DisplayResult(overlappingPairsCount, buffer.Length);
        }

        private bool CheckFullContain(int[] boundaries)
        {
            var assignedSectionsFirst = Enumerable.Range(boundaries[0], boundaries[1] - boundaries[0] + 1);
            var assignedSectionsSecond = Enumerable.Range(boundaries[2], boundaries[3] - boundaries[2] + 1);

            if (assignedSectionsFirst.Count() >= assignedSectionsSecond.Count())
                return assignedSectionsFirst.First() <= assignedSectionsSecond.First() && assignedSectionsFirst.Last() >= assignedSectionsSecond.Last(); 
            
            return assignedSectionsSecond.First() <= assignedSectionsFirst.First() && assignedSectionsSecond.Last() >= assignedSectionsFirst.Last();
        }

        private bool CheckOverlapping(int[] boundaries)
        {
            var assignedSectionsFirst = Enumerable.Range(boundaries[0], boundaries[1] - boundaries[0] + 1);
            var assignedSectionsSecond = Enumerable.Range(boundaries[2], boundaries[3] - boundaries[2] + 1);

            return (assignedSectionsSecond.Contains(assignedSectionsFirst.First()) || assignedSectionsFirst.Contains(assignedSectionsSecond.First())); 
        }
    }
}
