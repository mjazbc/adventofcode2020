using System;
using System.Collections.Generic;
using System.Linq;
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

            cupsList.AddRange(Enumerable.Range(10, 1000000 - 9));

            var cups = new LinkedList<int>(cupsList);

            // HashSet<LinkedList<int>> history = new HashSet<LinkedList<int>>();
            // history.Add()
            var current = cups.First;

            for(int i = 0; i < 10000; i++)
            {       
                // Console.WriteLine(i);
                var nextThree = new List<int>();
                for(int j = 0; j < 3; j++)
                {
                    var n = current.NextOrWrapAround();
                    nextThree.Add(n.Value);
                    cups.Remove(n);
                }

                var destination = current.Value == 1 ? 1000000 : current.Value - 1; 
                while(nextThree.Contains(destination))
                {
                    destination = destination == 1 ? 1000000 : destination - 1;        
                }

                var destCup = cups.Find(destination);
                destCup = cups.AddAfter(destCup, nextThree[0]);
                destCup = cups.AddAfter(destCup, nextThree[1]);
                destCup = cups.AddAfter(destCup, nextThree[2]);

                current = current.NextOrWrapAround();

                if(cups)
            }

            var one = cups.Find(1);
            return ((long)one.NextOrWrapAround().Value * one.NextOrWrapAround().NextOrWrapAround().Value).ToString();
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
