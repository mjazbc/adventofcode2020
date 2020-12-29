using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day24 : AdventPuzzle
    {
        private Dictionary<(double, double), bool> _tileColors;
        private Regex r = new Regex("se|sw|ne|nw|e|w");
        public override string SolveFirstPuzzle()
        {
            var tileList = Input.AsCustomTypeEnumerable(ParseTileList);
            _tileColors = new Dictionary<(double, double), bool>();
            foreach(var path in tileList)
            {
                var pos = GetPositions(path);
                _tileColors[pos] = _tileColors.GetValueOrDefault(pos, false) ^ true;
            }

            return _tileColors.Values.Count(x=>x).ToString();
        }

        public (double, double) GetPositions(IEnumerable<string> path){
            var reference = (0.0, 0.0d);
            foreach(var dir in path)
            {
                switch(dir){
                    case "se": reference = (reference.Item1 - 0.5, reference.Item2 + 0.5); break;
                    case "sw": reference = (reference.Item1 - 0.5, reference.Item2 - 0.5); break;
                    case "ne": reference = (reference.Item1 + 0.5, reference.Item2 + 0.5); break;
                    case "nw": reference = (reference.Item1 + 0.5, reference.Item2 - 0.5); break;
                    case "w":  reference = (reference.Item1, reference.Item2 - 1); break;
                    case "e":  reference = (reference.Item1, reference.Item2 + 1); break;
                    default: throw new Exception("unknown direction");
                }
            }

            return reference;
        }

        public override string SolveSecondPuzzle()
        {
            if(_tileColors == null)
                SolveFirstPuzzle();
            
            var tileColors = new Dictionary<(double,double), bool>(_tileColors.Where(x=>x.Value));
            _tileColors.Clear();

            var adjCoords = new[]{(0, 1), (0, -1), (-0.5, 0.5), (0.5, -0.5), (0.5, 0.5), (-0.5, -0.5)};
            
            int days = 100;
            for(int day = 0; day < days; day++)
            {
                var newColors = new Dictionary<(double,double), bool>();

                double maxY = tileColors.Max(x=>x.Key.Item1);
                double maxX = tileColors.Max(x=>x.Key.Item2);
                double minY = tileColors.Min(x=>x.Key.Item1);
                double minX = tileColors.Min(x=>x.Key.Item2);

                for(double y = minY -0.5; y <= maxY + 0.5; y += 0.5)
                {
                    for(double x = minX -0.5 ; x <= maxX + 0.5; x += 0.5)
                    {
                        if((y - x) % 1 != 0)
                            continue;
                        
                        var currBlack = tileColors.GetValueOrDefault((y,x), false);

                        var adjBlack = adjCoords.Count(a => tileColors.ContainsKey((y + a.Item1, x + a.Item2)));

                        if(currBlack && (adjBlack == 0 || adjBlack > 2))
                            continue;                         
                        else if(currBlack || (!currBlack && adjBlack == 2))
                            newColors[(y,x)] = true;
                    }
                }

                // Console.WriteLine($"Day {day +1}: " + newColors.Count());
                tileColors = new Dictionary<(double, double), bool>(newColors);              
            }

            return tileColors.Count().ToString();
        }

        private IEnumerable<string> ParseTileList(string line) => r.Matches(line)
            .SelectMany(x=>x.Groups.Values)
            .Select(x=>x.Value);
        
    }
}