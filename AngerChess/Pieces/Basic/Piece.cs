using System.Collections.Generic;

namespace MadChess
{
	abstract class Piece
	{
		public abstract List<Move> getMoves();
        public string name { get; set; }
        public int color { get; set; }
        public bool canLeap { get; set; }
        protected int moveDist;
        public int bitIdx;
        public bool immune;
        public bool canEnPassant { get; set; }
        public Square square{get; set;}
        public List<Piece> samePieces { get; set; }
        //A chess piece
        protected Piece(int color)
        {
            this.color = color;
            canEnPassant = false;
            canLeap = false;
            immune = false;
            samePieces = new List<Piece>();
        }
        public virtual void capture(Capture capture)
        {
            capture.capturedPiece.captureBy(capture);
            move(capture);
        }
        public virtual void move(Move move) {
            move.toSquare.piece = this;
            square.piece = null;
            square = move.toSquare;
        }
        public virtual void undoCapture(Capture capture)
        {
            undo(capture);
            capture.capturedPiece.revive();
        }
        public virtual void undoMove(Move move)
        {
            move.toSquare.piece = null;
            undo(move);
        }
        public virtual void undo(Move move)
        {
            move.fromSquare.piece = this;
            square = move.fromSquare;
        }
        public virtual void revive()
        {
            square.piece = this;
        }
        public virtual bool canAttackBy(Piece piece)
        {
            return piece.color != color;
        }
        public virtual bool canMove(Piece piece)
        {
            return false;
        }
        public virtual bool canPass(Piece piece)
        {
            return piece.canLeap;
        }
        public virtual void captureBy(Capture capture)
        {
        }
        public virtual bool canAttack(Square square)
        {
            return square.canAttackBy(this);
        }
        public virtual void moveEffect() { }
        public virtual void linkPiece(Piece piece)
        {
            piece.samePieces.Add(this);
            samePieces.Add(piece);
        }
        //link piece to all pieces of same type on board
        public virtual void linkAllPiece()
        {
            Square[,] squares = square.board.squares;
            for(int i = 0; i < squares.GetLength(0); i++)
            {
                for(int j = 0; j < squares.GetLength(1); j++)
                {
                    Piece piece = square.piece;
                    if (piece == null) continue;
                    if (piece.GetType() == GetType()) linkPiece(piece);
                }
            }
        }

        public virtual void spawn(Spawn spawn)
        {
            spawn.toSquare.piece = this;
            square = spawn.toSquare;
        }
        public virtual void despawn(Spawn spawn)
        {
            square.piece = null;
            square = null;
        }
        public virtual void shoot(Shoot shoot)
        {
            shoot.capturedPiece.captureBy(shoot);
        }
        public virtual void undoShoot(Shoot shoot)
        {
            shoot.capturedPiece.revive();
        }
    }
}
