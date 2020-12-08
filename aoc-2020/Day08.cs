using System;
using System.Collections.Generic;
using System.Linq;
using aoc_core;

namespace aoc_2020
{
    public class Day08 : AdventPuzzle
    {
        public override string SolveFirstPuzzle()
        {
            var code = Input.AsCustomTypeEnumerable<(string command, int value)>(ParseCommands).ToArray();
            (int acc, _) = RunProgram(code);

            return acc.ToString();
        }

        public override string SolveSecondPuzzle()
        {
            var code = Input.AsCustomTypeEnumerable<(string command, int value)>(ParseCommands).ToArray();
            
            for(int i = 0; i< code.Length; i++)
            {             
                var codeCopy = new (string command, int value)[code.Length];
                code.CopyTo(codeCopy, 0);

                if(code[i].command == "jmp")
                    codeCopy[i].command = "nop";
                else if(code[i].command == "nop")
                    codeCopy[i].command = "jmp";
                else
                    continue;

                 (int acc, bool terminated) = RunProgram(codeCopy);

                 if(terminated)
                    return acc.ToString();

            }

            throw new Exception("Solution not found.");
        }

        private (int accValue, bool terminated) RunProgram((string command, int value)[] code)
        {
            int acc = 0;
            int commandPointer = 0;
            var pointerHistory = new HashSet<int>();

            while(commandPointer < code.Count())
            {
                var cmd = code[commandPointer];

                if(pointerHistory.Contains(commandPointer))
                    return (acc, false);

                pointerHistory.Add(commandPointer);
                switch(cmd.command)
                {
                    case "nop": commandPointer++; break;
                    case "jmp": commandPointer += cmd.value; break;
                    case "acc": commandPointer++; acc += cmd.value; break;
                    default: throw new Exception("Invalid command " + cmd.command);
                }

            }

            return (acc, true);
        }

        private (string, int) ParseCommands(string input)
        {
            var split = input.Split(' ');
            return (split[0], int.Parse(split[1]));
        }
    }
}
