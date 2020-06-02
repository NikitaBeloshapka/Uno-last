using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Сards
{
    class GraphicUno : Uno
    {
        public GraphicUno(Panel[]playersDecks, Panel otherDeck,CardSet commonDeck,params Player[]players) : base(commonDeck, players)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].Cards = new GraphicCardSet(playersDecks[i]);
            }
        }
    }
}
