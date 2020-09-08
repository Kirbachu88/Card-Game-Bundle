using System;

namespace CardGameBundle
{
    class GoFish
    {
        public static void Start()
        {
            Console.WriteLine("><///*> blub blub blub");

            string[] playerHand = new string[39];
            string[] dealerHand = new string[39];

            Deck newdeck = new Deck();
            newdeck.Shuffle();

        }
    }
}
