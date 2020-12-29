using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day25 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var keys = Input.AsIntArray();
            int cardKey = keys[0];
            int doorKey = keys[1];

            int cardLoopsSize = FindLoopSize(cardKey, 7);
            int doorLoopsSize = FindLoopSize(doorKey, 7);
            
            long key = Calculate(cardLoopsSize, doorKey);
            
            return key.ToString();     
        }

        public long Calculate(int loopSize, int subject)
        {
            long num = 1;
            for(int i = 0; i < loopSize; i++)
                num = (num * subject) % 20201227;

            return num;
        }

        public int FindLoopSize(int key, int subject)
        {
            int loops = 0;
            int num = 1;
            while(num != key)
            {
                num = (num * subject) % 20201227;
                loops++;
            }

            return loops;
        }

        public override string SolveSecondPuzzle()
        {
            return "MERRY CHRISTMAS!";
        }
    }
}