using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Сards
{
    class Uno
    {
        public CardSet CommonDeck { get; set; }
        public CardSet Table { get; set; }
        public List<Player> Players { get; set; }
        public List<Card> CardForMoving { get; set; }

        public Player activePlayer { get; set; }
        public CardColour activeColor { get; set; }
        public bool Reversed { get; set; }
        public Card DeckCard { get; set; }

        public Action<Player> SelectPlayer { get; set; }
        public Action<List<Card>, Player> SelectCards { get; set; }
        public Action<string> ShowMessage { get; set; }


        public Uno(CardSet commonDeck, CardSet table, params Player[] players)
        {
            CommonDeck = commonDeck;
            Table = table;
            Players = new List<Player>(players);
        }

        //Method Refresh
        //отображает карты
        //выделяет активного игрока
        //? выделяет карты для хода


        //Method NextPlayer (Player player)
        public Player NextPlayer(Player player)
        {
            if (!Reversed)
            {
                int index = Players.IndexOf(player);
                if (index == Players.Count - 1)
                    return Players[0];
                else
                    return Players[index + 1];
            }
            else
            {
                return PreviousPlayer(player);
            }
        }

        // Method PreviousPlayer
        public Player PreviousPlayer(Player player)
        {
            for (int i = 0; i <= Players.Count; i++)
            {
                if (Players[i] == player)
                {
                    if (i == 0)
                    {
                        return Players[Players.Count - 1];
                    }
                    else
                    {
                        return Players[i - 1];
                    }
                }
            }
            throw new Exception("We dont have this player");
        }

        // Method NextMover
        // или след. или пред.

        public void Move(Card card)
        {
            Move(card, card.Colour);
        }

        public void Move(Card card, CardColour colour)
        {
            if(activePlayer.Cards.Cards.IndexOf(card)==-1)
            {
                Fail("ActivePlayer don't have this card");
                return;
            }

            if(!IsCorrect(DeckCard,card))
            {
                Fail("This card can't been put now");
                return;
            }

            DeckCard = card;
            activeColor = colour;

            if (DeckCard.Kinds == KindsOfCards.reverse)
            {
                Reversed = !Reversed;
            }
                                    
            if (DeckCard.Kinds == KindsOfCards.add2)
            {
                NextPlayer(activePlayer).Cards.Add(CommonDeck.Deal(2));
            }

            activePlayer = DeckCard.Kinds == KindsOfCards.skip ? 
                NextPlayer(NextPlayer(activePlayer)) : 
                NextPlayer(activePlayer);
            SelectPlayer(activePlayer);

            Refresh();

        }

        public void Refresh()
        {

            foreach (var player in Players)
            {
                player.Cards.Show();
            }
            Table.Cards.Clear();
            Table.Add(DeckCard);
            Table.Show();

        }

        private void Fail(string message)
        {
            ShowMessage(message);
        }

        private List<Card> GetCardsForMoving()
        {
            CardForMoving = new List<Card>();
            foreach (Card card in activePlayer.Cards.Cards)
            {
                if (IsCorrect(DeckCard, card))
                    CardForMoving.Add(card);
            }
            return CardForMoving;

        }

        private bool IsCorrect(Card toCard, Card card)
        {
            if (card.Colour == activeColor) return true;
            
            if (card.Kinds == toCard.Kinds) return true;

            if (card.Colour == CardColour.black) return true;

            return false;
        }

        public void Start()
        {
            CommonDeck.Mix();

            foreach (var player in Players)
            {
                player.Cards.Add(CommonDeck.Deal(7));
            }

            DeckCard = CommonDeck.Pull();
            activeColor = DeckCard.Colour;
            Reversed = false;
            activePlayer = Players[0];
            SelectPlayer(activePlayer);
            SelectCards(GetCardsForMoving(), activePlayer);
        }
    }
} 
