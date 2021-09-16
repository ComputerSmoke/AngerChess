using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class PawnAngel : Minion
    {
        public PawnAngel(int color) : base(color)
        {
            canDouble = true;
            name = "Pawn";
        }

        protected override Piece makePromotionPiece()
        {
            return new Judge(color);
        }
    }
}
