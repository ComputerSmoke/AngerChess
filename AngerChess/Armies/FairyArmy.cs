using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class FairyArmy : Army
    {
        public FairyArmy(int color) : base(color)
        {
            name = "fairy";
            loadPieces();
            linkPieces();
        }
        private void loadPieces()
        {
            for (int i = 0; i < 8; i++)
            {
                pieces[i] = new PawnFairy(color);
            }
            pieces[8] = new Shroom(color);
            pieces[9] = new Frog(color);
            pieces[10] = new Pixie(color);
            pieces[11] = new Wisp(color);
            pieces[12] = new King(color);
            pieces[13] = new Pixie(color);
            pieces[14] = new Frog(color);
            pieces[15] = new Shroom(color);
        }
    }
}
