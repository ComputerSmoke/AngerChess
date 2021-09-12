﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    abstract class Surround : Ortho
    {
        public Surround(int color) : base(color)
        {
            bitIdx = 4;
        }
        public override List<Move> getMoves()
        {
            List<Move> moves = base.getMoves();
            for (int dir = 4; dir < 8; dir++)
            {
                Square targetSquare = square;
                for (int i = 0; i < moveDist; i++)
                {
                    targetSquare = targetSquare.connectedSquares[dir];
                    if (targetSquare == null) break;
                    if (targetSquare.canAttack(this))
                    {
                        Capture capture = new Capture(this, targetSquare, targetSquare.piece);
                        moves.Add(capture);
                    }
                    else if (targetSquare.canMove(this))
                    {
                        Move move = new Move(this, targetSquare);
                        moves.Add(move);
                    }
                    if (!targetSquare.canPass(this)) break;
                }
            }
            return moves;
        }
    }
}
