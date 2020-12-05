using System;

using System.Linq;
using aoc_core;

namespace aoc_2020
{
    public class Day05 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var boardingPasses = Input.AsStringArray();
            return boardingPasses.Max(GetBoardingPassSeatId).ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var boardingPasses = Input.AsStringArray();
            var ids = boardingPasses.Select(GetBoardingPassSeatId).OrderBy(x => x);
            var diff = ids.Zip(ids.Skip(1)).Single(x => x.Second - x.First > 1);

            return (diff.First + 1).ToString();
        }

        public int GetBoardingPassSeatId(string boardingPass)
        {
            var rowSpecifier = boardingPass[..7].ToCharArray();
            var columnSpecifier = boardingPass[^3..].ToCharArray();
                
            int row = GetNumber('B', rowSpecifier);
            int column = GetNumber('R', columnSpecifier);

            return CalcSeatId(row, column);
        }

        public int CalcSeatId(int row, int column) => row * 8 + column;
        public int GetNumber(char bitIdentifier, char[] bitmap)
        {   
            int number = 0;
            for(int i = 0; i < bitmap.Length; i++)
            {
                if(bitmap[i] == bitIdentifier)
                    number += (int)Math.Pow(2.0, bitmap.Length - i -1);
            }

            return number;
        }

       
    }
}
