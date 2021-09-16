using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class PawnFairy : Minion
    {
        public PawnFairy(int color) : base(color)
        {
            canDouble = true;
            name = "Pawn";
        }

        protected override Piece makePromotionPiece()
        {
            return new Shroom(color);
        }
    }
}
