using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Rook : Ortho
    {
        public Rook(int color) : base(color)
        {
            moveDist = 8;
            name = "Rook";
        }
    }
}
