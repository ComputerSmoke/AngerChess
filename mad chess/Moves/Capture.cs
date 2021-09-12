﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Capture : Move
    {
        public Piece capturedPiece
        {
            get;
        }
        public Capture(Piece piece, Square toSquare, Piece capturedPiece) : base(piece, toSquare)
        {
            this.capturedPiece = capturedPiece;
        }
        public override void execute()
        {
            piece.capture(this);
        }
        public override void undo()
        {
            piece.undoCapture(this);
        }
    }
}
