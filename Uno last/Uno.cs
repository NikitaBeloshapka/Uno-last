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

        public Player activePlayer { get; set; }
        public bool Revesed { get; set; }


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
        //если номер активного — последний, то вернуть первого игрока
        //иначе вернуть следующего по номеру

        // Method PreviousPlayer

        // Method NextMover
        // или след. или пред.

        public void RegisterHandler(ShowInfo showInfo)
        {
            //gegree
            ShowMessage = showInfo;
        }

        public void Move(Card card)
        {
            // проверить, есть ли карта у активного игрока

            // проверить, можно ли картой ходить

            // если можно
            DeckCard = activePlayer.Cards.Pull(card);
            //меняем активного игрока
            //обновляем стол
            

            int a = 0;
            foreach (Card card in player1.Cards.Cards)
            {
                Card card1 = CommonDeck.Pull();
                if (card.Kinds  !=KindsOfCards.add2&& card.Kinds != KindsOfCards.reverse && card.Kinds != KindsOfCards.skip)
                {
                    if (card.Colour ==DeckCard.Colour||card.Kinds ==DeckCard.Kinds)
                    {
                        pic.Top += 20;
                    }
                    else
                    {
                        a++;
                        if (a==player1.Cards.Cards.Count)
                        {
                            Player1.Cards.Add(card1);
                            CommonDeck.Cards.Remove(card1);
                            if (card1.Colour==DeckCard.Colour||card1.Kinds==DeckCard.Kinds)
                            {
                                DeckCard = card1;
                                player1.Cards.Cards.Remove(card1);
                            }
                            else
                            {
                                break;
                            }
                        }
                        
                    }
                }
               if (card.Kinds==KindsOfCards.skip|| card.Kinds == KindsOfCards.reverse)
                {
                    if (card.Colour == DeckCard.Colour||card.Kinds == DeckCard.Kinds)
                    {
                        pic.Top += 20;
                    }
                    else
                    {
                        a++;
                        if (a == player1.Cards.Cards.Count)
                        {
                            Player1.Cards.Add(card1);
                            CommonDeck.Cards.Remove(card1);
                            if (card1.Colour == DeckCard.Colour || card1.Kinds == DeckCard.Kinds)
                            {
                                DeckCard = card1;
                                player1.Cards.Cards.Remove(card1);
                            }
                            else
                            {
                                break;
                            }
                        }
                        
                    }

                }
                if (card.Kinds ==KindsOfCards.add2)
                {
                    if (card.Colour == DeckCard.Colour || card.Kinds == DeckCard.Kinds)
                    {
                        pic.Top += 20;
                    }
                    else
                    {
                        a++;
                        if (a == player1.Cards.Cards.Count)
                        {
                            Player1.Cards.Add(card1);
                            CommonDeck.Cards.Remove(card1);
                            if (card1.Colour == DeckCard.Colour || card1.Kinds == DeckCard.Kinds)
                            {
                                DeckCard = card1;
                                player1.Cards.Cards.Remove(card1);
                            }
                            else
                            {
                                return;
                            }
                        }
                        
                    }
                }

            }
        }

        private List<Card> GetCardsForMoving()
        {
            List<Card> cardForMoving = new List<Card>();
            foreach (Card card in activePlayer.Cards.Cards)
            {
                    if (card.Colour == DeckCard.Colour || card.Kinds == DeckCard.Kinds)                    
                        cardForMoving.Add(card);   
            }
            return cardForMoving;

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
            Revesed = false;
            activePlayer = Players[0];
            SelectPlayer(activePlayer);
            SelectCards(GetCardsForMoving(), activePlayer);
        }
    }
}
