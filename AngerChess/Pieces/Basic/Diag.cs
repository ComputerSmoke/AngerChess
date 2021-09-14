﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    abstract class Diag : Piece
    {
        //A minor piece that moves diagonally
        public Diag(int color) : base(color)
        {
            bitIdx = 3;
        }
        public override List<Move> getMoves()
        {
            List<Move> moves = new List<Move>();
            for (int dir = 4; dir < 8; dir++)
            {
                Square targetSquare = square;
                for (int i = 0; i < moveDist; i++)
                {
                    targetSquare = targetSquare.connectedSquares[dir];
                    if (targetSquare == null) break;
                    if(targetSquare.canAttack(this))
                    {
                        Capture capture = new Capture(this, targetSquare, targetSquare.piece);
                        moves.Add(capture);
                    } else if (targetSquare.canMove(this))
                    {
                        Travel travel = new Travel(this, targetSquare);
                        moves.Add(travel);
                    }
                    if (!targetSquare.canPass(this)) break;
                }
            }
            return moves;
        }
    }
}
