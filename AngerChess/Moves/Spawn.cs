using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class Spawn : Move
    {
        public Piece spawnedPiece { get; set; }
        public Spawn(Piece piece, Square toSquare, Piece spawnedPiece):base(piece, toSquare)
        {
            this.spawnedPiece = spawnedPiece;
        }
        public override void execute()
        {
            if (toSquare.piece != null) throw new BadSpawnException("Error: Tried to spawn a piece on an occupied square.");
            spawnedPiece.spawn(this);
        }
        public override void undo()
        {
            spawnedPiece.despawn(this);
        }
    }
}
