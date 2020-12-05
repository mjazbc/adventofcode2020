using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day04 : AdventPuzzle
    {

        public override string SolveFirstPuzzle()
        {
            var creds = Input.AsCustomType<List<Dictionary<string, string>>>(ReadPassports);
            return creds.Count(p => p.Count == 8 || (p.Count == 7 && !p.Any(x=>x.Key == "cid" ))).ToString();         
        }

        public override string SolveSecondPuzzle()
        {
            var creds = Input.AsCustomType<List<Dictionary<string, string>>>(ReadPassports);
            return creds.Count(p => IsValid(p) == true).ToString();
        }

        private bool IsValid(Dictionary<string,string> passport)
        {
            var eyeColors = new string[]{"amb", "blu", "brn",  "gry", "grn", "hzl", "oth"};

            bool isValid = true;
            isValid &= passport.TryGetValue("byr", out string byr) && byr.Length == 4 && int.Parse(byr) >= 1920 && int.Parse(byr) <= 2002;
            isValid &= passport.TryGetValue("iyr", out string iyr) && iyr.Length == 4 && int.Parse(iyr) >= 2010 && int.Parse(iyr) <= 2020;
            isValid &= passport.TryGetValue("eyr", out string eyr) && eyr.Length == 4 && int.Parse(eyr) >= 2020 && int.Parse(eyr) <= 2030;
            isValid &= passport.TryGetValue("hgt", out string hgt);

            if(!isValid) return false;

            if(hgt.Contains("cm")){
                var cm = int.Parse(hgt.Replace("cm",""));
                isValid &= cm >= 150 && cm <= 193;
            }
            else if(hgt.Contains("in")){
                var inc = int.Parse(hgt.Replace("in",""));
                isValid &= inc >= 59 && inc <= 76;
            }
            else
                return false;
            
            isValid &= Regex.IsMatch(passport.GetValueOrDefault("hcl", " "), "^#[0-9a-f]{6}");
            isValid &= passport.TryGetValue("ecl", out string ecl) && eyeColors.Contains(ecl);
            isValid &= Regex.IsMatch(passport.GetValueOrDefault("pid", ""), @"^\d{9}$");

            return isValid;
        }

        private List<Dictionary<string, string>> ReadPassports(string input)
        {
            var data = input.Split(Environment.NewLine);
            var creds = new List<Dictionary<string, string>>();
            var currDict = new Dictionary<string, string>();

            foreach(var line in data)
            {   
                if(!string.IsNullOrEmpty(line))
                {
                    var splitLine = line.Split();
                    foreach(var keyvalue in splitLine)
                    {
                        var splitkv = keyvalue.Split(':');
                        currDict.Add(splitkv[0], splitkv[1]);
                    }
                }
                else
                {
                    creds.Add(currDict);
                    currDict = new Dictionary<string, string>();
                }
            }

            creds.Add(currDict);
            return creds;
        }
    }
}
