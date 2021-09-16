using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Pixie : Diag
    {
        public Pixie(int color):base(color)
        {
            name = "Pixie";
            moveDist = 3;
        }
        protected override void appendMovesDir(int dir, List<Move> moves)
        {
            Square targetSquare = square;
            for (int i = 0; i < moveDist; i++)
            {
                targetSquare = nextSquare(targetSquare, dir);
                if (targetSquare == null) break;
                //Pixie is normal diagonal here except that it can't attack at first step
                if (i > 0 && targetSquare.canAttackBy(this))
                {
                    Shoot shoot = new Shoot(this, targetSquare, targetSquare.piece);
                    moves.Add(shoot);
                }
                else if (targetSquare.canMove(this))
                {
                    Travel travel = new Travel(this, targetSquare);
                    moves.Add(travel);
                }
                if (!targetSquare.canPass(this)) break;
            }
        }

    }
}
