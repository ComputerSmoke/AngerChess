using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Frog : Leaper
    {
        public Frog(int color) : base(color)
        {
            name = "Frog";
        }
        public override List<Move> getMoves()
        {
            List<Move> moves = base.getMoves();
            //add hopping capture moves
            for(int dir = 4; dir < 8; dir++)
            {
                Square enemySquare = square.connectedSquares[dir];
                //can't hop if can't attack
                if (enemySquare == null || !enemySquare.canAttackBy(this)) continue;
                Square targetSquare = enemySquare.connectedSquares[dir];
                //can't hop if can't land
                if (targetSquare == null || !targetSquare.canMove(this)) continue;
                moves.Add(new Capture(this, targetSquare, enemySquare.piece));
            }
            return moves;
        }
    }
}
