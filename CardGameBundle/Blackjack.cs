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
            bool stand = false;

            // DEAL STARTING HAND
            Hit(ref playerHand, ref playerCount, newdeck);
            Hit(ref dealerHand, ref dealerCount, newdeck);

            // GET USER SCORE
            Comb(out playerScore, out playerAce, playerHand, playerCount);
            playerScore = Ace(playerScore, playerAce);

            // Immediately end user turn when win/lose condition is met
            if (playerScore >= 21)
            {
                stand = true;
            }

            // PLAYER'S TURN
            while (!stand)
            {
                Comb(out playerScore, out playerAce, playerHand, playerCount);
                Console.WriteLine($"Your cards: {playerScore = Ace(playerScore, playerAce)}");
                Read(playerHand, playerCount);
                if (playerScore >= 21)
                {
                    stand = true;
                    Console.Clear();
                }
                if (!stand)
                {
                    Console.WriteLine("\n1. Hit\n2. Double Down\n3. Stand");

                    string action = Console.ReadLine();
                    switch (action)
                    {
                        case "1":
                            playerHand[playerCount++] = DealCard(newdeck);
                            Console.Clear();
                            break;
                        case "2":
                            playerHand[playerCount++] = DealCard(newdeck);
                            doubleDown = true;
                            stand = true;
                            Console.Clear();
                            break;
                        case "3":
                            stand = true;
                            Console.Clear();
                            break;
                        default:
                            Console.WriteLine("Please enter a number from 1-3.");
                            break;
                    }

                    Comb(out playerScore, out playerAce, playerHand, playerCount);
                    Ace(playerScore, playerAce);

                    // Immediately end user turn when win/lose condition is met
                    if (playerScore >= 21)
                    {
                        stand = true;
                    }
                }
            }

            // LIST USER'S SCORE
            Comb(out playerScore, out playerAce, playerHand, playerCount);
            Console.WriteLine($"Your cards: {playerScore = Ace(playerScore, playerAce)}");
            Read(playerHand, playerCount);

            Console.WriteLine();

            // DEALER'S TURN
            bool dealerTurn = true;

            Comb(out dealerScore, out dealerAce, dealerHand, dealerCount);
            dealerScore = Ace(dealerScore, dealerAce);

            while (dealerTurn)
            {
                if ((dealerScore < 17) && (dealerScore < playerScore) && (playerScore <= 21))
                {
                    dealerHand[dealerCount++] = DealCard(newdeck);
                }
                else
                {
                    dealerTurn = false;
                }
                Comb(out dealerScore, out dealerAce, dealerHand, dealerCount);
                dealerScore = Ace(dealerScore, dealerAce);
            } // END OF DEALER'S TURN

            // LIST DEALER'S SCORE
            Comb(out dealerScore, out dealerAce, dealerHand, dealerCount);
            Console.WriteLine($"Dealer's cards: {dealerScore = Ace(dealerScore, dealerAce)}");
            Read(dealerHand, dealerCount);

            Console.WriteLine();


            // DETERMINE WINNER AND DOUBLE DOWN STATUS
            if ((playerScore > 21) || (playerScore <= dealerScore) && (dealerScore <= 21))
            {
                roundLost = WinLose(roundLost, doubleDown);
                Console.WriteLine("You lose...");
            }
            else
            {
                roundWon = WinLose(roundWon, doubleDown);
                Console.WriteLine("You won!");
            }

            Console.WriteLine($"{roundWon} Wins\n{roundLost} Losses\n");



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
        static void Hit(ref string[] Hand, ref int Count, Deck newdeck)
        {
            for (int i = 0; i < 2; i++)
            {
                Hand[Count++] = DealCard(newdeck);
            }
            
        }                                   // Add card(s) to a player's hand
        static void Read(string[] playerHand, int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine(playerHand[i]);
            }
        }                                            // List off cards
        static void Comb(out int playerScore, out int playerAce, string[] playerHand, int playerCount)
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
        }    // Retrieve card values
        static int Ace(int playerScore, int playerAce)
        {
            if (playerAce > 0)                // Definitely 1-4 Aces
            {
                if (playerScore >= 11)        // Definitely equals 1
                {
                    playerScore += playerAce;
                } 
                else                          // Maybe equals 11?
                {
                    playerScore += 11;
                    if (playerAce - 1 > 0)
                    {
                        playerScore += playerAce;
                    }
                }
            }
            return playerScore;
        }                                                    // Calculate Aces
        static int WinLose(int aRound, bool doubleDown)
        {
            if (doubleDown)
            {
                ++aRound;
            }
            return ++aRound;
        }                                                   // Increment Win/Loss count
        static string DealCard(Deck newdeck)
        {
            string newCard;
            newCard = Convert.ToString(newdeck.DealCard());
            return newCard;
        }
    }
}
