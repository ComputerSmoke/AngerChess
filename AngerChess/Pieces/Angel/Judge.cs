using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Judge : Ortho
    {
        Square overlapGuard;
        public Judge(int color): base(color)
        {
            name = "Judge";
            moveDist = 5;
            overlapGuard = null;
        }

        protected override Square nextSquare(Square prevSquare, int dir)
        {
            //default behavior on up/down or if next square is not edge
            if (dir == 0 || dir == 2) return base.nextSquare(prevSquare, dir);
            //wrap across edge if headed off edge horizontally
            if (dir == 3)
            {
                //if moving left, track the last square visited. We will use this to avoid overlapping when scanning right.
                if (prevSquare.connectedSquares[dir] == null)
                {
                    overlapGuard = prevSquare.board.squares[prevSquare.row, prevSquare.board.squares.GetLength(0) - 1];
                }
                else
                {
                    overlapGuard = base.nextSquare(prevSquare, dir);
                }
                return overlapGuard;
            }
            if (dir == 1)
            {
                //get next square to right
                Square next;
                if (prevSquare.connectedSquares[dir] == null) next = prevSquare.board.squares[prevSquare.row, 0];
                else next = base.nextSquare(prevSquare, dir);
                //if this has been visited when scanning left, return null.
                if (next == overlapGuard) return null;
                //return this square otherwise
                return next;
            }
            //if we somehow got some other direction, return null.
            return null;
        }
    }
}
