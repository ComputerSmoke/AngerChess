using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Head : Minion
    {
        public Head(int color):base(color)
        {
            name = "Head";
            canEnPassant = false;
            bitIdx = 6;
            canBeEnPassant = false;
        }
        protected override Piece makePromotionPiece()
        {
            ((DragonArmy)square.board.armies[color]).heads--;
            return new Egg(color);
        }
        public override void captureBy(Capture capture)
        {
            base.captureBy(capture);
            ((DragonArmy)square.board.armies[color]).heads--;
        }
        public override void revive()
        {
            base.revive();
            ((DragonArmy)square.board.armies[color]).heads++;
        }
        public override void move(Move move)
        {
            int dir = 2*(1 - color);
            Square doubleSquare = square.connectedSquares[dir];
            if (doubleSquare != null) doubleSquare = doubleSquare.connectedSquares[dir];
            //if we have moved 2 squares, no other head can ever again.
            if (move.toSquare == doubleSquare) ((DragonArmy)square.board.armies[color]).headDouble = false;
            base.move(move);
        }

        protected override bool canDoubleMove()
        {
            return ((DragonArmy)square.board.armies[color]).headDouble;
        }

        public override void spawn(Spawn spawn)
        {
            base.spawn(spawn);
            ((DragonArmy)square.board.armies[color]).heads++;
        }
        public override void despawn(Spawn spawn)
        {
            base.despawn(spawn);
            ((DragonArmy)square.board.armies[color]).heads--;
        }
    }
}
