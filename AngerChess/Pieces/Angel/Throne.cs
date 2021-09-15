using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Throne : Leaper
    {
        public Throne(int color) : base(color) {
            name = "Throne";
        }
        public override bool canPass(Piece piece)
        {
            //friendly pieces can move through the throne
            if (piece.color == color) return true;
            return base.canPass(piece);
        }
    }
}
