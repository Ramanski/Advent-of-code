using System.Text;
using System.Linq;

namespace Advent_of_Code;

internal class Day8 : ITaskSolver
{
    public string GetTaskName() => "Day 8: Treetop Tree House";

    private string[]? buffer;
    private string pathToInputFile = @"input/task8.txt";

    public void SolvePartOne()
    {
        if (buffer == null) buffer = File.ReadAllLines(pathToInputFile, Encoding.UTF8);

        var treeGrid = buffer.Select(x => x.Select(x => byte.Parse(x.ToString())).ToArray()).ToArray();
        var isVisibleGrid = new bool[treeGrid[0].Length, treeGrid.Length];

        var columnLength = treeGrid[0].Length;
        var rowLength = treeGrid.Length;

        //mark visible all trees on the edges
        for(int i=0; i<columnLength; i++)
        {
            for(int j=0; j<rowLength; j++)
            {
                var isOnEdge = (i == 0 || i+1 == columnLength || j == 0 || j+1 == rowLength);
                isVisibleGrid.SetValue(isOnEdge, i, j);
            }
        }

        //check visibility from left
        for(int i=1; i<columnLength-1; i++)
        {
            var maxHeightSoFar = 0;
            for(int j=0; j<rowLength-1; j++)
            {
                var isVisibleSoFar = isVisibleGrid[i, j];
                isVisibleGrid.SetValue(isVisibleSoFar || (treeGrid[i][j] > maxHeightSoFar), i, j);
                maxHeightSoFar = Math.Max(maxHeightSoFar, treeGrid[i][j]);
            }
        }

        //check visibility from right
        for(int i=1; i<columnLength-1; i++)
        {
            var maxHeightSoFar = 0;
            for(int j=rowLength - 1; j>=0; j--)
            {
                var isVisibleSoFar = isVisibleGrid[i, j];
                isVisibleGrid.SetValue(isVisibleSoFar || (treeGrid[i][j] > maxHeightSoFar), i, j);
                maxHeightSoFar = Math.Max(maxHeightSoFar, treeGrid[i][j]);
            }
        }

        //check visibility from top
        for(int j=1; j<rowLength-1; j++)
        {
            var maxHeightSoFar = 0;
            for(int i=0; i<columnLength-1; i++)
            {
                var isVisibleSoFar = isVisibleGrid[i, j];
                isVisibleGrid.SetValue(isVisibleSoFar || (treeGrid[i][j] > maxHeightSoFar), i, j);
                maxHeightSoFar = Math.Max(maxHeightSoFar, treeGrid[i][j]);
            }
        }

        //check visibility from bottom
        for(int j=0; j<rowLength; j++)
        {
            var maxHeightSoFar = 0;
            for(int i=columnLength-1; i>0; i--)
            {
                var isVisibleSoFar = isVisibleGrid[i, j];
                isVisibleGrid.SetValue(isVisibleSoFar || (treeGrid[i][j] > maxHeightSoFar), i, j);
                maxHeightSoFar = Math.Max(maxHeightSoFar, treeGrid[i][j]);
            }
        }

        var totalTreesVisible = 0;
        foreach(var isVisible in isVisibleGrid)
        {
            if (isVisible) totalTreesVisible++;
        }

        Console.WriteLine("Done");
        ConsoleExtensions.DisplayResult(totalTreesVisible,buffer.Length);
    }

    public void SolvePartTwo()
    {
        if (buffer == null) buffer =  File.ReadAllLines(pathToInputFile, Encoding.UTF8);

        var treeGrid = buffer.Select(x => x.Select(x => byte.Parse(x.ToString())).ToArray()).ToArray();
        
        var columnLength = treeGrid[0].Length;
        var rowLength = treeGrid.Length;
        var maxScore = 0;

        for(int i=1; i<columnLength-1; i++)
        {
            for(int j=1; j<rowLength-1; j++)
            {
                var maxHeight = treeGrid[i][j];
                var score = 1;

                int targetPosition = j - 1;
                //look left
                while(targetPosition > 0 && treeGrid[i][targetPosition]<maxHeight)
                {
                    targetPosition--;
                }
                score *= j - targetPosition;

                targetPosition = j + 1;
                //look right
                while(targetPosition < rowLength-1 && treeGrid[i][targetPosition]<maxHeight)
                {
                    targetPosition++;
                }
                score *= targetPosition - j;

                targetPosition = i - 1;
                //look up
                while(targetPosition > 0 && treeGrid[targetPosition][j]<maxHeight)
                {
                    targetPosition--;
                }
                score *= i - targetPosition;

                targetPosition = i + 1;
                //look down
                while(targetPosition < columnLength-1 && treeGrid[targetPosition][j]<maxHeight)
                {
                    targetPosition++;
                }
                score *= targetPosition - i;

                maxScore = Math.Max(maxScore, score);
            }
        }

        Console.WriteLine("Done");
        ConsoleExtensions.DisplayResult(maxScore, buffer.Length);
    }
}
