using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Drake : Leaper
    {
        bool powered;
        public Drake(int color) : base(color)
        {
            name = "Drake";
            powered = false;
        }
        public override List<Move> getMoves()
        {
            List<Move> moves = base.getMoves();
            if (!powered) return moves;
            //Add 3,1 leaps 
            for(int i = 0; i < 4; i++)
            {
                Square squarei = square.connectedSquares[i];
                if (squarei == null) continue;
                for(int j = 0; j < 2; j++)
                {
                    int dir = (15 + (i * 2) + j) % 16 + 8;
                    Square squaret = squarei.connectedSquares[dir];
                    if (squaret == null) continue;
                    appendMove(moves, squaret);
                }
            }
            return moves;
        }
        public override void captureBy(Capture capture)
        {
            base.captureBy(capture);
            ((Drake)samePieces[0]).powered = true;
        }
        public override void revive()
        {
            base.revive();
            ((Drake)samePieces[0]).powered = false;
        }
    }
}
