using System;

namespace CardGameBundle
{
    class Blackjack
    {
        public static void Start()
        {
            Console.Clear();

            Deck newdeck = new Deck();
            newdeck.Shuffle();

            NewRound(newdeck);
        }

        static void NewRound(Deck newdeck, int roundWon = 0, int roundLost = 0)
        {
            // USER VARIABLES
            string[] playerHand = new string[11];
            int playerCount = 0;
            int playerScore = 0;
            int playerAce = 0;

            // DEALER VARIABLES
            string[] dealerHand = new string[11];
            int dealerCount = 0;
            int dealerScore = 0;
            int dealerAce = 0;

            // GAME CONDITIONS
            bool doubleDown = false;
            bool cont = false;

            // Deal Starting Cards
            Hit(ref playerHand, ref playerCount, newdeck);
            Hit(ref dealerHand, ref dealerCount, newdeck);

            // Determine user's initial score
            Score(out playerScore, out playerAce, playerHand);

            while (!cont)
            {
                // PLAYER STATS TEST
                Console.WriteLine($"Score: {playerScore} Count: {playerCount} Aces: {playerAce}");
                Read(playerHand, playerCount);

                Console.WriteLine("1. Hit\n2. Double Down\n3. Stand");

                string action = Console.ReadLine();
                switch (action)
                {
                    case "1":
                        playerHand[playerCount++] = DealCard(newdeck);
                        Score(out playerScore, out playerAce, playerHand, playerCount);
                        break;
                    case "2":
                        playerHand[playerCount++] = DealCard(newdeck);
                        Score(out playerScore, out playerAce, playerHand, playerCount);
                        doubleDown = true;
                        cont = true;
                        break;
                    case "3":
                        cont = true;
                        break;
                    default:
                        Console.WriteLine("Please enter a number from 1-3.");
                        break;
                }
                Console.Clear();
            }

            // PLAYER STATS TEST
            Console.WriteLine($"Score: {playerScore} Count: {playerCount} Aces: {playerAce}");
            Read(playerHand, playerCount);

            // Determine dealer's score
            Score(out dealerScore, out dealerAce, dealerHand);

            // DETERMINE WINNER AND DOUBLE DOWN STATUS

            // Ask if user wants to play again
            Console.WriteLine("Type \"QUIT\" to exit game: ");
            if (Console.ReadLine().ToUpper() == "QUIT")
            {

            }
            else
            {
                Console.Clear();
                NewRound(newdeck, roundWon, roundLost);
            }
        }
        static void Hit(ref string[] Hand, ref int Count, Deck newdeck, int number = 2)
        {
            for (int i = 0; i < number; i++)
            {
                Hand[Count++] = DealCard(newdeck);
            }
            
        }                       // Add card(s) to a player's hand
        static void Read(string[] playerHand, int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine(playerHand[i]);
            }
        }                                            // List off cards
        static void Score(out int playerScore, out int playerAce, string[] playerHand, int playerCount= 2)
        {
            playerScore = 0;
            playerAce = 0;
            for (int i = 0; i < playerCount; i++)
            {
                string currentCard = playerHand[i];
                switch (playerHand[i])
                {
                case string a when a.Contains("Ace"):
                        playerAce++; // Just calculate this outside of here
                        break;
                case string a when a.Contains("Two"):
                        playerScore += 2;
                        break;
                case string a when a.Contains("Three"):
                        playerScore += 3;
                        break;
                case string a when a.Contains("Four"):
                        playerScore += 4;
                        break;
                case string a when a.Contains("Five"):
                        playerScore += 5;
                        break;
                case string a when a.Contains("Six"):
                        playerScore += 6;
                        break;
                case string a when a.Contains("Seven"):
                        playerScore += 7;
                        break;
                case string a when a.Contains("Eight"):
                        playerScore += 8;
                        break;
                case string a when a.Contains("Nine"):
                        playerScore += 9;
                        break;
                default: // 10 and Face Cards
                        playerScore += 10;
                        break;
                } // I still need a hug
            }
        }   // Retrieve card values
        static int Final(int playerScore, int playerAce)
        {
            switch (playerAce)
            {
                case 0:
                    break;
                case 1:
                    break;
                default:
                    break;
            }
            return playerScore;
        }
        static string DealCard(Deck newdeck)
        {
            string newCard;
            newCard = Convert.ToString(newdeck.DealCard());
            return newCard;
        }
    }
}
