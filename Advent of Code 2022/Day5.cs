using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code;

internal class Day5 : ITaskSolver
{
    public string GetTaskName() => "Day 5: Supply Stacks";

    private string[]? buffer;
    private string pathToInputFile = @"input/task5.txt";

    public void SolvePartOne()
    {
        if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);

        var cratesOnStacks = GetCratesOnStacks(buffer);
        var moves = GetMoves(buffer) ?? throw new ArgumentException();

        foreach(var move in moves)
        {
            for (int i = 0; i < move.Quantity; i++)
            {
                var movingItem = cratesOnStacks.GetValueOrDefault(move.TargetStack)?.Pop()
                    ?? throw new ArgumentException();
                cratesOnStacks.GetValueOrDefault(move.DestinationStack)?.Push(movingItem);
            }
        }

        var result = string.Empty;

        foreach(var cratesOnStack in cratesOnStacks)
        {
            result += cratesOnStack.Value.Peek();
        }

        Console.WriteLine("Done");
        ConsoleExtensions.DisplayResult(result, buffer.Length, cratesOnStacks.Count, moves?.Count() ?? 0);
    }

    public void SolvePartTwo()
    {
        if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);

        var cratesOnStacks = GetCratesOnStacks(buffer);
        var moves = GetMoves(buffer) ?? throw new ArgumentException();

        foreach(var move in moves)
        {
            List<char> tempStack = new();
            for (int i = 0; i < move.Quantity; i++)
            {
                var movingItem = cratesOnStacks.GetValueOrDefault(move.TargetStack)?.Pop()
                    ?? throw new ArgumentException();
                tempStack.Add(movingItem);
            }
            tempStack.Reverse();
            tempStack.ForEach(item => cratesOnStacks.GetValueOrDefault(move.DestinationStack)?.Push(item));
        }

        var result = string.Empty;

        foreach(var cratesOnStack in cratesOnStacks)
        {
            result += cratesOnStack.Value.Peek();
        }

        Console.WriteLine("Done");
        ConsoleExtensions.DisplayResult(result, buffer.Length, cratesOnStacks.Count, moves?.Count() ?? 0);
    }

    private Dictionary<int, Stack<char>> GetCratesOnStacks(string[] rawInput)
    {
        var cratesMatrix = rawInput
            .TakeWhile(x => !string.IsNullOrEmpty(x))
            .Reverse()
            .Select(line => line.ToCharArray().Where((val, index) => index % 4 == 1).ToList())
            .ToList();

        var stacksLastNum = int.Parse(cratesMatrix.First().Last().ToString());

        var cratesOnStacks = new Dictionary<int, Stack<char>>(Enumerable.Range(1, stacksLastNum)
            .Select(i => new KeyValuePair<int, Stack<char>>(i, new Stack<char>())));

        cratesMatrix.ForEach(crateLine =>
            crateLine
            .Select((val, i) => new { Number = i + 1, Crate = val })
            .Where(item => char.IsLetter(item.Crate))
            .ToList()
            .ForEach(item => cratesOnStacks.GetValueOrDefault(item.Number)?.Push(item.Crate)));

        return cratesOnStacks;
    }

    private IEnumerable<Move>? GetMoves(string[] rawInput)
    {
        var regex = new Regex(@"move (?<quantity>\d+) from (?<targetStack>\d+) to (?<destinationStack>\d+)");

        return rawInput.SkipWhile(x => !string.IsNullOrEmpty(x))
            .Skip(1)
            .Select(x => regex.Matches(x).Select(x =>
                new Move {
                    Quantity = int.Parse(x.Groups["quantity"].Value),
                    TargetStack = int.Parse(x.Groups["targetStack"].Value),
                    DestinationStack = int.Parse(x.Groups["destinationStack"].Value)
                }).First());
    }

    struct Move
    {
        public int Quantity { get; set; }
        public int TargetStack { get; set; }
        public int DestinationStack { get; set; }
    }
}
