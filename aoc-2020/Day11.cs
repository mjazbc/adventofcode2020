using System;
using System.Collections.Generic;
using aoc_core;

namespace aoc_2020
{
    public class Day11 : AdventPuzzle
    {
        private char[,] _layout;
        private (int y, int x)[] adjecant = { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

        public override void ParseInput() => _layout = Input.AsCharMatrix();
        
        public override string SolveFirstPuzzle() => IterateMoving(CountAdjecantSeats, occupiedLimit: 4).ToString();
        
        public override string SolveSecondPuzzle() => IterateMoving(CountVisibleSeats, occupiedLimit: 5).ToString();

        private bool MovePeople(char[,] newLayout, char[,] currentLayout, 
        Func<char[,], char[,], int, int, Dictionary<char, int>> CountFunction, int occupiedLimit)
        {
            bool changes = false;
            for (int y = 0; y < newLayout.GetLength(0); y++)
            {
                for (int x = 0; x < newLayout.GetLength(1); x++)
                {
                    char current = currentLayout[y, x];

                    var counts = CountFunction(newLayout, currentLayout, y, x);

                    if (current == 'L' && counts['#'] == 0)
                    {
                        newLayout[y, x] = '#';
                        changes = true;
                    }
                    else if (current == '#' && counts['#'] >= occupiedLimit)
                    {
                        newLayout[y, x] = 'L';
                        changes = true;
                    }
                }
            }
            return changes;
        }

        public string IterateMoving(Func<char[,], char[,], int, int, Dictionary<char, int>> CountFunction, int occupiedLimit)
        {
            bool changes = true;
            var currentLayout = new char[_layout.GetLength(0), _layout.GetLength(1)];
            Array.Copy(_layout, currentLayout, currentLayout.Length);

            while (changes)
            {
                var newLayout = new char[currentLayout.GetLength(0), currentLayout.GetLength(1)];

                Array.Copy(currentLayout, newLayout, newLayout.Length);

                changes = MovePeople(newLayout, currentLayout, CountFunction, occupiedLimit);

                currentLayout = newLayout;

            }

            int occupied = 0;
            for (int y = 0; y < currentLayout.GetLength(0); y++)
                for (int x = 0; x < currentLayout.GetLength(1); x++)
                    if (currentLayout[y, x] == '#')
                        occupied++;

            return occupied.ToString();
        }

        private Dictionary<char, int> CountAdjecantSeats(char[,] newLayout, char[,] currentLayout, int y, int x)
        {
            var counts = InitCountsDict();

            foreach (var (ady, adx) in adjecant)
            {
                if (y + ady < 0 || y + ady >= newLayout.GetLength(0) || x + adx < 0 || x + adx >= newLayout.GetLength(1))
                    continue;

                char adj = currentLayout[y + ady, x + adx];
                counts[adj]++;
            }
            return counts;
        }

        private Dictionary<char, int> InitCountsDict() =>  new Dictionary<char, int>{
                        {'.', 0},
                        {'#', 0},
                        {'L', 0}
                    };

        private Dictionary<char, int> CountVisibleSeats(char[,] newLayout,char[,] currentLayout, int y, int x)
        {
            var counts = InitCountsDict();

            foreach (var (ady, adx) in adjecant)
            {
                var sight = (ady, adx);
                while (true)
                {
                    if (y + sight.ady < 0 || y + sight.ady >= newLayout.GetLength(0)
                        || x + sight.adx < 0 || x + sight.adx >= newLayout.GetLength(1))
                        break;

                    char adj = currentLayout[y + sight.ady, x + sight.adx];

                    if (adj != '.')
                    {
                        counts[adj]++;
                        break;
                    }

                    sight.ady += ady;
                    sight.adx += adx;
                }
            }
            return counts;
        }
    }
}
