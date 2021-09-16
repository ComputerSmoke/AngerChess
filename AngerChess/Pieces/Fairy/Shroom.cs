using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Shroom : Ortho
    {
        public Shroom(int color) : base(color)
        {
            name = "Shroom";
            moveDist = 4;
        }
        public override void captureBy(Capture capture)
        {
            undoEffect();
            base.captureBy(capture);
        }
        public override void move(Move move)
        {
            //make prev adjacent squares not invincible
            undoEffect();
            base.move(move);
            moveEffect();
        }
        public override void undo(Move move)
        {
            undoEffect();
            base.undo(move);
            moveEffect();
        }
        public override void moveEffect()
        {
            for (int dir = 0; dir < 7; dir++)
            {
                if (square.connectedSquares[dir] == null) continue;
                square.connectedSquares[dir].shroomed[color]++;
            }
        }
        public void undoEffect()
        {
            for (int dir = 0; dir < 7; dir++)
            {
                if (square.connectedSquares[dir] == null) continue;
                square.connectedSquares[dir].shroomed[color]--;
            }
        }
    }
}
