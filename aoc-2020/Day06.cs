using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day06 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var groups = Input.AsStringArray(Environment.NewLine+Environment.NewLine)
                .Select(x => Regex.Replace(x, @"\s+", ""));

            var count = groups.Select(g => g.Distinct().Count()).Sum();
            return count.ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var groups = Input.AsStringArray(Environment.NewLine+Environment.NewLine)
                .Select(x => x.Split(Environment.NewLine));
            
            int sum = 0;
            foreach(var group in groups)
            {
                var hashsets = group.Select(g => g.ToHashSet<char>());
                var intersection = new HashSet<char>(hashsets.First());
                
                foreach(var hashset in hashsets.Skip(1))
                    intersection.IntersectWith(hashset);
                
                sum += intersection.Count();
            }
            return sum.ToString();   
        }
    }
}
