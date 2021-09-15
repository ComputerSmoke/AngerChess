using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    abstract class Diag : Surround
    {
        //A minor piece that moves diagonally
        public Diag(int color) : base(color)
        {
            bitIdx = 3;
        }
        public override List<Move> getMoves()
        {
            List<Move> moves = new List<Move>();
            //Move in a straight line diagonally
            for (int dir = 4; dir < 8; dir++) appendMovesDir(dir, moves);
            return moves;
        }
    }
}
