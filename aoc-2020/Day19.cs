using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day19 : AdventPuzzle
    {
        private Dictionary<int, string> _rules;
        private List<string> _examples;
        public override void ParseInput()
        {
            var data = Input.AsStringArray();
            _rules = new Dictionary<int, string>();

            foreach(var line in data.TakeWhile(x => !string.IsNullOrEmpty(x)))
            {
                var splitLine = line.Split(':');
                _rules[int.Parse(splitLine[0])] = splitLine[1].Replace("\"","").Trim(); 
            }

            _examples = data.Skip(Array.IndexOf(data, "") + 1).ToList();
        }
        public override string SolveFirstPuzzle()
        {
            var rules = GenerateValid(0).ToHashSet();

            int count = 0;
            foreach(var example in _examples)
            {
                if(rules.Any(x => x == example))
                    count++;

            }

            return count.ToString();
        }

        private List<string> GenerateValid(int key)
        {
            var rule = _rules[key];
            if(rule.Length == 1)   
                return new List<string>{rule};

            var results = new List<string>();
            var orRule = rule.Split('|');
            foreach(var orr in orRule)
            {
                var tmpResults  = new List<string>(){""};
                var concatRule = orr.Trim().Split(' ');
                foreach(var ccr in concatRule)
                {
                    var tmpResultsLenght = tmpResults.Count();
                    for(int i = 0; i < tmpResultsLenght; i++)
                    {   
                        var current = tmpResults[i];
                        
                        foreach(var newString in GenerateValid(int.Parse(ccr))){
                            tmpResults.Add(current + newString);
                        }
                    } 
                    tmpResults.RemoveRange(0, tmpResultsLenght);
                }

                results.AddRange(tmpResults);

            }

            return results;
        }

        public override string SolveSecondPuzzle()
        {
            throw new NotImplementedException();
        }
    }
}
