using FinalTask.Staff;
using System;
using System.Collections.Generic;

namespace FinalTask.Games
{
    internal class CrapsGame : CasinoGameBase
    {
        private int _diceAmount;
        private int _minDiceValue;
        private int _maxDiceValue;

        private Dice _dice;

        public CrapsGame(int diceAmount, int min, int max)
        {
            _diceAmount = diceAmount;
            _minDiceValue = min;
            _maxDiceValue = max;

            FactoryMethod();
        }

        public override void PlayGame()
        {
            List<int> playerDices = new List<int>();
            List<int> computerDices = new List<int>();

            for (int i = 0; i < _diceAmount; i++)
            {
                playerDices.Add(_dice.Number);
            }

            for (int i = 0; i < _diceAmount; i++)
            {
                computerDices.Add(_dice.Number);
            }

            int playerPoints = SumOfDices(playerDices);
            int computerPoints = SumOfDices(computerDices);

            ShowDicesInfo("Player", playerDices, playerPoints);
            ShowDicesInfo("Dealer", computerDices, computerPoints);

            if (playerPoints > computerPoints)
            {
                OnWinInvoke();
            }
            else if (playerPoints < computerPoints)
            {
                OnLoseInvoke();
            }
            else
            {
                OnDrawInvoke();
            }
        }

        protected override void FactoryMethod()
        {
            _dice = new Dice(_minDiceValue, _maxDiceValue);
        }

        protected override void ShowGameResults()
        {
            throw new System.NotImplementedException();
        }

        private void ShowDicesInfo(string owner, List<int> dices, int sum = 0)
        {
            Console.WriteLine($"---------- {owner} Dices ----------\n");

            for (int i = 0; i < _diceAmount; i++)
            {
                Console.Write($"{dices[i]} ");
            }

            Console.WriteLine($"\nSum of dices: {sum}\n");
        }

        private int SumOfDices(List<int> dices)
        {
            int sum = 0;

            for (int i = 0; i < _diceAmount; i++)
            {
                sum += dices[i];
            }

            return sum;
        }
    }
}
