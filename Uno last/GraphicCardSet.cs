﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Сards
{
    class GraphicCardSet : CardSet
    {
        public Panel Panel { get; set; }
        public GraphicCardSet(Panel panel, CardSetType cardSetType = CardSetType.Empty) : base(cardSetType)
        {
            Panel = panel;
        }
        public override Card GetCard(CardColour colour, KindsOfCards kind)
        {
            return new GraphicCard(colour, kind);
        }
        public override void Show()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                GraphicCard graphicCard = (GraphicCard)Cards[i];
                PictureBox pb = graphicCard.Pb;
                Panel.Controls.Add(pb);
                pb.BringToFront();
                pb.Location = new Point(i * 50, 0);
                pb.Size = new Size(Panel.Width/Cards.Count, Panel.Height);
                pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                pb.TabIndex = i;
                pb.TabStop = false;

                graphicCard.Show();
            }

        }
    }
}
