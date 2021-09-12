using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class King : Surround
    {
        public King(int color) : base(color)
        {
            moveDist = 1;
            name = "King";
            bitIdx = 5;
        }
        public override void capture()
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
