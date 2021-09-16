using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class PawnDragon : Minion
    {
        public PawnDragon(int color) : base(color) {
            canDouble = true;
            name = "Pawn";
        }
        protected override Piece makePromotionPiece()
        {
            return new Egg(color);
        }
    }
}
