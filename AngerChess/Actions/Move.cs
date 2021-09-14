using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    abstract class Move
    {
        protected Piece piece;
        protected Square prevEnPassant;
        protected Piece prevEnPassantPiece;
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
            //square piece is moving from is the square it is currently on
            fromSquare = piece.square;
        }

        public virtual void execute()
        {
            //store previous enpassant state for undo
            prevEnPassant = piece.square.board.enPassant;
            prevEnPassantPiece = piece.square.board.enPassantPiece;
            //clear enpassant vulnerability
            piece.square.board.enPassant = null;
        }
        public virtual void undo()
        {
            //restore previous en passant state
            piece.square.board.enPassant = prevEnPassant;
            piece.square.board.enPassantPiece = prevEnPassantPiece;
        }
    }
}
