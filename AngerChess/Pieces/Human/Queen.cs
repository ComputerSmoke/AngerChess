using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Queen : Surround
    {
        public Queen(int color) : base(color)
        {
            moveDist = 8;
            name = "Queen";
        }
    }
}
