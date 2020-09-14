using System;

namespace CardGameBundle
{
    class CardLookUp
    {
        public static void Start()
        {
            Deck newdeck = new Deck();      // Create new deck class
            newdeck.Shuffle();              // Shuffle deck
            for (int i = 0; i < 52; i++)    // Output all cards
            {
                Console.Write("{0,-19}", newdeck.DealCard()); // Negative number results in a left-justified field
                if ((i + 1) % 4 == 0)       // Print 4 cards per line
                {
                    Console.WriteLine();
                }

            }

            Console.ReadLine();
        }

    }
}
