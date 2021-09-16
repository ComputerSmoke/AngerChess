using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Wyvern : Diag
    {
        Capture revenge;
        public Wyvern(int color) : base(color)
        {
            name = "Wyvern";
            moveDist = 3;
        }
        public override void captureBy(Capture capture)
        {
            if(!capture.piece.immune && !capture.revenge && Math.Abs(capture.fromSquare.row-square.row) == 1 && Math.Abs(capture.fromSquare.col - square.col) < 1)
            {
                revenge = new Capture(this, square, capture.piece);
                capture.revenge = true;
                revenge.execute();
            }
            base.captureBy(capture);
        }
        public override void revive()
        {
            base.revive();
            if (revenge == null) return;
            //put back revenged piece if applicable
            revenge.undo();
            revenge = null;
        }
    }
}
