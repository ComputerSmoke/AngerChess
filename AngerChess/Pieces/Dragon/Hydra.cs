using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Hydra : Surround
    {
        static int maxHeads = 3;
        List<Head> heads;
        public Hydra(int color) : base(color)
        {
            name = "Hydra";
            moveDist = 1;
            heads = new List<Head>();
        }

        protected override void appendMovesDir(int dir, List<Move> moves)
        {
            //move normally unless going forward
            if (dir == (1 - color) * 2)
            {
                //can't spawn if square is occupied or spawned heads exceeds max heads.
                if (square.connectedSquares[dir].piece != null || ((DragonArmy)square.board.armies[color]).heads >= maxHeads) return;
                //Append spawn of new head otherwise
                Head head = new Head(color);
                Spawn spawn = new Spawn(this, square.connectedSquares[dir], head);
                moves.Add(spawn);
            } else base.appendMovesDir(dir, moves);
        }
    }
}
