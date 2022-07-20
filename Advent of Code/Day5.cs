using System.Text;

namespace Advent_of_Code
{
    internal class Day5 : ITaskSolver
    {
        public string GetTaskName() => "Day 5: Hydrothermal Venture";

        private string[]? buffer;
        private string pathToInputFile = @"input/task5.txt";
        //private string pathToInputFile = @"input/task5-example.txt";

        public void SolvePartOne()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);
            int[,] field = new int[1000, 1000];

            // x1 = [0], y1 = [1]
            // x2 = [2], y2 = [3]
            foreach (int[] coordinates in SeparateCoordinates(buffer))
            {
                var deltaX = coordinates[0] - coordinates[2];
                var deltaY = coordinates[1] - coordinates[3];

                if (deltaX == 0)
                {
                    int maxValue = Math.Max(coordinates[1], coordinates[3]);
                    int minValue = Math.Min(coordinates[1], coordinates[3]);
                    while (minValue <= maxValue)
                    {
                        field[minValue++, coordinates[0]]++;
                    }
                }
                else if (deltaY == 0)
                {
                    int maxValue = Math.Max(coordinates[0], coordinates[2]);
                    int minValue = Math.Min(coordinates[0], coordinates[2]);
                    while (minValue <= maxValue)
                    {
                        field[coordinates[1], minValue++]++;
                    }
                }
            }
            int result = 0;

            foreach(var value in field)
            {
                if (value >= 2)
                    result++;
            }

            Console.WriteLine("Done");
            //ConsoleExtensions.DisplayMatrix(field);

            ConsoleExtensions.DisplayResult(result,buffer.Length);
        }

        public void SolvePartTwo()
        {
            if (buffer == null) buffer =  File.ReadAllLines(pathToInputFile, Encoding.UTF8);

            int[,] field = new int[1000, 1000];

            // x1 = [0], y1 = [1]
            // x2 = [2], y2 = [3]
            foreach (int[] coordinates in SeparateCoordinates(buffer))
            {
                var deltaX = coordinates[0] - coordinates[2];
                var deltaY = coordinates[1] - coordinates[3];

                if (deltaX == 0)
                {
                    int maxValue = Math.Max(coordinates[1], coordinates[3]);
                    int minValue = Math.Min(coordinates[1], coordinates[3]);
                    while (minValue <= maxValue)
                    {
                        field[minValue++, coordinates[0]]++;
                    }
                }
                else if (deltaY == 0)
                {
                    int maxValue = Math.Max(coordinates[0], coordinates[2]);
                    int minValue = Math.Min(coordinates[0], coordinates[2]);
                    while (minValue <= maxValue)
                    {
                        field[coordinates[1], minValue++]++;
                    }
                }
                else if(Math.Abs(deltaX) == Math.Abs(deltaY))
                {
                    var incrementX = deltaX < 0 ? 1 : -1;
                    var incrementY = deltaY < 0 ? 1 : -1;

                    do
                    {
                        field[coordinates[1], coordinates[0]]++;
                        coordinates[0] += incrementX;
                        coordinates[1] += incrementY;
                    }
                    while (coordinates[0] != coordinates[2] &&
                           coordinates[1] != coordinates[3]);
                    
                    field[coordinates[1], coordinates[0]]++;
                }
            }
            int result = 0;

            foreach (var value in field)
            {
                if (value >= 2)
                    result++;
            }

            Console.WriteLine("Done");
            //ConsoleExtensions.DisplayMatrix(field);

            ConsoleExtensions.DisplayResult(result, buffer.Length);
        }

        private IEnumerable<int[]> SeparateCoordinates(string[] buffer)
        {
            foreach(var line in buffer)
            {
                yield return Array.ConvertAll(line.Replace(" -> ", ",").Split(','), int.Parse);
            }
        }
    }
}
