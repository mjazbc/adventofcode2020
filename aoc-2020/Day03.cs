using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day03 : AdventPuzzle
    {

        public override string SolveFirstPuzzle()
        {
            var map = Input.AsCharMatrix();
            return CountTrees(map, (1, 3)).ToString();   
        }

        public override string SolveSecondPuzzle()
        {
           var map = Input.AsCharMatrix();
           var slopes = new []{(1,1), (1,3), (1,5), (1,7), (2,1)};

            return slopes.Select(s => CountTrees(map, s))
                         .Aggregate(1L, (seed, item) => seed * item)
                         .ToString();
        }

        private int CountTrees(char[][] map, (int stepy, int stepx) slope)
        {
            int y = 0;
            int x = 0;

            int trees = 0;

            while(y < map.Length - slope.stepy)
            {
                y += slope.stepy;
                x = (x + slope.stepx) % map[0].Length;

                if(map[y][x] == '#')
                    trees++;
            }

            return trees;
        }
    }
}
