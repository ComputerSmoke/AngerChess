using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Egg : Ortho
    {
        //the snake that will be spawned when egg captured
        public Snake baby;
        //spawn event that will be constructed when piece captured
        public Spawn spawnEvent;
        public Egg(int color) : base(color)
        {
            name = "Egg";
            moveDist = 3;
            baby = new Snake(color);
        }
        public override void captureBy(Capture capture)
        {
            Square behind = square.connectedSquares[2 * color];
            if (behind.piece == null) makeSnake(behind);
            base.captureBy(capture);
        }
        private void makeSnake(Square behind)
        {
            spawnEvent = new Spawn(this, behind, baby);
            spawnEvent.execute();
        }

        public override void revive()
        {
            if(spawnEvent != null) spawnEvent.undo();
            spawnEvent = null; 
        }

        public override void linkPiece(Piece piece)
        {
            base.linkPiece(piece);
            baby.linkPiece(((Egg)piece).baby);
        }
    }
}
