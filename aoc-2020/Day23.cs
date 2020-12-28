using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using aoc_core;

namespace aoc_2020
{
    public class Day23 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var cupsList = Input.AsString().ToCharArray().Select(x => int.Parse(x+""));
            var cups = new LinkedList<int>(cupsList);

            var current = cups.First;

            for(int i = 0; i < 100; i++)
            {       
                // Console.WriteLine(i);
                var nextThree = new List<int>();
                for(int j = 0; j < 3; j++)
                {
                    var n = current.NextOrWrapAround();
                    nextThree.Add(n.Value);
                    cups.Remove(n);
                }

                var destination = current.Value == 1 ? 9 : current.Value - 1; 
                while(nextThree.Contains(destination))
                {
                    destination = destination == 1 ? 9 : destination - 1;        
                }

                var destCup = cups.Find(destination);
                destCup = cups.AddAfter(destCup, nextThree[0]);
                destCup = cups.AddAfter(destCup, nextThree[1]);
                destCup = cups.AddAfter(destCup, nextThree[2]);

                current = current.NextOrWrapAround();
            }

            string output = "";
            var start = cups.Find(1).NextOrWrapAround();
            while(start.Value > 1)
            {
                output +=  start.Value;
                start = start.NextOrWrapAround();
            }

            return output;

        }
        public override string SolveSecondPuzzle()
        {
            var cupsList = Input.AsString().ToCharArray().Select(x => int.Parse(x+"")).ToList();

            int total = 1000000;
            cupsList.AddRange(Enumerable.Range(10, total - 9));
            var cups = cupsList.Zip(cupsList.Append(cupsList.First()).Skip(1)).ToDictionary(k => k.First, v => v.Second);

            var current = cupsList[0];
    
            for(int i = 0; i < 10000000; i++)
            {    
                var nextThree = new List<int>();
                var next = current;
                for(int j = 0; j < 3; j++)
                {
                    var n = cups[next];
                    nextThree.Add(n);
                    next = n;
                }

                var destination = current == 1 ? total : current - 1; 
                while(nextThree.Contains(destination))
                {
                    destination = destination == 1 ? total : destination - 1;        
                }

                var destCupAfter = cups[destination];
                cups[current] = cups[nextThree[2]];
                cups[destination] = nextThree[0];
                cups[nextThree[2]] = destCupAfter; 

                current = cups[current];

            }

            var one = cups[1];
            return ((long)one * cups[one]).ToString();
        }
    }

    public static class Extension
    {
        public static LinkedListNode<T> NextOrWrapAround<T>(this LinkedListNode<T> node){

            var nxt = node.Next;
            if(nxt == null)
                nxt = node.List.First;
            
            return nxt;
        }
    }
}
