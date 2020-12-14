using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day14 : AdventPuzzle
    {
        private Regex r = new Regex(@"^mask = (?<mask>[X10]{36})$|^mem\[(?<mem>\d+)\] = (?<memval>\d+)");
        public override string SolveFirstPuzzle()
        {
            var input = Input.AsStringArray();
            long setmask = 0;
            long deletemask = 0;

            var mem = new Dictionary<long, long>();
            foreach (var line in input)
            {
                var m = r.Match(line);
                if (m.Groups["mask"].Success)
                {

                    setmask = Convert.ToInt64(m.Groups["mask"].Value.Replace("X", "0"), 2);
                    deletemask = Convert.ToInt64(m.Groups["mask"].Value.Replace("X", "1"), 2);

                    continue;
                }

                long memAddr = long.Parse(m.Groups["mem"].Value);
                long val = long.Parse(m.Groups["memval"].Value);

                var masked = (val & deletemask) | setmask;
                mem[memAddr] = masked;
            }

            return mem.Sum(x => x.Value).ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var input = Input.AsStringArray();
            long setmask = 0;
            long deletemask = 0;
            long[] floating = new long[36];

            var mem = new Dictionary<long, long>();
            foreach (var line in input)
            {
                var m = r.Match(line);
                if (m.Groups["mask"].Success)
                {

                    var mask = m.Groups["mask"].Value;
                    setmask = Convert.ToInt64(mask.Replace("X", "0"), 2);
                    deletemask = Convert.ToInt64(mask.Replace("0", "1").Replace("X", "0"), 2);

                    floating = Enumerable.Range(0, mask.Length)
                        .Reverse()
                        .Zip(mask.ToCharArray())
                        .Where(x => x.Second == 'X')
                        .Select(x => (long)Math.Pow(2, x.First))
                        .ToArray();

                    continue;
                }

                long memAddr = long.Parse(m.Groups["mem"].Value);
                long val = long.Parse(m.Groups["memval"].Value);

                var masked = (memAddr & deletemask) | setmask;

                foreach(var comb in Permutations<long>(floating)){
                    
                    var memAddrFloating = masked | comb.Sum();
                    mem[memAddrFloating] = val;

                }
               
            }

            return mem.Sum(x => x.Value).ToString();
        }

        public static IEnumerable<T[]> Permutations<T>(IEnumerable<T> source)
        {
            if (null == source)
                throw new ArgumentNullException(nameof(source));

            T[] data = source.ToArray();

            return Enumerable
              .Range(0, 1 << (data.Length))
              .Select(index => data
                 .Where((v, i) => (index & (1 << i)) != 0)
                 .ToArray());
        }
    }
}
