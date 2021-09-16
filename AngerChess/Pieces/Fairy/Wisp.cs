using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Wisp : Surround
    {
        public Wisp(int color):base(color)
        {
            name = "Wisp";
            moveDist = 2;
        }
        protected override void appendMovesDir(int dir, List<Move> moves)
        {
            //move normally on diagonals
            if (dir > 3) {
                base.appendMovesDir(dir, moves);
                return;
            }
            //ortho move dist 2, range 2 is leaping shoot instead if square occupied
            Square targetSquare = square.connectedSquares[dir];
            if (targetSquare == null) return;
            if(targetSquare.canAttackBy(this))
            {
                Capture capture = new Capture(this, targetSquare, targetSquare.piece);
                moves.Add(capture);
            }
            Square shootSquare = targetSquare.connectedSquares[dir];
            if (shootSquare == null) return;
            if(shootSquare.canAttackBy(this))
            {
                Shoot shoot = new Shoot(this, shootSquare, shootSquare.piece);
                moves.Add(shoot);
            } else if(targetSquare.canPass(this) && shootSquare.canMove(this))
            {
                Travel travel = new Travel(this, shootSquare);
                moves.Add(travel);
            }
        }

        public override void capture(Capture capture)
        {
            capture.capturedPiece.captureBy(capture);
        }
        public override void undoCapture(Capture capture)
        {
            capture.capturedPiece.revive();
        }

    }
}
