﻿using System;
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

        protected override Piece makePromotionPiece()
        {
            return new Queen(color);
        }
    }
}
