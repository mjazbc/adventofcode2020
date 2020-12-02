using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day02 : AdventPuzzle
    {

        public override string SolveFirstPuzzle()
        {
            var policies = Input.AsCustomTypeEnumerable<(int pos1, int pos2, char c, string password)>(ParseInputFile);
    
            var count = policies.Select(policy => new {p = policy, count = policy.password.Count(pc=> policy.c == pc)})
                                .Count(policy => policy.count >= policy.p.pos1 && policy.count <= policy.p.pos2);


            return count.ToString();       
        }

        public override string SolveSecondPuzzle()
        {
            var policies = Input.AsCustomTypeEnumerable<(int pos1, int pos2, char c, string password)>(ParseInputFile);
            var count = policies.Count(policy => policy.password[policy.pos1-1] == policy.c ^ policy.password[policy.pos2-1] == policy.c);

            return count.ToString();
        }

        private (int pos1, int pos2, char c, string password) ParseInputFile(string inputText)
        {
            var match = Regex.Match(inputText, @"(\d+)-(\d+) (\w): (.*)");
            return (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), match.Groups[3].Value.Single(), match.Groups[4].Value);
        }
    }
}