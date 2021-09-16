using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class King : Surround
    {
        //A king
        public King(int color) : base(color)
        {
            moveDist = 1;
            name = "King";
            bitIdx = 5;
            immune = true;
        }
        public override List<Move> getMoves()
        {
            //get basic moves
            List<Move> moves = base.getMoves();
            bool shroomed = square.shroomed[color] > 0;
            if (!shroomed) return moves;
            //add additional Travel moves if shroomed
            for (int i = 0; i < 8; i++)
            {
                Square tSquare = square;
                for(int j = 0; j < 2; j++)
                {
                    tSquare = square.connectedSquares[i];
                    if (tSquare == null) goto cont;
                }
                if(tSquare.canMove(this))
                {
                    moves.Add(new Travel(this, tSquare));
                }

            cont:;
            }
            return moves;
        }
        public override void captureBy(Capture capture)
        {
            square.win((color + 1) % 2);
        }
        public override void revive()
        {
            base.revive();
            square.undoWin();
        }
    }
}
