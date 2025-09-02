namespace FinalTask.Main
{
    internal class PlayerProfile
    {
        public readonly string Name;
        public int Bank { get; private set; }

        public PlayerProfile(string name, int bank)
        {
            Name = name;
            Bank = bank;
        }

        public bool TryGetMoney(int money)
        {
            if (money <= Bank)
            {
                Bank -= money;
                return true;
            }

            return false;
        }

        public void AddMoney(int money)
        {
            Bank = checked(Bank + money);
        }

        public override string ToString()
        {
            return $"Player name: {Name}\nBank: {Bank}";
        }
    }
}
