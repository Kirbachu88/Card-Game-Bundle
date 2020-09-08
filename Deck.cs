using System;

namespace CardGameBundle
{
    class Deck // All 3 classes are in the same project/namespace, therefore know about each other already
    {
        private Card[] deck;                        // Private array of cards DECLARING only, has null value
        private int currentCard;                    // Keep track of where we are in our deck
        private const int NUMBER_OF_CARDS = 52;     // Constant integer number of cards in a deck
        private System.Random randNum;              // Random number for shuffle function

        public Deck() // Constructor which will populate deck
        {
            // Card faces and values
            string[] faces = { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace" }; // "Ten" is a thing don't forget it.
            string[] suit = { "Spades", "Clubs", "Diamonds", "Hearts" };

            // Create a new deck with Card class
            deck = new Card[NUMBER_OF_CARDS]; // Creates array with 52 elements, NOT CARD OBJECTS, they are variables that CAN REFER TO card objects, default value for each is null
            currentCard = 0;                  // Default value for an integer that's declared as an instance variable and NOT initialized
            randNum = new Random();
            
            for(int count = 0; count < deck.Length; count++) // Populate deck
            {
                deck[count] = new Card(faces[count % 13], suit[count / 13]);
                // For every element of array, assign a new card object which will be initialized as a result of the two expressions

                /* Faces: Divide count by 13 (NOT 11 THIS CAUSES DUPLICATE/MISSING CARDS), give remainder after integer division, will always be number between 0-12
                 * Exactly the set of index values for the thirteen element array interfaces
                 * Keep iterating from 0-12 for each set of 13 cards within the array
                 * 
                 * Suits: When you divide count by 13 (DIVISION OPERATOR), it will always result in an integer value
                 * If value of count is 0-12, the value of the expression will be 0, to pick off suit[0], next 13 suit[1], etc.
                 * Iterate 0, 1, 2, or 3 for the first 13 cards, the second 13 cards, etc.
                 * 
                 * Even the debugger will use the ToString() method to show each element (Put stop at end of method to view)
                 */

            } // Deck location = new Card(Return remainder of count divided by 11, Suits divided by 13 = No remainder)
        }

        public void Shuffle()
        {
            // Set currentCard to 0 and shuffle deck
            currentCard = 0;
            for (int first = 0; first < deck.Length; first++)
            {
                // Declare another integer (second) to store the next random card
                int second = randNum.Next(NUMBER_OF_CARDS);

                // Store first of the deck into the temporary variable
                Card temp = deck[first];

                // Store second of deck into first
                deck[first] = deck[second];

                // Store temporary back into second
                deck[second] = temp;
            }
        }

        public Card DealCard()
        {
            if (currentCard < deck.Length) // Stay within bounds of deck array
            {
                return deck[currentCard++]; // Return the next card in the deck
            }
            else
            {
                return null; // Indicate that all cards were dealt
            }
        }
    }
}
