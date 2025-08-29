using FinalTask.Games;

namespace FinalTask
{
    class Program
    {
        static void Main(string[] args)
        {
            //CasinoGameBase game = new CrapsGame(5, 1, 6);
            CasinoGameBase game = new BlackjackGame(5);
            game.PlayGame();
        }
    }
}
