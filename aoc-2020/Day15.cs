using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day15 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            int limit = 30000000;
            var nums = Input.AsIntArray(",");

            var idxs = Enumerable.Range(1, nums.Length)
                .Zip(nums)
                .ToDictionary(k => k.Second, v => v.First);

            int prev = -1;
            int curr = 0;
            for(int turn = nums.Length + 1; turn <= limit; turn++){

                if(idxs.TryGetValue(prev, out int prevValue))
                    curr = turn - 1 - prevValue;
                else
                    curr = 0;        

                idxs[prev] = turn -1;
                prev = curr;
            }

            return curr.ToString();
        }

        public override string SolveSecondPuzzle()
        {
            throw new NotImplementedException();
        }
    }
}
