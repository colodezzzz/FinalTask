using System;

namespace FinalTask.Staff
{
    public struct Dice
    {
        private int _min;
        private int _max;
        private Random _random;

        public int Number => _random.Next(_min, _max + 1);

        public Dice(int min, int max)
        {
            if (min < 1)
            {
                throw new WrongDiceNumberException($"Wrong dice number!\n{min} entered, but available from {1} to {int.MaxValue}.");
            }

            if (max > int.MaxValue)
            {
                throw new WrongDiceNumberException($"Wrong dice number!\n{max} entered, but available from {1} to {int.MaxValue}.");
            }

            if (max < min)
            {
                int t = min;
                min = max;
                max = t;
            }

            _min = min;
            _max = max;

            _random = new Random();
        }
    }

    public class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException(string message) : base(message)
        {

        }
    }
}
