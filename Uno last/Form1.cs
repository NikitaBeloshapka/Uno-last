using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Сards
{
    public partial class Form1 : Form
    {
        Card activeCard;
        Uno game;
        Player mover;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<Player> players = new List<Player> { new Player("Bob", new GraphicCardSet(panel1)), 
                new Player("Tom", new GraphicCardSet(panel2)),
                new Player("Jack", new GraphicCardSet(panel4))};
            game = new Uno(new GraphicCardSet(panel3, CardSetType.Full), new GraphicCardSet(pnlTable), players.ToArray());

            foreach (var card in game.CommonDeck.Cards)
            {
                PictureBox cardPictureBox = ((GraphicCard)card).Pb;
                PictureBox TablePictureBox = ((GraphicCard)card).Pb;
                cardPictureBox.Click += CardPictureBox_Click;
                TablePictureBox.Click += TablePictureBox_Click;
            }

            game.ShowMessage = ShowMessage;
            game.SelectPlayer = selectPlayer;
            game.SelectCards = SelectCards;
            game.PlayerWin = Win;

            game.Start();
        }

        private void Win(Player winner)
        {
            MessageBox.Show($"Player {winner.Name} has won");
            this.Close();
        }

        private void SelectCards(List<Card> cards, Player player)
        {
            //выделить карты
        }

        private void CardPictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            SetActiveCard(pictureBox);
        }
        private void TablePictureBox_Click(object sender, EventArgs e)
        {
            if (activeCard != null && mover != null)
                game.Move(activeCard);
        }

        private void ShowMessage(string message)
        {
            label1.Text = message;
        }

        private void selectPlayer(Player activePlayer)
        {
            foreach (var player in game.Players)
            {
                if (player == activePlayer)
                    foreach (var card in player.Cards.Cards)
                    {
                        GraphicCard graphicCard = (GraphicCard)card;
                        graphicCard.Opened = true;
                    }
                else
                    foreach (var card in player.Cards.Cards)
                    {
                        GraphicCard graphicCard = (GraphicCard)card;
                        graphicCard.Opened = false;
                    }

            }
            game.Refresh();

        }



        private void SetActiveCard(PictureBox pictureBox)
        {
            foreach (var player in game.Players)
            {
                foreach (var card in player.Cards.Cards)
                {
                    if (((GraphicCard)card).Pb == pictureBox)
                    {
                        if (card == activeCard)
                        {
                            activeCard = null;
                            pictureBox.Top -= 10;
                            mover = null;
                        }
                        else
                        {
                            activeCard = card;
                            pictureBox.Top += 10;
                            mover = player;
                        }


                        return;
                    }
                }
            }
        }

        private void pnlTable_Click(object sender, EventArgs e)
        {
               
        }
         private void button2_Click_1(object sender, EventArgs e)
        {
            
        }
        private void pnlTable_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void pnlTable_MouseMove_1(object sender, MouseEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            game.TakeOneCard();
            button2.Text = game.giveMove ? "Give move" : "Take card";
        }
    }
}
