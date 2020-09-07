using System;

namespace CardGames
{
    class Card
    {
        // I don't know why these are here
        private string face;
        private string suit;

        // Constructor
        public Card(string cardFace, string cardSuit)
        {
            face = cardFace;
            suit = cardSuit;
        }

        // Every class will have an existing .NET framework ToString() function which we will override
        public override string ToString()
        {
            return face + " of " + suit;
        }

        /* Default implementation of ToString() gives back a text representation which is generally
         * not a useful text representation
         * 
         * Almost every class should define a ToString() method so that you can produce,
         * for the caller, a useful string of information
         * 
         * Even if that string is just a string representation of all of the data inside of an object
         * that might be used for error message logging purposes
         * 
         * It's considered a good practice to provide such a method to help users of your class
         */

    }
}
