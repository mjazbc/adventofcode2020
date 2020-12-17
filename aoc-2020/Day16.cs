using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day16 : AdventPuzzle
    {
        private Dictionary<string, (int from, int to)[]> _rules = new Dictionary<string, (int from, int to)[]>();
        private int[] _ticket;
        List<int[]> _nearby = new List<int[]>();

        public override void ParseInput()
        {
            var input = Input.AsStringArray();
            var rulesRegex = new Regex(@"^([\w ]+): (\d+)-(\d+) or (\d+)-(\d+)");
           
            var section = Section.Rules;
            foreach(var line in input)
            {
                if(string.IsNullOrEmpty(line)){
                    section++;
                    continue;
                }
                
                if(line == "your ticket:" || line == "nearby tickets:")
                    continue;

                switch(section){
                    case Section.Rules:
                    {
                        var match = rulesRegex.Match(line);
                        _rules[match.Groups[1].Value] = new[]{(int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value)),
                            (int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value))};
                        break;
                    }
                    case Section.MyTicket:{
                        _ticket = line.Split(',').Select(x=> int.Parse(x)).ToArray();
                        break;
                    }
                    case Section.NearbyTickets:{
                        _nearby.Add(line.Split(',').Select(x=> int.Parse(x)).ToArray());
                        break;
                    }
                }
            }
        }
        public override string SolveFirstPuzzle()
        {
            int sum = 0;
            foreach(var ticket in _nearby)
            {
                foreach(var num in ticket)
                {
                    if(!IsValid(num))
                        sum += num;
                }
            }

            return sum.ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var valid = _nearby.Where(x=> x.All(x=>IsValid(x))).ToArray();
            var possibleRules = new Dictionary<int, HashSet<string>>();

            for(int i = 0; i< valid[0].Length; i++)
            {
                var column = valid.Select(x=>x[i]);
                possibleRules[i] = new HashSet<string>();

                foreach(var rule in _rules)
                {
                    bool validRule = true;
                    foreach(int num in column)
                        validRule &= ValidateRule(rule.Value, num);

                    if(validRule)
                        possibleRules[i].Add(rule.Key);
                }
            }

            while(true)
            {
                var singles = possibleRules.Where(x => x.Value.Count == 1).SelectMany(x=>x.Value).ToList();

                if(singles.Count() == possibleRules.Count())
                    break;

                foreach(var pr in possibleRules.Where(x => x.Value.Count > 1))
                    pr.Value.ExceptWith(singles);
                
            }

            long total = 1;
            foreach(var dep in possibleRules.Where(x => x.Value.Single().StartsWith("departure")))
                total *= _ticket[dep.Key];
            
            return total.ToString();          
        }

        private bool IsValid(int ticketNum)
        {
            foreach(var rule in _rules.Values)
            {
                if(ValidateRule(rule, ticketNum))
                    return true;
            }

            return false;           
        }

        private bool ValidateRule((int from, int to)[] rule, int ticketNum)
        {
            return (ticketNum >= rule[0].from && ticketNum <= rule[0].to) || (ticketNum >= rule[1].from && ticketNum <= rule[1].to);      
        }

        public enum Section{
            Rules,
            MyTicket,
            NearbyTickets
        }
    }
}
