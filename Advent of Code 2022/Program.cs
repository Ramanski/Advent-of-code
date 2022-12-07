using Advent_of_Code;

Dictionary<int, ITaskSolver> tasks = new()
{
    { 1, new Day1() },
    { 2, new Day2() },
    { 3, new Day3() },
    { 4, new Day4() },
    { 6, new Day6() }
};

Console.WriteLine("Choose day\n");

foreach(var task in tasks)
{
    Console.WriteLine($"{task.Key}) {task.Value.GetTaskName()}");
}

if(int.TryParse(Console.ReadLine(), out int selectedTask) && tasks.TryGetValue(selectedTask, out var taskSolver))
{
    Console.WriteLine("What part to run ? (choose One/Two or 1/2 or I/II)");

    var selectedPart = Console.ReadLine()?.Trim() ?? string.Empty;

    try
    {
        if (selectedPart.Equals("One", StringComparison.OrdinalIgnoreCase) || selectedPart == "1" || selectedPart == "I")
            taskSolver.SolvePartOne();
        else if (selectedPart.Equals("Two", StringComparison.OrdinalIgnoreCase) || selectedPart == "2" || selectedPart == "II")
            taskSolver.SolvePartTwo();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }

    return;
}

Console.WriteLine("Inaccurate value provided");

