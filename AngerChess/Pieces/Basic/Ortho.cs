using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    abstract class Ortho : Piece
    {
        public Ortho(int color) : base(color)
        {
            bitIdx = 1;
        }
        public override List<Move> getMoves()
        {
            List<Move> moves = new List<Move>();
            for (int dir = 0; dir < 4; dir++)
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
