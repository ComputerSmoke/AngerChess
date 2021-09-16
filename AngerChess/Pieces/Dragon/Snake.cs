using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Snake : Diag
    {
        public Snake(int color):base(color)
        {
            name = "Snake";
            moveDist = 2;
            bitIdx = 7;
        }
    }
}
