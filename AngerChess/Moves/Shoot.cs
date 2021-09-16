﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Shoot : Capture
    {
        public Shoot(Piece piece, Square toSquare, Piece capturedPiece):base(piece, toSquare, capturedPiece)
        {

        }
        public void execute()
        {
            piece.shoot(this);
        }
        public void undo()
        {
            piece.shoot(this);
        }
    }
}
