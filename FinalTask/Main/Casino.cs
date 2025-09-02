using FinalTask.Games;
using System;


namespace FinalTask.Main
{
    public class Casino : IGame
    {
        private CasinoGameBase _blackJackGame;
        private CasinoGameBase _crapsGame;

        private PlayerProfile _playerProfile;
        private int _playerBet;
        private int _computerBet;

        public Casino()
        {
            _blackJackGame = new BlackjackGame();
            _crapsGame = new CrapsGame(3, 1, 6);
        }

        public void StartGame()
        {
            Console.WriteLine("Welcome to our casino!");

            // ---------- Load or create player profile ----------
            _playerProfile = new PlayerProfile("Player", 100);

            Console.WriteLine("Choose the game you want.");

            int chosenGame = GetChosenGameNumber();
            CasinoGameBase game = GetGame(chosenGame);

            ShowInfo(_playerProfile, chosenGame);

            _playerBet = GetBet(_playerProfile);
            _computerBet = _playerBet;

            game.OnWin += Game_OnWin;
            game.OnLose += Game_OnLose;
            game.OnDraw += Game_OnDraw;

            game.PlayGame();

            // ---------- Save player profile ----------


            Console.WriteLine("Good Bye!");
        }

        private void Game_OnDraw()
        {
            Pause();
            _playerProfile.AddMoney(_playerBet);
            ShowInfo(_playerProfile);

            Console.WriteLine("Draw! Your bet back to you.");
        }

        private void Game_OnLose()
        {
            Pause();
            ShowInfo(_playerProfile);
            Console.WriteLine("You lose!");

            if (_playerProfile.Bank == 0)
            {
                Console.WriteLine("No money? Kicked!");
            }
        }

        private void Game_OnWin()
        {
            Pause();
            bool isDrinkAway = false;

            try
            {
                _playerProfile.AddMoney(_playerBet + _computerBet);
            }
            catch (OverflowException)
            {
                int remainder = (_playerProfile.Bank + _playerBet + _computerBet) / 2 - _playerProfile.Bank;
                _playerProfile.AddMoney(remainder);
                isDrinkAway = true;
            }
            finally
            {
                ShowInfo(_playerProfile);

                Console.WriteLine("You win!");
                Console.WriteLine($"You get {_playerBet + _computerBet} money.");

                if (isDrinkAway)
                {
                    Console.WriteLine($"You wasted half of your bank money in casino’s bar");
                }
            }
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key to contiune.");
            Console.ReadKey();
        }

        private void ShowInfo(PlayerProfile profile, int chosenGame = -1)
        {
            Console.Clear();
            Console.WriteLine($"Your name: {profile.Name}");
            Console.WriteLine($"Your bank: {profile.Bank}\n");

            if (chosenGame == -1)
            {
                return;
            }

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

            Console.WriteLine(game);
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
            while (input != 1 && input != 2);

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
                    return _blackJackGame;

                case 2:
                    return _crapsGame;

                default:
                    throw new Exception($"Game with number {number} doesn't exsit!");
            }
        }
    }
}
