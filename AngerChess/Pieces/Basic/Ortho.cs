using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    abstract class Ortho : Surround
    {
        //A minor piece that moves orthogonally
        public Ortho(int color) : base(color)
        {
            bitIdx = 1;
        }
        public override List<Move> getMoves()
        {
            List<Move> moves = new List<Move>();
            //Move orthogonally
            for (int dir = 0; dir < 4; dir++) appendMovesDir(dir, moves);
            return moves;
        }
    }
}
