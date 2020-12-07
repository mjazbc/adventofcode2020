using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day07 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var rules = ParseInput(Input.AsStringArray());
            
            var bagsToCheck = new Queue<string>();
            var checkd = new HashSet<string>();
            bagsToCheck.Enqueue("shiny gold");

            while(bagsToCheck.Any())
            {
                var current = bagsToCheck.Dequeue();
                if(checkd.Contains(current))
                    continue;

                checkd.Add(current);
                var bagsList = rules.Where(r => r.Value.Any(v => v.Item1 == current)).Select(x => x.Key);
                foreach(var bag in bagsList)
                    bagsToCheck.Enqueue(bag);

            }
            
            return (checkd.Count() - 1).ToString();

        }

        public override string SolveSecondPuzzle()
        {
            var rules = ParseInput(Input.AsStringArray());
            var total = CountBags(rules, "shiny gold");

            return total.ToString();    
        }

        private int CountBags(Dictionary<string, List<(string, int)>> input, string current)
        {
            return input[current].Sum(bag => bag.Item2 + bag.Item2 * CountBags(input, bag.Item1));
        }


        private Dictionary<string, List<(string, int)>> ParseInput(string[] input)
        {
            var rulesRaw = input.Select(x => x.Replace(" bags contain ", ",")
                                                    .Replace(" bags", "")
                                                    .Replace(" bag", "")
                                                    .TrimEnd('.'));

            var rules = new Dictionary<string, List<(string, int)>>();
            foreach(var line in rulesRaw)
            {
                var splitLine = line.Split(',').Select(x=>x.Trim());
                var key = splitLine.First();
                rules.Add(key, new List<(string, int)>());
                foreach(var containingBags in splitLine.Skip(1))
                {
                    if(containingBags == "no other")
                        continue;

                    var r = Regex.Match(containingBags, @"(\d+) (.*)");
                    rules[key].Add((r.Groups[2].Value, int.Parse(r.Groups[1].Value)));
                }
            }

            return rules;
        }
    }

    

    
}
