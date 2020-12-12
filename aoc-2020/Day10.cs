using System;
using System.Collections.Generic;
using System.Linq;
using aoc_core;

namespace aoc_2020
{
    public class Day10 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var array = Input.AsIntArray().ToList();
            int joltage = array.Max() + 3;

            array.Add(0);
            array.Add(joltage);

            var orderedArray = array.OrderBy(x => x);

            var diffs = orderedArray.Zip(orderedArray.Skip(1), (first, second) => second - first);

            return (diffs.Count(x => x == 1) * diffs.Count(x => x == 3)).ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var inputList = Input.AsIntArray().OrderBy(x => x).ToList();       
            int joltage = inputList.Max() + 3;

            inputList.Insert(0, 0);
            inputList.Add(joltage);

            var array = inputList.ToArray();

            var total = new List<long>(){
                1,1
            };
            for(int i = 2; i < array.Count(); i++)
            {
                int current = array[i];
                int start = i-3 >= 0 ? i-3 : 0;
                var sub = array[start..i];
                
                int count = sub.Count(x=> current - 3 <= x );
                total.Add(total.TakeLast(count).Sum());

            }

            return total.Last().ToString();
        }


    }
}
