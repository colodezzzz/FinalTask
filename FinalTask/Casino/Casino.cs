using FinalTask.Games;
using System;

namespace FinalTask.Casino
{
    internal class Casino : IGame
    {
        CasinoGameBase _blackJackGame;
        CasinoGameBase _crapsGame;

        public Casino()
        {
            _blackJackGame = new BlackjackGame();
            _crapsGame = new CrapsGame(3, 1, 6);
        }

        public void StartGame()
        {
            Console.WriteLine("Welcome to our casino!");

            // ---------- Load or create player profile ----------
            PlayerProfile playerProfile = new PlayerProfile("Player", 100);

            Console.WriteLine("Choose the game you want.");

            int chosenGame = GetChosenGameNumber();
            CasinoGameBase game = GetGame(chosenGame);

            ShowInfo(playerProfile, chosenGame);

            int bet = GetBet(playerProfile);


        }

        private void ShowInfo(PlayerProfile profile, int chosenGame)
        {
            Console.Clear();

            Console.WriteLine($"Your name: {profile.Name}");
            Console.WriteLine($"Your bank: {profile.Bank}");

            string game = "Chosen game: ";

            switch (chosenGame)
            {
                case 1:
                    game += "Blackjack";
                    break;

                case 2:
                    game += "Craps";
                    break;

                default:
                    break;
            }

            Console.WriteLine('\n' + game);
        }

        private int GetChosenGameNumber()
        {
            int input;

            do
            {
                Console.WriteLine("Enter 1 to play blackjack.\nEnter 2 to play craps.");

                if (int.TryParse(Console.ReadLine(), out input) == false)
                {
                    continue;
                }
            }
            while (input != 1 || input != 2);

            return input;
        }

        private int GetBet(PlayerProfile profile)
        {
            int input = 0;

            while (true)
            {
                Console.Write("Place your bet: ");

                if (int.TryParse(Console.ReadLine(), out input))
                {
                    if (input > 0 && profile.TryGetMoney(input))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Your bet must bet greater than 0 and less or equal than your bank!");
                    }
                }
            }

            return input;
        }

        private CasinoGameBase GetGame(int number)
        {
            switch (number)
            {
                case 1:
                    return new BlackjackGame();

                case 2:
                    return new CrapsGame(3, 1, 6);

                default:
                    throw new Exception($"Game with number {number} doesn't exsit!");
            }
        }
    }
}
