using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day18 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var expressions = Input.AsStringArray();
            Regex r = new Regex(@"\([0-9\+\* ]+\)");

            var results = new List<long>();
            foreach (var expression in expressions)
            {
                var expr = expression;

                while (!long.TryParse(expr, out _))
                {
                    long total = 0;
                    string operation = "+";

                    string innerExpression = r.Match(expr).Groups[0].Value;

                    if (string.IsNullOrEmpty(innerExpression))
                        innerExpression = expr;

                    foreach (var c in Regex.Matches(innerExpression, @"\d+|\*|\+").Select(x => x.Value))
                    {
                        if (int.TryParse(c, out int num))
                        {
                            if (operation == "+")
                                total += num;
                            else if (operation == "*")
                                total *= num;
                        }
                        else
                            operation = c;
                    }

                    expr = expr.Replace(innerExpression, total.ToString());
                }

                results.Add(long.Parse(expr));
            }

            return results.Sum().ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var expressions = Input.AsStringArray();
            Regex r = new Regex(@"\([0-9\+\* ]+\)");

            var results = new List<long>();
            foreach (var expression in expressions)
            {
                var expr = expression;

                while (!long.TryParse(expr, out _))
                {
                    string innerExpression = r.Match(expr).Groups[0].Value;
                
                    if (string.IsNullOrEmpty(innerExpression))
                        innerExpression = expr;

                     string exp2 = innerExpression;

                    foreach (var c in Regex.Matches(innerExpression, @"\d+( \+ \d+)+").Select(x => x.Value))
                    {   
                        var sum = Regex.Matches(c, @"\d+").Select(x => long.Parse(x.Value)).Sum();
                        innerExpression = innerExpression.Replace(c, sum.ToString());
                    }

                    var product = Regex.Matches(innerExpression, @"\d+")
                        .Select(x => long.Parse(x.Value))
                        .Aggregate((f, s) => f*s);

                    innerExpression = product.ToString();

                    expr = expr.Replace(exp2, innerExpression);
                }

                results.Add(long.Parse(expr));
            }

            return results.Sum().ToString();
        }
    }
}
