using System.Text;

namespace Advent_of_Code
{
    internal class Day4 : ITaskSolver
    {
        public string GetTaskName() => "Day 4: Giant Squid";

        private string[]? buffer;
        private string pathToInputFile = @"input/task4.txt";
        private short marker = -1;

        public void SolvePartOne()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile);

            var(boards, numbers) = FillNumbersAndBoards();
            var positions = GetPositions(5, 5);
            short? finalNumber = null;
            List<short[]> winningBoards = new();

            foreach (var number in numbers.Take(4))
            {
                MarkNumbersOnBoards(ref boards,number);
            }

            foreach (var number in numbers.Skip(4))
            {
                MarkNumbersOnBoards(ref boards, number);
                winningBoards = CheckMarksOnBoards(ref boards, positions);
                if(winningBoards.Any())
                {
                    finalNumber = number;
                    break;
                }
            }

            Console.WriteLine("Done");

            if (finalNumber == null || !winningBoards.Any())
            {
                Console.WriteLine("There's no winning board!");
            }
            else
            {

                Console.WriteLine("Winning board:");
                ConsoleExtensions.DisplayFlattendMatrix(winningBoards[0], 5);
                var sum = winningBoards[0].Where(x => x != marker).Sum(t => t);
                ConsoleExtensions.DisplayResult((sum*finalNumber).Value,buffer.Length,sum, (int)finalNumber);
            }
        }

        public void SolvePartTwo()
        {
            if (buffer == null) buffer = File.ReadAllLines(pathToInputFile);

            var (boards, numbers) = FillNumbersAndBoards();
            var positions = GetPositions(5, 5);
            short? finalNumber = null;

            foreach (var number in numbers.Take(4))
            {
                MarkNumbersOnBoards(ref boards, number);
            }

            foreach (var number in numbers.Skip(4))
            {
                MarkNumbersOnBoards(ref boards, number);
                var winningBoards = CheckMarksOnBoards(ref boards, positions);
                if (winningBoards.Any())
                {
                    if (boards.Count > 1)
                    {
                        boards.RemoveAll(board => winningBoards.Contains(board));
                    }
                    else
                    {
                        finalNumber = number;
                        break;
                    }
                }
            }

            Console.WriteLine("Done");

            if (finalNumber == null || boards.Count != 1)
            {
                Console.WriteLine("There's no last winning board! {0} boards are left", boards.Count);
                foreach (var board in boards)
                {
                    ConsoleExtensions.DisplayFlattendMatrix(board, 5);
                }
            }
            else
            {

                Console.WriteLine("Last winning board:");
                ConsoleExtensions.DisplayFlattendMatrix(boards[0], 5);
                var sum = boards.First().Where(x => x != marker).Sum(t => t);
                ConsoleExtensions.DisplayResult((sum * finalNumber).Value, buffer.Length, sum, (int)finalNumber);
            }
        }

        private void MarkNumbersOnBoards(ref List<short[]> boards, short targetItem)
        {
            foreach(var board in boards)
            {
                for(int i = 0; i < board.Length; i++)
                {
                    if(board[i] == targetItem)
                        board[i] = marker;
                }
            }
        }

        private List<short[]> CheckMarksOnBoards(ref List<short[]> boards, List<int[]> positions)
        {
            List<short[]> winningBoards = new();
            foreach (var board in boards)
            {
                foreach(int[] position in positions)
                {
                    if(position.All(i => board[i] == marker))
                        winningBoards.Add(board);
                }
            }

            return winningBoards;
        }

        private List<int[]> GetPositions(int rowCount, int colCount)
        {
            List<int[]> positions = new List<int[]>();
            int[] allPositions = Enumerable.Range(0, rowCount * colCount).ToArray();
            
            // positions in row
            positions.AddRange(allPositions.Chunk(colCount));

            // positions in columns
            positions.AddRange(
                allPositions
                    .Take(colCount)
                    .SelectMany(i => allPositions.Where(j => (j + i) % colCount == 0))
                    .Chunk(rowCount));

            return positions;
        }

        private (List<short[]> boards, short[] numbers) FillNumbersAndBoards()
        {
            var numbers = Array.ConvertAll(buffer![0].Split(","), short.Parse);
            var boards = new List<short[]>();

            short[] board = new short[5*5];

            int row = 0;
            foreach (var line in buffer[2..])
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                int col = 0;
                foreach (string value in line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                {
                    board[row * 5 + col++] = short.Parse(value);
                }
                if (++row == 5)
                {
                    boards.Add(board);
                    row = 0;
                    board = new short[5*5];
                }
            }

            return (boards, numbers);
        }
    }
}
