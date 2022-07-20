using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public static class ConsoleExtensions
    {
        public static void DisplayMatrix(this int[,] array)
        {
            Console.WriteLine("-------------------------------");

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write("|" + array[i, j]);
                }
                Console.WriteLine("|");
            }

            Console.WriteLine("-------------------------------");
        }

        public static void DisplayFlattendMatrix(this short[] array, int columns)
        {
            Console.WriteLine("-------------------------------");

            foreach(short[] row in array.Chunk(columns))
            {
                Console.Write("|");
                Console.Write(string.Join("|", row));
                Console.WriteLine("|");
            }

            Console.WriteLine("-------------------------------");
        }

        public static void DisplayResult(int result, int proceededLines, params int[] values)
        {
            int i = 1;

            Console.WriteLine("-------------------------------");
            Console.WriteLine("Result is: {0}", result);
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Statistic:");
            Console.WriteLine("Overall values read\t {0}", proceededLines);
            foreach(int value in values)
                Console.WriteLine($"Param #{i++} \t {value}");
        }

        public static void DisplayResult(string result, int proceededLines, params int[] values)
        {
            int i = 1;

            Console.WriteLine("-------------------------------");
            Console.WriteLine("Result is: {0}", result);
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Statistic:");
            Console.WriteLine("Overall values read\t {0}", proceededLines);
            foreach (int value in values)
                Console.WriteLine($"Param #{i++} \t {value}");
        }
    }
}
