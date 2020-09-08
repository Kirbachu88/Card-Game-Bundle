using System;

namespace CardGameBundle
{
    class CardGame
    {

        static void Main(string[] args)
        {
            GameSelect();

            Console.ReadLine();
        }

        // Select game from list
        static void GameSelect()
        {
            Console.WriteLine("Please select a game:");
            Console.WriteLine("1. Blackjack\n2. Go Fish\n3. 52 Card Look Up");

            try
            {
                int selection = Convert.ToInt32(Console.ReadLine());

                if (selection == 1)
                {
                    BlackJack.Start();
                }
                else if (selection == 2)
                {
                    GoFish.Start();
                }
                else if (selection == 3)
                {
                    Console.Clear();
                    CardLookUp.Start();
                }
                else
                {
                    Console.WriteLine("That's not an option.");
                    Console.ReadLine();
                    GameSelect();
                }
            }
            catch
            {
                Console.WriteLine("You think this is funny?");
                Console.ReadLine();
                GameSelect();
            }

        }

    }
}
