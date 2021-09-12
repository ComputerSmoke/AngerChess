using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Bishop : Diag
    {
        public Bishop(int color) : base(color)
        {
            moveDist = 8;
            name = "Bishop";
        }
    }
}
