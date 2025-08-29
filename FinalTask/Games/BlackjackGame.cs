using System;
using System.Collections.Generic;
using FinalTask.Staff;
using FinalTask.Utils;

namespace FinalTask.Games
{
    class BlackjackGame : CasinoGameBase
    {
        private const string OPPONENT_NAME = "Dealer";
        private const int MAX_POINTS_VALUE = 21;

        private List<Card> _cards;
        private Queue<Card> _deck;

        public BlackjackGame(int cardsAmount) 
        {
            FactoryMethod();
        }

        public override void PlayGame()
        {
            Shuffle();

            List<Card> playerCards = new List<Card>();
            List<Card> computerCards = new List<Card>();

            // Player's take
            playerCards.Add(_deck.Dequeue());
            playerCards.Add(_deck.Dequeue());

            // Computer's take
            computerCards.Add(_deck.Dequeue());
            computerCards.Add(_deck.Dequeue());

            int playerPoints = CalculatePoints(playerCards);
            int computerPoints = CalculatePoints(computerCards);

            ShowCardsInfo("Player", playerCards);
            ShowCardsInfo(OPPONENT_NAME, computerCards);

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
                do
                {
                    Pause();
                    Console.Clear();

                    playerCards.Add(_deck.Dequeue());
                    computerCards.Add(_deck.Dequeue());

                    playerPoints = CalculatePoints(playerCards);
                    computerPoints = CalculatePoints(computerCards);

                    ShowCardsInfo("Player", playerCards);
                    ShowCardsInfo(OPPONENT_NAME, computerCards);
                } 
                while (playerPoints == computerPoints && playerPoints < MAX_POINTS_VALUE);

                if (playerPoints >= MAX_POINTS_VALUE && computerPoints >= MAX_POINTS_VALUE)
                {
                    OnDrawInvoke();
                }
                else if (playerPoints <= MAX_POINTS_VALUE && (computerPoints > MAX_POINTS_VALUE || computerPoints < playerPoints))
                {
                    OnWinInvoke();
                }
                else if (computerPoints <= MAX_POINTS_VALUE && (playerPoints > MAX_POINTS_VALUE || playerPoints < computerPoints))
                {
                    OnLoseInvoke();
                }
            }
        }

        protected override void FactoryMethod()
        {
            _cards = new List<Card>();

            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    Card card = new Card(type, value);
                    _cards.Add(card);
                }
            }
        }

        protected override void ShowGameResults()
        {
            throw new NotImplementedException();
        }

        private void Pause()
        {
            Console.WriteLine("\nPree any kay to contiune.");
            Console.ReadKey();
        }

        private int GetCardValue(CardValue value)
        {
            switch (value)
            {
                case CardValue.Six:
                case CardValue.Seven:
                case CardValue.Eight:
                case CardValue.Nine:
                case CardValue.Ten:
                    return (int)value;

                case CardValue.Jack:
                case CardValue.Queen:
                case CardValue.King:
                    return (int)CardValue.Ten;

                case CardValue.Ace:
                    return 11;

                default:
                    throw new Exception("Invalid card type!");
            }
        }

        private void ShowCard(Card card)
        {
            Console.WriteLine($"Карта: {card.Type} - {card.Value}");
        }

        private void ShowCardsInfo(string owner, List<Card> cards)
        {
            Console.WriteLine($"--------- Cards of {owner} ---------\n");

            foreach (Card card in cards)
            {
                ShowCard(card);
            }

            Console.WriteLine();
            Console.WriteLine($"Sum of cards: {CalculatePoints(cards)}\n");
        }

        private int CalculatePoints(List<Card> cards)
        {
            List<Card> sortedCards = new List<Card>(cards);
            sortedCards.Sort();

            int points = 0;
            int index = 0;

            while (index < sortedCards.Count && sortedCards[index].Value != CardValue.Ace)
            {
                points += GetCardValue(sortedCards[index].Value);
                index++;
            }

            int aceAmount = sortedCards.Count - index;

            if (aceAmount > 0)
            {
                int pointsFromAce = GetCardValue(CardValue.Ace) + aceAmount - 1;

                if (points + pointsFromAce > MAX_POINTS_VALUE)
                {
                    pointsFromAce -= GetCardValue(CardValue.Ace) - 1;
                }

                points += pointsFromAce;
            }

            //foreach (Card card in sortedCards)
            //{
            //    if (card.Value == CardValue.Ace)
            //    {
            //        if (points + (int)CardValue.Ace > MAX_POINTS_VALUE)
            //        {
            //            points++;
            //        }
            //        else
            //        {
            //            points += (int)CardValue.Ace;
            //        }
            //    }
            //    else
            //    {
            //        points += (int)card.Value;
            //    }
            //}

            return points;
        }

        private void Shuffle()
        {
            Random random = new Random();

            for (int i = _cards.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);

                var temp = _cards[j];
                _cards[j] = _cards[i];
                _cards[i] = temp;
            }

            _deck = new Queue<Card>();

            for (int i = 0; i < _cards.Count; i++)
            {
                _deck.Enqueue(_cards[i]);
            }
        }
    }
}
