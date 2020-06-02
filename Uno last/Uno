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
        public List<Player> Players { get; set; }
        public List<Card> CardForMoving { get; set; }

        public Player activePlayer { get; set; }
        public bool Reversed { get; set; }


        public delegate void ShowInfo(string message);
        private ShowInfo ShowMessage;
        public Card DeckCard { get; set; }
        public PictureBox pic { get; set; }

        public Action<Player> SelectPlayer { get; set; }
        public Action<List<Card>, Player> SelectCards { get; set; }


        public Uno(CardSet commonDeck, params Player[] players)
        {
            CommonDeck = commonDeck;
            Players = new List<Player>(players);
        }

        //Method Refresh
        //отображает карты
        //выделяет активного игрока
        //? выделяет карты для хода


        //Method NextPlayer (Player player)
        public Player NextPlayer(Player player)
        {
            int i = 0;
            for (i = 0; i <= Players.Count; i++)
            {
                if (Players[i]==player)
                {
                    if (i==Players.Count)
                    {
                        i = 0;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return Players[i++];
        }

        // Method PreviousPlayer
        public Player PreviousPlayer(Player player)
        {
            int i = 0;
            for (i = 0; i <= Players.Count; i++)
            {
                if (Players[i] == player)
                {
                    if (i == 0)
                    {
                        i = Players.Count;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return Players[i--];
        }

        // Method NextMover
        // или след. или пред.

        public void RegisterHandler(ShowInfo showInfo)
        {
            //gegree
            ShowMessage = showInfo;
        }

        public void Move(Card card)
        {
            if (activePlayer.Cards.Cards.Count>0)
            {
                GetCardsForMoving();
                if (CardForMoving.Count>0)
                {
                    CardSet cardformoving = new CardSet(CardForMoving);
                    if (cardformoving.Pull().Kinds==KindsOfCards.reverse)
                    {
                        DeckCard = cardformoving.Pull();
                        if (Reversed)
                        {
                            Reversed = false;
                            activePlayer = NextPlayer(activePlayer);
                        }
                        else
                        {
                            Reversed = true;
                            activePlayer = NextPlayer(activePlayer);
                        }
                    }
                    else if (cardformoving.Pull().Kinds == KindsOfCards.skip)
                    {
                        DeckCard = cardformoving.Pull();
                        activePlayer = NextPlayer(NextPlayer(activePlayer));
                    }
                    else if (cardformoving.Pull().Kinds == KindsOfCards.add2)
                    {
                        DeckCard = cardformoving.Pull();
                        NextPlayer(activePlayer).Cards.Add(CommonDeck.Deal(2));
                        activePlayer = NextPlayer(activePlayer);
                    }
                    else
                    {
                        DeckCard = cardformoving.Pull();
                        activePlayer = NextPlayer(activePlayer);
                    }
                }
            }
        }

        private List<Card> GetCardsForMoving()
        {
            CardForMoving = new List<Card>();
            foreach (Card card in activePlayer.Cards.Cards)
            {
                if (card.Colour == DeckCard.Colour || card.Kinds == DeckCard.Kinds)
                    CardForMoving.Add(card);
            }
            return CardForMoving;

        }

        public void Start()
        {
            Random r = new Random();
            CommonDeck.Mix();
            CardSet cards = new CardSet(52);


            foreach (var player in Players)
            {
                player.Cards.Add(CommonDeck.Deal(7));
            }

            DeckCard = CommonDeck.Pull();
            Reversed = false;
            activePlayer = Players[0];
            SelectPlayer(activePlayer);
            SelectCards(GetCardsForMoving(), activePlayer);
        }
    }
} 
