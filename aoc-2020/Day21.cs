using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day21 : AdventPuzzle
    {
        private List<(string[] ingredients, string[] alergens)> _foodList;
        public override void ParseInput()
        {
            var lines = Input.AsStringArray();
            Regex r = new Regex(@"((\w+ )+)\(contains ([\w, ]+)\)");

            _foodList = new List<(string[] ingredients, string[] alergens)>();
            foreach (var line in lines)
            {
                var matches = r.Match(line);
                var ingredients = matches.Groups[1].Value.Trim().Split();
                var alergens = matches.Groups[3].Value.Split(", ");

                _foodList.Add((ingredients, alergens));
            }
        }

        public Dictionary<string, HashSet<string>> FindIngredients()
        {
            var allergens = _foodList.SelectMany(x=>x.alergens).ToHashSet();
            var usedIngredients = new Dictionary<string, HashSet<string>>();
            foreach(var allergen in allergens)
            {
                var foods = _foodList.Where(x=>x.alergens.Contains(allergen)).Select(x=>x.ingredients.ToHashSet());

                var common = foods.First();
                foreach(var food in foods.Skip(1))
                    common.IntersectWith(food);

                usedIngredients[allergen] = common;
            }

            return usedIngredients;
        }

        public override string SolveFirstPuzzle()
        {         
            var usedIngredients = FindIngredients();

            int counts = 0;
            foreach (var food in _foodList)
                counts += food.ingredients.Except(usedIngredients.SelectMany(x=>x.Value).ToHashSet()).Count();

            return counts.ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var usedIngredients = FindIngredients();

            while(usedIngredients.Values.Any(x=>x.Count > 1))
            {
                var singles = usedIngredients.Where(x => x.Value.Count == 1).SelectMany(x=>x.Value).ToList();
                foreach(var u in usedIngredients.Where(x=> x.Value.Count > 1).Select(x=>x.Key))
                {
                    usedIngredients[u].ExceptWith(singles);
                }
            }

            return string.Join(",",usedIngredients.OrderBy(x=>x.Key).Select(x=>x.Value.Single()));  
        }
    }
}
