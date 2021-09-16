using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Wisp : Surround
    {
        public Wisp(int color):base(color)
        {
            name = "Wisp";
            moveDist = 2;
        }
        public override List<Move> getMoves()
        {
            List<Move> moves = new List<Move>();
            //move normally on diagonalls
            for(int i = 4; i < 8; i++)
            {
                appendMovesDir(i, moves);
            }
            //can pass other pieces on orthogonals
            canLeap = true;
            for(int idx = 0; idx < 4; idx++)
            {
                appendMovesDir(idx, moves);
            }
            canLeap = false;
            return moves;
        }

        public override void capture(Capture capture)
        {
            capture.capturedPiece.captureBy(capture);
        }
        public override void undoCapture(Capture capture)
        {
            capture.capturedPiece.revive();
        }

    }
}
