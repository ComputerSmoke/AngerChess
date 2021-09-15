using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Travel : Move
    {
        public Travel(Piece piece, Square toSquare) : base(piece, toSquare)
        {
        }
        public override void execute()
        {
            base.execute();
            piece.move(this);
        }
        public override void undo()
        {
            base.undo();
            piece.undoMove(this);
        }
    }
}
