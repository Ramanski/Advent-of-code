using System.Text;

namespace Advent_of_Code
{
    internal class Day6 : ITaskSolver
    {
        public string GetTaskName() => "Day 6: Lanternfish";

        private string[]? buffer;
        private string pathToInputFile = @"input/task6.txt";
        //private string pathToInputFile = @"input/task6-example.txt";

        private static readonly int ancestorSpawntime = 7;
        private static readonly int newfishSpawntime = 9;
        private static readonly int finishDay = 256;

        public void SolvePartOne()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);
            var totalFishes = 0L;
            var initialTimers = GroupInitialData(buffer[0]);

            // key is initialTimer, value is count of the same initialTimers
            foreach (var pair in initialTimers)
            {
                totalFishes += Spawn(pair.Key) * pair.Value;
            }

            Console.WriteLine("Done");
            ConsoleExtensions.DisplayResult(totalFishes.ToString(), initialTimers.Values.Sum());
        }

        public void SolvePartTwo()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);
            var totalFishes = 0L;
            var initialTimers = GroupInitialData(buffer[0]);

            // key is initialTimer, value is count of the same initialTimers
            foreach (var pair in initialTimers)
            {
                totalFishes += Spawn(pair.Key) * pair.Value;
            }

            Console.WriteLine("Done");
            ConsoleExtensions.DisplayResult(totalFishes.ToString(), initialTimers.Values.Sum());
        }

        private long Spawn(int spawnday)
        {
            long fishesCount = 1;
            while (spawnday < finishDay)
            {
                fishesCount += Spawn(spawnday + newfishSpawntime);
                spawnday += ancestorSpawntime;
            }

            return fishesCount;
        }

        private Dictionary<int, int> GroupInitialData(string data)
        {
            return data.Split(',')
                .Select(value => int.Parse(value))
                .GroupBy(value => value)
                .ToDictionary(key => key.Key, value => value.Count());
        }
    }
}
