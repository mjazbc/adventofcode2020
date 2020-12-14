using System;
using System.Linq;
using aoc_core;

namespace aoc_2020
{
    public class Day13 : AdventPuzzle
    {
        private int _timestamp;
        private string[] _busIds;
        public override void ParseInput()
        {
            var input = Input.AsStringArray();
            _timestamp = int.Parse(input.First());
            _busIds = input.Last().Split(',');
        }
        public override string SolveFirstPuzzle()
        {
           
            var parsedIds = _busIds.Where(x => x != "x")
                            .Select(x=> int.Parse(x));

            var waitTimes = parsedIds.Select(busId => new {busId,  waittime = ((_timestamp / busId) + 1) * busId - _timestamp } );
            var min = waitTimes.Min(x=>x.waittime);

            var minBus = waitTimes.Single(x => x.waittime == min);

            return (minBus.busId * minBus.waittime).ToString();
          
        }

        public override string SolveSecondPuzzle()
        {
            var parsedIds = _busIds.Select(x=> x == "x" ? -1 : int.Parse(x));
            var tDeltas = Enumerable.Range(0, parsedIds.Count())
                .Zip(parsedIds)
                .Where(x=>x.Second > -1)
                .ToList();
            
            long step = tDeltas.First().Second;
            long t = tDeltas.First().First;

            for(int i = 0; i < tDeltas.Count -1; i++)
            {          
                var current = tDeltas[i];
                var next = tDeltas[i + 1];

                while(true)
                {       
                    if((t + next.First) % next.Second == 0)
                        break;

                    t += step;
                }
                step *= next.Second;
            }

            return t.ToString();
        }
    }
}
