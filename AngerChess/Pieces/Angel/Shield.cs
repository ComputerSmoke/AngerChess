using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Shield : Surround
    {
        public Shield(int color) : base(color)
        {
            moveDist = 3;
            name = "Shield";
        }
        public override bool canAttack(Square square)
        {
            //Shield cannot attack
            return false;
        }
        public override bool canAttackBy(Piece piece)
        {
            //Shield cannot be attacked
            return false;
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
            for (int dir = 1; dir <= 3; dir += 2)
            {
                if (square.connectedSquares[dir] == null) continue;
                square.connectedSquares[dir].invincible++;
            }
        }
        public void undoEffect()
        {
            for (int dir = 1; dir <= 3; dir += 2)
            {
                if (square.connectedSquares[dir] == null) continue;
                square.connectedSquares[dir].invincible--;
            }
        }
    }
}
