using System;
using aoc_core;

namespace aoc_2020
{
    public class Day01 : AdventPuzzle
    {

        public override string SolveFirstPuzzle()
        {
            var input = Input.AsInt();
            return (input * 2).ToString();

        }

        public override string SolveSecondPuzzle()
        {
            throw new NotImplementedException();
        }
    }
}
