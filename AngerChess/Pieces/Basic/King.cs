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
