using System;
using System.Collections.Generic;
using System.Linq;
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
            foreach (var line in input.Skip(1))
            {
                if (int.TryParse(line, out int card))
                {
                    if (player1)
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

            while (player1Deck.Any() && player2Deck.Any())
            {
                var p1Card = player1Deck.Dequeue();
                var p2Card = player2Deck.Dequeue();

                if (p1Card > p2Card)
                {
                    player1Deck.Enqueue(p1Card);
                    player1Deck.Enqueue(p2Card);
                }
                else
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

            var winner = PlayGame(player1Deck, player2Deck);

            var score = winner == 1 ? CalculatePlayersScore(player1Deck) : CalculatePlayersScore(player2Deck);
            return score.ToString();

        }

        private int PlayGame(Queue<int> deck1, Queue<int> deck2)
        {
            var prevDecks = new HashSet<string>();
            while (deck1.Any() && deck2.Any())
            {
                
                var serdeck1 = SerializeDeck(1, deck1);
                var serdeck2 = SerializeDeck(2, deck2);

                if (prevDecks.Contains(serdeck1) || prevDecks.Contains(serdeck2))
                    return 1;

                prevDecks.Add(serdeck1);
                prevDecks.Add(serdeck2);

                var c1 = deck1.Dequeue();
                var c2 = deck2.Dequeue();

                int winner = 0;
                if (deck1.Count >= c1 && deck2.Count >= c2)
                {
                    var d1 = new Queue<int>(deck1.Take(c1));
                    var d2 = new Queue<int>(deck2.Take(c2));

                    winner = PlayGame(d1, d2);
                }
                else
                {
                    winner = c1 > c2 ? 1 : 2;
                }

                if (winner == 1)
                {
                    deck1.Enqueue(c1);
                    deck1.Enqueue(c2);
                }
                else if (winner == 2)
                {
                    deck2.Enqueue(c2);
                    deck2.Enqueue(c1);
                }
                else
                    throw new Exception("Unknown winner :o");

            }

            return deck1.Any() ? 1 : 2;
        }

        private string SerializeDeck(int player, Queue<int> deck) => $"{player} {string.Join(", ", deck)}";
    }
}
