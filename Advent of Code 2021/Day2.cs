using System.Text;

namespace Advent_of_Code
{
    internal class Day2 : ITaskSolver
    {
        public string GetTaskName() => "Day 2: Dive!";

        private string[]? buffer;
        private string pathToInputFile = @"input/task2.txt";

        public void SolvePartOne()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);

            int depth = 0;
            int length = 0;

            foreach (string row in buffer)
            {
                var param = row.Split(' ');
                var value = int.Parse(param[1]);

                switch (param[0])
                {
                    case "forward":
                        length += value;
                        break;
                    case "down":
                        depth += value;
                        break;
                    case "up":
                        depth -= value;
                        break;
                    default:
                        Console.WriteLine("Unrecognized parameter {0}", param[0]);
                        break;
                }
            }

            Console.WriteLine("Done");
            DisplayResult(depth, length);
        }

        public void SolvePartTwo()
        {
            if (buffer == null) buffer =  File.ReadAllLines(pathToInputFile, Encoding.UTF8);

            int depth = 0;
            int length = 0;
            int aim = 0;

            foreach (string row in buffer)
            {
                var param = row.Split(' ');
                var value = int.Parse(param[1]);

                switch (param[0])
                {
                    case "forward":
                        length += value;
                        depth += aim * value;
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                    default:
                        Console.WriteLine("Unrecognized parameter {0}", param[0]);
                        break;
                }
            }

            Console.WriteLine("Done");
            DisplayResult(depth, length, aim);
        }

        private void DisplayResult(int depth, int length, int? aim = null)
        {
            Console.WriteLine("Result is: {0}", depth * length);
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Statistic:");
            Console.WriteLine("Overall rows \t {0}", buffer!.Length);
            Console.WriteLine("Length \t {0}", length);
            Console.WriteLine("Depth \t {0}", depth);

            if(aim.HasValue)
                Console.WriteLine("Aim \t {0}", aim);
        }
    }
}
