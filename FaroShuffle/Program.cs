using System;
using System.Collections.Generic;
using System.Linq;

namespace FaroShuffle
{
    static class Program
    {
        static void Main(string[] args)
        {
            // Query Syntax
            var startingDeck = (from s in Suits().LogQuery("Suit Generation")
                               from r in Ranks().LogQuery("Rank Generation")
                               select new { Suit = s, Rank = r })
                               .LogQuery("Starting Deck")
                               .ToArray();

            // Method Syntax
            //var startingDeck = Suits().SelectMany(suit => Ranks().Select(rank => new { Suit = suit, Rank = rank }));

            var top = startingDeck.Take(26);
            var bottom = startingDeck.Skip(26);
            //var shuffle = top.InterleaveSequenceWith(bottom);

            var times = 0;

            var shuffle = startingDeck;
            do
            {
                shuffle = shuffle.Skip(26).LogQuery("Bottom Half")
                    .InterleaveSequenceWith(shuffle.Take(26)).LogQuery("Top Half")
                    .LogQuery("Shuffle")
                    .ToArray();

                foreach (var card in shuffle)
                {
                    Console.WriteLine(card);
                }
                Console.WriteLine();
                times++;
            } while (!startingDeck.SequenceEquals(shuffle));

            Console.WriteLine(times);
        }

        static IEnumerable<string> Suits()
        {
            yield return "clubs";
            yield return "diamons";
            yield return "hearts";
            yield return "spades";
        }

        static IEnumerable<string> Ranks()
        {
            yield return "two";
            yield return "three";
            yield return "four";
            yield return "five";
            yield return "six";
            yield return "seven";
            yield return "eight";
            yield return "nine";
            yield return "ten";
            yield return "jack";
            yield return "queen";
            yield return "king";
            yield return "ace";
        }
    }
}
