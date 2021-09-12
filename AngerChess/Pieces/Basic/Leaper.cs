using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    abstract class Leaper : Piece
    {
        public Leaper(int color) : base(color)
        {
            bitIdx = 2;
        }
        public override List<Move> getMoves()
        {
            List<Move> moves = new List<Move>();
            for(int dir = 8; dir < 16; dir++)
            {
                Square targetSquare = square.connectedSquares[dir];
                if (targetSquare == null) continue;
                if (targetSquare.canAttack(this))
                {
                    Capture capture = new Capture(this, targetSquare, targetSquare.piece);
                    moves.Add(capture);
                } else if(targetSquare.canMove(this))
                {
                    Move move = new Move(this, targetSquare);
                    moves.Add(move);
                }
            }
            return moves;
        }
    }
}
