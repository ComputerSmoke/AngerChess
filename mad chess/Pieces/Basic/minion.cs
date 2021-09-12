using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    abstract class Minion : Piece
    {
        protected bool canDouble;
        public Minion(int color) : base(color)
        {
            bitIdx = 0;
        }
        public override List<Move> getMoves()
        {
            List<Move> moves = new List<Move>();
            int moveDir = 2 - 2 * color;
            int[] atkDirs = new int[2];
            if(color == 1)
            {
                atkDirs[0] = 4;
                atkDirs[1] = 7;
            } else
            {
                atkDirs[0] = 5;
                atkDirs[1] = 6;
            }
            Square moveSquare = square.connectedSquares[moveDir];
            Square[] atkSquares = new Square[2];
            for(int i = 0; i < atkSquares.GetLength(0); i++)
            {
                atkSquares[i] = square.connectedSquares[atkDirs[i]];
            }
            if (moveSquare.canMove(this))
            {
                Move move = new Move(this, moveSquare);
                moves.Add(move);
            }
            if(canDouble && moveSquare.canPass(this) && moveSquare.connectedSquares[moveDir].canMove(this))
            {
                Move move = new Move(this, moveSquare.connectedSquares[moveDir]);
                moves.Add(move);
            }
            foreach(Square atkSquare in atkSquares)
            {
                if (atkSquare == null) continue;
                if(atkSquare.canAttack(this))
                {
                    Capture capture = new Capture(this, atkSquare, atkSquare.piece);
                    moves.Add(capture);
                }
            }

            return moves;
        }

        public override void move(Move move)
        {
            base.move(move);
            canDouble = false;
            if (square.row == 7 * ((color + 1) % 2)) promote();
        }

        public virtual void promote()
        {
        }
    }
}
