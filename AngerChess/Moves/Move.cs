using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Move
    {
        protected Piece piece;
        public Square fromSquare
        {
            get;
        }
        public Square toSquare
        {
            get;
        }
        public Move(Piece piece, Square toSquare)
        {
            this.piece = piece;
            this.toSquare = toSquare;
            fromSquare = piece.square;
        }
        public virtual void execute()
        {
            piece.move(this);
        }
        public virtual void undo()
        {
            piece.undoMove(this);
        }
    }
}
