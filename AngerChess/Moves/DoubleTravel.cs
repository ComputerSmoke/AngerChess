using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class DoubleTravel : Travel
    {
        Piece prevEnPiece;
        Square prevEnSquare;
        public DoubleTravel(Piece piece, Square toSquare) : base(piece, toSquare) {
            this.piece = (Minion)this.piece;
        }
        public override void execute()
        {
            base.execute();
            prevEnPiece = piece.square.board.enPassantPiece;
            //if double moved, create enPassant vulnerability
            if (((Minion)piece).canBeEnPassant)
            {
                piece.square.board.enPassant = piece.square.connectedSquares[piece.color * 2];
                piece.square.board.enPassantPiece = piece;
            }
        }
        public override void undo()
        {
            piece.square.board.enPassantPiece = prevEnPiece;
            base.undo();
        }
    }
}
