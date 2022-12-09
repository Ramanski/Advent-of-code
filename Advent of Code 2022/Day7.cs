using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code;

internal class Day7 : ITaskSolver
{
    public string GetTaskName() => "Day 7: No Space Left On Device";

    private string[]? buffer;
    private string pathToInputFile = @"input/task7.txt";

    public void SolvePartOne()
    {
        if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);
        const int SIZE_LIMIT = 100_000;

        var rootDirectory = GetFullDirectory(buffer);

        var sum = 0;

        foreach(var directory in rootDirectory.GetAllDirectories())
        {
            Console.WriteLine($"Directory {directory.DirectoryName} size {directory.DirectorySize}");
            if (directory.DirectorySize > SIZE_LIMIT) continue;

            sum += directory.DirectorySize;
        }

        Console.WriteLine("Done");
        ConsoleExtensions.DisplayResult(sum, buffer.Length);
    }

    public void SolvePartTwo()
    {
        if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);
        const int FREE_SPACE_NEEDED = 30_000_000;
        const int FILESYSTEM_SPACE = 70_000_000;

        var rootDirectory = GetFullDirectory(buffer);

        var currentFreeSpace = FILESYSTEM_SPACE - rootDirectory.DirectorySize;
        var sizeToFreeUp = FREE_SPACE_NEEDED - currentFreeSpace;

        Console.WriteLine($"Size to free up is {sizeToFreeUp}");

        var directoryToDelete = rootDirectory.GetAllDirectories()
            .OrderBy(dir => dir.DirectorySize)
            .First(dir => dir.DirectorySize >= sizeToFreeUp);

        Console.WriteLine("Done");
        ConsoleExtensions.DisplayResult(directoryToDelete.DirectorySize, buffer.Length);
    }

    private Directory GetFullDirectory(string[] cmdLines)
    {
        var changeDirectoryCommandRegex = new Regex(@"\$ cd (?<targetDirectoryName>/|\.{2}|\S+)");
        var listItemsCommandRegex = new Regex(@"\$ ls");
        var directoryRepresentationRegex = new Regex(@"dir (?<directoryName>\S+)");
        var fileRepresentationRegex = new Regex(@"(?<fileSize>\d+) (?<fileName>\S+)");

        var rootDirectory = new Directory("/");
        var currentDirectory = rootDirectory;

        foreach(var cmdLine in cmdLines)
        {
            if (changeDirectoryCommandRegex.IsMatch(cmdLine))
            {
                var targetDirectoryName = changeDirectoryCommandRegex.Match(cmdLine).Groups["targetDirectoryName"].Value;
                currentDirectory = targetDirectoryName switch
                {
                    "/" => rootDirectory,
                    ".." => currentDirectory?.ParentDirectory,
                    _ => currentDirectory?.GetInnerDirectoryOrNull(targetDirectoryName)
                        ?? throw new System.Exception($"Directory doesn't exist or not discovered yet: {targetDirectoryName} in {currentDirectory?.DirectoryName}")
                };
                continue;
            }

            if (directoryRepresentationRegex.IsMatch(cmdLine))
            {
                var newDirectoryName = directoryRepresentationRegex.Match(cmdLine).Groups["directoryName"].Value;
                var isDirectoryAdded = currentDirectory?.TryAddInnerDirectory(newDirectoryName) ?? false;
                if (!isDirectoryAdded) Console.WriteLine("Warning: Directory {newDirectoryName} already exists in {currentDirectory?.DirectoryName}");
                continue;
            }

            if (fileRepresentationRegex.IsMatch(cmdLine))
            {
                var fileName = fileRepresentationRegex.Match(cmdLine).Groups["fileName"].Value;
                var fileSize = int.Parse(fileRepresentationRegex.Match(cmdLine).Groups["fileSize"].Value);
                var isFileAdded = currentDirectory?.TryAddFile(fileName, fileSize) ?? false;
                if (!isFileAdded) Console.WriteLine("Warning: File {fileName} already exists in {currentDirectory?.DirectoryName}");
                continue;
            }

            if (listItemsCommandRegex.IsMatch(cmdLine)) continue;
        }

        return rootDirectory;
    }
}

public class Directory: IEnumerable<Directory>
{
    Dictionary<string, int> Files { get; set; }
    List<Directory> InnerDirectories { get; set; }

    public Directory? ParentDirectory { get; private set; }

    public string DirectoryName { get; private set; }
    public int DirectorySize =>
        (Files?.Sum(file => file.Value) ?? 0) + InnerDirectories.Sum(dir => dir.DirectorySize);

    public Directory(string directoryName)
    {
        DirectoryName = directoryName;
        Files = new Dictionary<string, int>();
        InnerDirectories = new List<Directory>();
    }

    public bool TryAddInnerDirectory(string directoryName)
    {
        if (GetInnerDirectoryOrNull(directoryName) is not null) return false;

        var innerDirectory = new Directory(directoryName) { ParentDirectory = this };
        InnerDirectories.Add(innerDirectory);

        return true;
    }

    public bool TryAddFile(string fileName, int fileSize)
    {
        return Files.TryAdd(fileName, fileSize);
    }

    public Directory? GetInnerDirectoryOrNull(string directoryName)
    {
        return InnerDirectories.FirstOrDefault(dir => dir.DirectoryName == directoryName);
    }

    public IEnumerable<Directory> GetAllDirectories()
    {
        yield return this;

        foreach(var innerDirectory in InnerDirectories)
        {
            foreach(var directory in innerDirectory.GetAllDirectories())
            {
                yield return directory;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Directory> GetEnumerator()
    {
        foreach(var directory in GetAllDirectories())
        {
            yield return directory;
        }
    }
}
