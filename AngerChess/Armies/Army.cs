using System;
using System.Collections.Generic;

namespace MadChess
{
	abstract class Army
	{
        protected Piece[] pieces;
        public int color;
        public string name;
        protected Army(int color)
        {
            this.color = color;
            pieces = new Piece[16];
        }
        public void place(Board board)
        {
            Square startSquare = board.squares[5 * color + 1, 0];
            Square square = startSquare;
            for(int i = 0; i < 8; i++)
            {
                square.piece = pieces[i];
                pieces[i].square = square;
                square = square.connectedSquares[1];
            }
            square = startSquare.connectedSquares[2 * color];
            for(int i = 0; i < 8; i++)
            {
                square.piece = pieces[i + 8];
                pieces[i+8].square = square;
                square = square.connectedSquares[1];
            }
            for(int i = 0; i < 16; i++)
            {
                pieces[i].moveEffect();
            }
        }
        //Link pieces of the same type for abilities such as shaman and drake.
        protected void linkPieces()
        {
            for(int i = 0; i < 3; i++)
            {
                pieces[i + 8].linkPiece(pieces[pieces.Length - i - 1]);
            }
        }
	}
}
