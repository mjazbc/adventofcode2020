using System;
using System.Collections.Generic;
using System.Linq;
using aoc_core;

namespace aoc_2020
{
    public class Day09 : AdventPuzzle
    {
        private long[] _data;
        private long? firstInvalid;
        public override void ParseInput()
        {
            _data = Input.AsLongArray();
        }
        public override string SolveFirstPuzzle()
        {
            firstInvalid = GetFirstInvalid(_data);
            return firstInvalid.ToString();
        }

        public override string SolveSecondPuzzle()
        {
            
            if(!firstInvalid.HasValue)
                firstInvalid = GetFirstInvalid(_data);

            int windowStart = 0;
            int windowSize = 2;

            while(true)
            {
                var window = _data[windowStart..(windowStart+windowSize)];
                var sum = window.Sum();

                if(sum == firstInvalid)
                    return (window.Min() + window.Max()).ToString();
                
                windowSize++;

                if(sum > firstInvalid || windowStart + windowSize >= _data.Length){
                    
                    windowStart++;
                    windowSize = 2;
                }
            }
        }

        private long GetFirstInvalid(long[] data)
        {
            int windowSize = 25;
    
            int windowStart = 0;
            
            foreach(var number in data.Skip(windowSize))
            {
                var window = data[windowStart..(windowStart+windowSize)];
                var comb = GetPermutations(window).Select(x => x.Sum());

                if(!comb.Contains(number))
                    return number;

                windowStart++;
            }

            throw new NotFiniteNumberException();
        }

        static IEnumerable<IEnumerable<long>> GetPermutations(IEnumerable<long> list)
        {

          return list.Select(t => new long[] { t })
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new long[] { t2 }));
        }
    }
}
