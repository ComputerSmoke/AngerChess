using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Zealot : Diag
    {
        //direction to bounce - first index is previous travel, second index is 0 if hitting top/bottom of board, 1 otherwise.
        static int[,] bounceDirs = new int[,] { { 5, 7 }, { 4, 6 }, { 7, 5 }, { 6, 4 } };
        //what corners not to bounce which direction in
        static int[,] noBounce = new int[,] { { 0, 1 }, { 1, 1 }, { 1, 0 }, { 0, 0 } };
        int newDir;
        public Zealot(int color) : base(color)
        {
            moveDist = 5;
            name = "Zealot";
            newDir = 0;
        }
        protected override void appendMovesDir(int dir, List<Move> moves)
        {
            Square targetSquare = square;
            for (int i = 0; i < moveDist; i++)
            {
                newDir = dir;
                //have bounce option for move dists past first, but never bounce off a wall we are touching directly
                if (i > 0) targetSquare = nextSquare(targetSquare, dir);
                else targetSquare = base.nextSquare(targetSquare, dir);
                //change dir if we bounced
                dir = newDir;
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
        protected override Square nextSquare(Square prevSquare, int dir)
        {
            if (prevSquare.connectedSquares[dir] != null) return base.nextSquare(prevSquare, dir);
            //calculate what we're hitting otherwise
            int rows = prevSquare.board.squares.GetLength(0);
            int cols = prevSquare.board.squares.GetLength(1);
            int col = prevSquare.col;
            int row = prevSquare.row;
            bool hittingSide = col == 0 || col == cols - 1;
            //do not bounce off corner
            if (col == noBounce[dir-4, hittingSide?1:0]*(cols-1) && row == noBounce[dir-4, hittingSide?1:0]*(rows-1))
            {
                Console.WriteLine("Hitting corner");
                return null;
            }
            //bounce otherwise
            newDir = bounceDirs[dir - 4, hittingSide ? 1 : 0];
            return base.nextSquare(prevSquare, newDir);
        }
    }
}
