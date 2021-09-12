using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Pawn : Minion
    {
        public Pawn(int color) : base(color)
        {
            canDouble = true;
            name = "Pawn";
        }

        public override void promote()
        {
            Queen queen = new Queen(color);
            queen.square = square;
            square.piece = queen;
        }
    }
}
