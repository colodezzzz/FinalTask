using FinalTask.Utils;
using System;

namespace FinalTask.Staff
{
    public struct Card : IComparable<Card>
    {
        public readonly CardType Type;
        public readonly CardValue Value;

        public Card(CardType type, CardValue value)
        {
            Type = type;
            Value = value;
        }

        public int CompareTo(Card otherCard)
        {
            if ((int)Value > (int)otherCard.Value)
            {
                return 1;
            }
            else if ((int)Value < (int)otherCard.Value)
            {
                return -1;
            }

            return 0;
        }
    }
}
