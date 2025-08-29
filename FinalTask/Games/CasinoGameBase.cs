using System;

namespace FinalTask.Games
{
    public abstract class CasinoGameBase
    {
        public event Action OnWin;
        public event Action OnLose;
        public event Action OnDraw;

        public CasinoGameBase()
        {
            //FactoryMethod();
        }

        public abstract void PlayGame();

        protected abstract void FactoryMethod();

        protected abstract void ShowGameResults();

        protected void OnWinInvoke()
        {
            OnWin?.Invoke();
        }

        protected void OnLoseInvoke()
        {
            OnLose?.Invoke();
        }

        protected void OnDrawInvoke()
        {
            OnDraw?.Invoke();
        }
    }
}
