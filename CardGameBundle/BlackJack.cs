using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CardGameBundle
{
    class BlackJack
    {
        public static void Start()
        {
            Console.Clear();

            Deck newdeck = new Deck();
            newdeck.Shuffle();

            int roundWon = 0;
            int roundLost = 0;

            NewRound(roundWon, roundLost, newdeck);

        }

        static void NewRound(int roundWon, int roundLost, Deck newdeck)
        {
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
            bool doubleDown = false;
            bool stand = false;

            // Deal Starting Cards
            for (int i = 0; i < MIN_COUNT; i++)
            {
                playerHand[playerCount++] = DealCard(newdeck);
                dealerHand[dealerCount++] = DealCard(newdeck);
            }

            int[] stats = Score(playerHand, playerCount, playerSecond, playerAce, playerScore); // This is bad

            playerScore = stats[0];
            playerSecond = stats[1];
            playerAce = Convert.ToBoolean(stats[2]);

            int playerFinal = playerScore;

            while (stats[0] < 21 && !doubleDown && !stand)
            {
                // Ask if player wants more cards
                if (playerAce && playerSecond <= 21) // TODO: Fix playerSecond and/or make the game end immediately when BlackJack is drawn
                {
                    playerSecond = playerScore + 11;
                    Console.WriteLine($"Your cards: {playerScore} / {playerSecond}");
                    playerFinal = playerSecond;
                }
                else
                {
                    Console.WriteLine($"Your cards: {playerFinal}");
                }
                Status(playerCount, playerHand);

                Console.WriteLine("1. Hit\n2. Double Down\n3. Stand");
                int action = Convert.ToInt32(Console.ReadLine());

                Console.Clear();

                if (action == 1)
                {
                    playerHand[playerCount++] = DealCard(newdeck);
                    stats = Score(playerHand, playerCount, playerSecond, playerAce, playerScore); // Definitely bad
                    playerScore = stats[0];
                    playerSecond = stats[1];
                    playerAce = Convert.ToBoolean(stats[2]);
                    playerFinal = playerScore;
                }
                else if (action == 2)
                {
                    doubleDown = true;
                    {
                        playerHand[playerCount++] = DealCard(newdeck);
                        stats = Score(playerHand, playerCount, playerSecond, playerAce, playerScore);
                        playerScore = stats[0];
                        playerSecond = stats[1];
                        playerAce = Convert.ToBoolean(stats[2]);
                        playerFinal = playerScore;
                    }
                }
                else if (action == 3)
                {
                    stand = true;
                }
            }

            if (playerSecond <= 21 && playerSecond > playerScore)
            {
                playerFinal = playerSecond;
            }
            else
            {
                playerFinal = playerScore;
            }
           
            Console.Clear();

            stats = Score(dealerHand, dealerCount, dealerSecond, dealerAce, dealerScore); // Still bad
            dealerScore = stats[0];
            dealerSecond = stats[1];
            dealerAce = Convert.ToBoolean(stats[2]);

            int dealerFinal = dealerScore;

            if (EndRound(playerFinal, playerCount, playerHand, dealerFinal, dealerCount, dealerHand))
            {
                roundWon++;
                if (doubleDown)
                {
                    roundWon++;
                }
            }
            else
            {
                roundLost++;
                if (doubleDown)
                {
                    roundLost++;
                }
            }

            Console.WriteLine($"{roundWon} Won\n{roundLost} Lost");

            Console.WriteLine("\nPlay Again?\n1. Yes\n2. No\n");
            if (Console.ReadLine() == "1")
            {
                Console.Clear();
                NewRound(roundWon, roundLost, newdeck);
            }
        }

        static int[] Score(string[] playerHand, int playerCount, int playerSecond, bool playerAce, int playerScore) // Calculate current score
        {
            // Scoring Cards
            int[] stats = Comb(playerHand, playerCount, playerScore, playerSecond);
            playerScore = stats[0];
            playerSecond = stats[1];
            playerAce = Convert.ToBoolean(stats[2]);

            // Determine whether Ace = 1 or 11
            int playerFinal = playerScore;

            if (playerScore < playerSecond && playerSecond <= 21)
            {
                playerFinal = playerSecond;
            }
            return stats;
        }

        static void Status(int playerCount, string[] playerHand) // Display Player's Current Hand
        {
            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine(playerHand[i]);
            }
            Console.WriteLine();
        }

        static bool EndRound(int playerFinal, int playerCount, string[] playerHand, int dealerFinal, int dealerCount, string[]dealerHand) // End round and determine outcome
        {
            bool roundWon = false;
            // PLAYER RESULTS
            Console.WriteLine($"Your cards: {playerFinal}");
            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine(playerHand[i]);
            }
            Console.WriteLine();

            // DEALER RESULTS
            Console.WriteLine($"Dealer's cards: {dealerFinal}");
            for (int i = 0; i < dealerCount; i++)
            {
                Console.WriteLine("{0,-19}", dealerHand[i]);
            }
            Console.WriteLine();

            // Game Result
            if (playerFinal > dealerFinal && playerFinal <= 21)
            {
                Console.WriteLine("You won!");
                roundWon = true;
            }
            else if (playerFinal < 21 && dealerFinal > 21)
            {
                Console.WriteLine("You won!");
                roundWon = true;
            }
            else /*if (dealerFinal <= 21)*/
            {
                //if (playerFinal > 21 || playerFinal < dealerFinal)
                //{
                Console.WriteLine("You lose...");
                roundWon = false;
                //}
            }
            return roundWon;
        }

        static int[] Comb(string[] playerHand, int playerCount, int playerScore, int playerSecond) // Retrieve card values
        {
            playerScore = 0;
            bool playerAce = false;
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
                        playerSecond = playerScore + 11;
                        playerScore++;
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

        static string DealCard(Deck newdeck) // Deal Cards to selected player
        {
            string newCard;
            newCard = Convert.ToString(newdeck.DealCard());
            return newCard;
        }

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

        // Check to see if we can keep track of # of cards
        //Console.WriteLine($"Player Cards: {playerCount}\nDealer Cards: {dealerCount}");

    }
}
