using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Сards;

namespace Cards
{
    class GraphicCard : Card
    {
        public PictureBox Pb { get; set; }
        public bool Opened
        {
            get
            {
                return opened;
            }
            set
            {
                opened = value;
                Pb.Image = opened ? Image.FromFile(fileName) : Image.FromFile(imageShirtPath);
            }
        }

        private bool opened;
        private readonly string imageShirtPath = Application.StartupPath + @"\images\shirt.jpg";
        private readonly string fileName;

        public GraphicCard(KindsOfCards kinds, CardColour colour, PictureBox pb, bool opened = true) : base(colour,kinds)
        {
            Pb = pb;
            Pb.SizeMode = PictureBoxSizeMode.Zoom;
            Pb.Visible = false;
            fileName = Application.StartupPath + @"\images\" + this.ToString() + ".png";
            Opened = opened;
        }

        public GraphicCard(KindsOfCards kinds, CardColour colour) : this(kinds, colour, new PictureBox()) { }

        public GraphicCard(CardColour colour, KindsOfCards kinds) : base(colour, kinds)
        {
        }

        public override void Show()
        {
            Pb.Visible = true;
        }

        public override string ToString()
        {
            return String.Format($"{Kinds}s {Colour}");
        }

    }
}
