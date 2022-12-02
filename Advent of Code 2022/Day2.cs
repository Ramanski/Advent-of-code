using System.Text;

namespace Advent_of_Code
{
    internal class Day2 : ITaskSolver
    {
        public string GetTaskName() => "Day 2: Rock Paper Scissors";

        private string[]? buffer;
        private string pathToInputFile = @"input/task2.txt";

        private enum Shape
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }

        private enum Result
        {
            Lose,
            Draw,
            Win
        }

        public void SolvePartOne()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);

            var result = 0;

            foreach(string currentLine in buffer)
            {
                var strategyGuide = currentLine.Split(' ');
                var opponentShape = strategyGuide[0] switch 
                {
                    "A" => Shape.Rock,
                    "B" => Shape.Paper,
                    "C" => Shape.Scissors,
                    _ => throw  new ArgumentException(strategyGuide[0])
                };

                var myShape = strategyGuide[1] switch 
                {
                    "X" => Shape.Rock,
                    "Y" => Shape.Paper,
                    "Z" => Shape.Scissors,
                    _ => throw  new ArgumentException(strategyGuide[1])
                };

                if ((int)myShape == 1 + ((int)opponentShape) % 3)
                {
                    result += 6;
                }
                else if (myShape == opponentShape)
                {
                    result += 3;
                }
                
                result += (int)myShape;
            }

            Console.WriteLine("Done");
            ConsoleExtensions.DisplayResult(result, buffer.Length);
        }

        public void SolvePartTwo()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);

            var points = 0;

            foreach(string currentLine in buffer)
            {
                var strategyGuide = currentLine.Split(' ');
                var opponentShape = strategyGuide[0] switch 
                {
                    "A" => Shape.Rock,
                    "B" => Shape.Paper,
                    "C" => Shape.Scissors,
                    _ => throw  new ArgumentException(strategyGuide[0])
                };

                var result = strategyGuide[1] switch 
                {
                    "X" => Result.Lose,
                    "Y" => Result.Draw,
                    "Z" => Result.Win,
                    _ => throw  new ArgumentException(strategyGuide[1])
                };

                points += result switch 
                {
                    Result.Lose => ((int)opponentShape + 1) % 3 + 1,
                    Result.Draw => 3 + (int)opponentShape,
                    Result.Win => 6 + ((int)opponentShape) % 3 + 1
                };
            }

            Console.WriteLine("Done");
            ConsoleExtensions.DisplayResult(points, buffer.Length);
        }
    }
}
