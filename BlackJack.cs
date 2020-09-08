using System;

namespace CardGameBundle
{
    class BlackJack
    {
        public static void Start()
        {
            Console.Clear();

            Deck newdeck = new Deck();
            newdeck.Shuffle();
            string[] playerHand = new string[11];
            string[] dealerHand = new string[11];

            const int MIN_COUNT = 2; // Starting cards
            int playerCount = 0;     // Total player cards
            int dealerCount = 0;     // Total dealer cards

            int playerScore = 0;     // Player's current score
            int playerSecond = 0;    // Score if Ace can be 11
            bool playerAce = false;  // Display secondary score if Ace is in hand and <21

            int dealerScore = 0;
            int dealerSecond = 0;
            bool dealerAce = false;

            int roundWon = 0;
            int roundLost = 0;

            int[] stats = new int[3];

            // Deal Starting Cards
            for (int i = 0; playerCount < MIN_COUNT; i++)
            {
                playerHand[playerCount++] = DealCard(newdeck);
                dealerHand[dealerCount++] = DealCard(newdeck);
            }

            // Scoring Cards
            stats = Score(playerHand, playerCount, playerScore, playerSecond, playerAce);
            playerScore = stats[0];
            playerSecond = stats[1];
            playerAce = Convert.ToBoolean(stats[2]);

            stats = Score(dealerHand, dealerCount, dealerScore, dealerSecond, dealerAce);
            dealerScore = stats[0];
            dealerSecond = stats[1];
            dealerAce = Convert.ToBoolean(stats[2]);

            // Get Player and Dealer score
            //Console.WriteLine("Round results: ");
            //if (playerAce && playerSecond <= 21)
            //{
            //    Console.WriteLine($"{playerScore} / {playerSecond}");
            //}
            //else
            //{
            //    Console.WriteLine(playerScore);
            //}
            //if (dealerAce && dealerSecond <= 21)
            //{
            //    Console.WriteLine($"{dealerScore} / {dealerSecond}");
            //}
            //else
            //{
            //    Console.WriteLine(dealerScore);
            //}

            // Determine whether Ace = 1 or 11
            int playerFinal = playerScore;
            int dealerFinal = dealerScore;

            if (playerScore < playerSecond && playerSecond <= 21)
            {
                playerFinal = playerSecond;
            }
            if (dealerScore < dealerSecond && dealerSecond <= 21)
            {
                dealerFinal = dealerSecond;
            }

            // PLAYER RESULTS
            Console.WriteLine($"Your cards: {playerFinal}");
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine(playerHand[i]);
            }
            Console.WriteLine();

            // DEALER RESULTS
            Console.WriteLine($"Dealer's cards: {dealerFinal}");
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("{0,-19}", dealerHand[i]);
            }
            Console.WriteLine();

            // Game Result
            if (playerFinal > dealerFinal && playerFinal <= 21)
            {
                Console.WriteLine("You won!");
                roundWon++;
            }
            else if (playerFinal < 21 && dealerFinal > 21)
            {
                Console.WriteLine("You won!");
                roundWon++;
            }
            else /*if (dealerFinal <= 21)*/
            {
                //if (playerFinal > 21 || playerFinal < dealerFinal)
                //{
                Console.WriteLine("You lose...");
                roundLost++;
                //}
            }

            Console.WriteLine($"{roundWon} Won\n{roundLost} Lost");

            // Check to see if we can keep track of # of cards
            //Console.WriteLine($"Player Cards: {playerCount}\nDealer Cards: {dealerCount}");

        }

        // METHOD FOR GETTING EITHER PLAYER'S SCORES
        static int[] Score(string[] playerHand, int playerCount, int playerScore, int playerSecond, bool playerAce)
        {
            int[] stats = new int[3];
            for (int i = 0; i < playerCount; i++)
            {
                string currentCard = playerHand[i];

                if (currentCard.Contains("Ten") || currentCard.Contains("Jack") || currentCard.Contains("Queen") || currentCard.Contains("King"))
                {
                    playerScore += 10;
                }
                else if (currentCard.Contains("Ace"))
                {
                    if (playerScore + 11 <= 21)
                    {
                        playerAce = true;
                        playerSecond += 11;
                    }
                    else
                    {
                        playerAce = false;
                        playerScore++;
                    }
                }
                else // I just don't know what is even life
                {
                    switch (playerHand[i])
                    {
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
                    } // I need a hug
                }

            }

            if (playerSecond > 21)
            {
                playerAce = false;
            }

            stats[0] = playerScore;
            stats[1] = playerSecond;
            stats[2] = Convert.ToInt32(playerAce);
            return stats;
        }

        // Method to Deal Cards to selected player
        static string DealCard(Deck newdeck)
        {
            string newCard;
            newCard = Convert.ToString(newdeck.DealCard());
            return newCard;
        }

    }
}
