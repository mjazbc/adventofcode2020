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
            var rules42 = GenerateValid(42, 0).ToHashSet();
            var rules31 = GenerateValid(31, 0).ToHashSet();

            int count = 0;
            foreach(var example in _examples)
            {
                var chunks = ToChunks(example, 8).ToArray();
                
                if(chunks.Length == 3 && 
                    rules42.Contains(chunks[0]) && rules42.Contains(chunks[1]) && rules31.Contains(chunks[2]))
                    count++;

            }

            return count.ToString();
        }

        private IEnumerable<string> ToChunks(string text, int chunkSize)
        {
            int start = 0;
            while(start + chunkSize < text.Length)
            {
                yield return text.Substring(start, chunkSize);
                start += chunkSize;
            }
            
            yield return text.Substring(start);
        }

        private List<string> GenerateValid(int key, int loopsize)
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
                    var intCcr = int.Parse(ccr);
                    if(intCcr == key){
                        // Console.WriteLine(intCcr);
                        loopsize++;

                        if(loopsize == 1){
                            loopsize = 0;
                            break;
                        }
                    }

                    var tmpResultsLenght = tmpResults.Count();
                    for(int i = 0; i < tmpResultsLenght; i++)
                    {   
                        var current = tmpResults[i];
                        
                        foreach(var newString in GenerateValid(intCcr,loopsize)){
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
            var rules42 = GenerateValid(42, 0).ToHashSet();
            var rules31 = GenerateValid(31, 0).ToHashSet();

            int count = 0;
            foreach(var example in _examples)
            {
                if(example.Length % 8 > 0)
                    continue;

                var chunks = ToChunks(example, 8).Reverse().ToArray();
                
                int count31, count42, i;
                count31 = count42 = i = 0;
     
                while(i < chunks.Length && rules31.Contains(chunks[i]))
                {
                    i++;
                    count31++;
                }

                while(i < chunks.Length && rules42.Contains(chunks[i]))
                {
                    i++;
                    count42++;
                }

                if(i == chunks.Length && count31 > 0 && count42 > count31)
                    count++;

            }

            return count.ToString();
        }
    }
}
