using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    abstract class Surround : Piece
    {
        //A piece that moves both orthogonally and diagonally
        public Surround(int color) : base(color)
        {
            bitIdx = 4;
        }
        public override List<Move> getMoves()
        {
            List<Move> moves = new List<Move>();
            //Move in a straight line all 8 directions
            for (int dir = 0; dir < 8; dir++) appendMovesDir(dir, moves);
            return moves;
        }
        //append all moves in specified direction to provided move list
        protected virtual void appendMovesDir(int dir, List<Move> moves)
        {
            Square targetSquare = square;
            for (int i = 0; i < moveDist; i++)
            {
                targetSquare = nextSquare(targetSquare, dir);
                if (targetSquare == null) break;
                if (targetSquare.canAttackBy(this))
                {
                    Capture capture = new Capture(this, targetSquare, targetSquare.piece);
                    moves.Add(capture);
                }
                else if (targetSquare.canMove(this))
                {
                    Travel travel = new Travel(this, targetSquare);
                    moves.Add(travel);
                }
                if (!targetSquare.canPass(this)) break;
            }
        }
        protected virtual Square nextSquare(Square startSquare, int dir)
        {
            return startSquare.connectedSquares[dir];
        }
    }
}
