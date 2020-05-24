﻿using System;
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
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public delegate void ShowInfo(string message);
        private ShowInfo ShowMessage;
        public Card DeckCard { get; set; }
        public PictureBox pic { get; set; }

        public Uno(CardSet commonDeck, Player player1, Player player2)
        {
            CommonDeck = commonDeck;
            Player1 = player1;
            Player2 = player2;
        }

        public void RegisterHandler(ShowInfo showInfo)
        {
            //gegree
            ShowMessage = showInfo;
        }

        public void Move(Player player1,Player player2)
        {
            player1.Cards.Show();
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

        public void Start()
        {
            Random r = new Random();
            CommonDeck.Mix();
            CardSet cards = new CardSet(52);
            int a = r.Next(cards.Cards.Count);
            int b = r.Next(cards.Cards.Count);
            Player1.Cards.Deal(7);
            cards.Cards.RemoveRange(cards.Cards.Count-7, cards.Cards.Count);
            Player2.Cards.Deal(7);
            cards.Cards.RemoveRange(cards.Cards.Count - 7, cards.Cards.Count);
            DeckCard = CommonDeck.Pull();
            cards.Cards.RemoveAt(0);
            while (Player1.Cards.Cards.Count!=0&&Player2.Cards.Cards.Count!=0)
            {
                Move(Player1, Player2);
                if (Player1.Cards.Cards.Count ==0)
                {
                    ShowMessage(String.Format("Player {0} win!", Player1.Name));
                }
                Move(Player2, Player1);
                if (Player2.Cards.Cards.Count==0)
                {
                    ShowMessage(String.Format("Player {0} win!", Player2.Name));
                }
            }
        }
    }
}
