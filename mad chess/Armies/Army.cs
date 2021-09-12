﻿using System;
using System.Collections.Generic;

namespace MadChess
{
	abstract class Army
	{
        protected Piece[] pieces;
        public int color;
        public int bitCount;
        public string name;
        protected Army(int color)
        {
            this.color = color;
            bitCount = 6;
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
        }
	}
}
