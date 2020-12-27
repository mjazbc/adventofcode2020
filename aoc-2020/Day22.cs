using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using aoc_core;

namespace aoc_2020
{
    public class Day22 : AdventPuzzle
    {
        public (Queue<int> player1Deck, Queue<int> player2Deck) ParseDecks()
        {
            var player1Deck = new Queue<int>();
            var player2Deck = new Queue<int>();

            var input = Input.AsStringArray();
            bool player1 = true;
            foreach(var line in input.Skip(1))
            {   
                if(int.TryParse(line, out int card))
                {
                    if(player1)
                        player1Deck.Enqueue(card);
                    else
                        player2Deck.Enqueue(card);
                }
                else
                    player1 = false;
            }

            return (player1Deck, player2Deck);
        }
        public override string SolveFirstPuzzle()
        {
            (Queue<int> player1Deck, Queue<int> player2Deck) = ParseDecks();

            while(player1Deck.Any() && player2Deck.Any())
            {
                var p1Card = player1Deck.Dequeue();
                var p2Card = player2Deck.Dequeue();

                if(p1Card > p2Card)
                {
                    player1Deck.Enqueue(p1Card);
                    player1Deck.Enqueue(p2Card);
                }else
                {
                    player2Deck.Enqueue(p2Card);
                    player2Deck.Enqueue(p1Card);
                }         
            }

            var score = player1Deck.Any() ? CalculatePlayersScore(player1Deck) : CalculatePlayersScore(player2Deck);
            return score.ToString();
        }

        private int CalculatePlayersScore(Queue<int> deck)
        {
            return Enumerable.Range(1, deck.Count).Zip(deck.Reverse()).Sum(x => x.First * x.Second);
        }

        public override string SolveSecondPuzzle()
        {
            (Queue<int> player1Deck, Queue<int> player2Deck) = ParseDecks();

            throw new NotImplementedException();

        }

        
    }
}
