using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class AngelArmy : Army
    {
        public AngelArmy(int color):base(color)
        {
            name = "angel";
            loadPieces();
        }
        private void loadPieces()
        {
            for (int i = 0; i < 8; i++)
            {
                pieces[i] = new PawnAngel(color);
            }
            pieces[8] = new Judge(color);
            pieces[9] = new Throne(color);
            pieces[10] = new Zealot(color);
            pieces[11] = new Shield(color);
            pieces[12] = new King(color);
            pieces[13] = new Zealot(color);
            pieces[14] = new Throne(color);
            pieces[15] = new Judge(color);
        }
    }
}
