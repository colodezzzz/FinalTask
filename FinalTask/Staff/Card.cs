using FinalTask.Utils;

namespace FinalTask.Staff
{
    public struct Card
    {
        public readonly CardType Type;
        public readonly CardValue Value;

        public Card(CardType type, CardValue value)
        {
            Type = type;
            Value = value;
        }
    }
}
