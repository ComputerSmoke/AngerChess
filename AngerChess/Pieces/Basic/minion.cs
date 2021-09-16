using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    abstract class Minion : Piece
    {
        protected bool canDouble;
        public bool canBeEnPassant;
        //A pawn-like piece
        public Minion(int color) : base(color)
        {
            canEnPassant = true;
            canBeEnPassant = true;
            bitIdx = 0;
        }
        public override List<Move> getMoves()
        {
            List<Move> moves = new List<Move>();
            int moveDir = 2 - 2 * color;
            int[] atkDirs = new int[2];
            if(color == 1)
            {
                atkDirs[0] = 4;
                atkDirs[1] = 7;
            } else
            {
                atkDirs[0] = 5;
                atkDirs[1] = 6;
            }
            Square moveSquare = square.connectedSquares[moveDir];
            Square[] atkSquares = new Square[2];
            for(int i = 0; i < atkSquares.GetLength(0); i++)
            {
                atkSquares[i] = square.connectedSquares[atkDirs[i]];
            }
            if (moveSquare.canMove(this))
            {
                Travel travel = new Travel(this, moveSquare);
                moves.Add(travel);
            }
            if(canDoubleMove() && moveSquare.canPass(this) && moveSquare.connectedSquares[moveDir].canMove(this))
            {
                Travel travel = new Travel(this, moveSquare.connectedSquares[moveDir]);
                moves.Add(travel);
            }
            foreach(Square atkSquare in atkSquares)
            {
                if (atkSquare == null) continue;
                if(atkSquare.canAttackBy(this))
                {
                    Capture capture;
                    //if piece on square is null, we must be taking the enPassant piece.
                    if (atkSquare.piece == null) capture = new Capture(this, atkSquare, square.board.enPassantPiece);
                    else capture = new Capture(this, atkSquare, atkSquare.piece);
                    moves.Add(capture);
                }
            }

            return moves;
        }

        protected virtual bool canDoubleMove()
        {
            return canDouble;
        }

        public override void move(Move move)
        {
            //store previous row index
            int prevRow = square.row;
            //move the piece
            base.move(move);
            //if double moved, create enPassant vulnerability
            if (canBeEnPassant && Math.Abs(prevRow - square.row) == 2)
            {
                square.board.enPassant = square.connectedSquares[color*2];
                square.board.enPassantPiece = this;
            }
            //minion cannot double move after having moved
            canDouble = false;
            //promote if on last rank
            if (square.row == 7 * ((color + 1) % 2)) promote();
        }

        public virtual void promote()
        {
            Piece piece = makePromotionPiece();
            piece.square = square;
            square.piece = piece;
            piece.linkAllPiece();
        }
        protected abstract Piece makePromotionPiece();
    }
}
