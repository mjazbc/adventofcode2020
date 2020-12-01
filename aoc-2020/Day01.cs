using System;
using aoc_core;

namespace aoc_2020
{
    public class Day01 : AdventPuzzle
    {

        public override string SolveFirstPuzzle()
        {
            var input = Input.AsIntArray();

            for(int i = 0; i < input.Length; i++)
            {
                for(int j = i+1; j < input.Length; j++)
                {
                    if(input[i] + input[j] == 2020)
                        return (input[i] * input[j]).ToString();
                }
            }

            throw new Exception("Result not found.");

        }

        public override string SolveSecondPuzzle()
        {
            var input = Input.AsIntArray();

            for(int i = 0; i < input.Length; i++)
            {
                for(int j = i+1; j < input.Length; j++)
                {
                    for(int k = j+1; k < input.Length; k++)
                    {
                        if(input[i] + input[j] + input[k] == 2020)
                            return (input[i] * input[j] * input[k]).ToString();
                    }
                }
            }

            throw new Exception("Result not found.");
        }
    }
}
