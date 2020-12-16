using System;
using System.IO;
using aoc_core;

namespace aoc_2020
{
    class Program
    {
        private const string inputPath = "./inputs/";
        static void Main(string[] args)
        {
            int day = 15;

            string dayName = $"Day{day:00}";
            string puzzleClassName = $"{typeof(Program).Namespace}.{dayName}";
            Type t = Type.GetType(puzzleClassName);

            AdventPuzzle puzzle = Activator.CreateInstance(t) as AdventPuzzle;
            puzzle.Input.LoadFromFile(Path.Combine(inputPath, $"{dayName}.txt"));
            puzzle.ParseInput();
            
            puzzle.Solve(Puzzle.Both);
        }
    }
}
