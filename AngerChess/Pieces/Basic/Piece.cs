﻿using System.Collections.Generic;

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
        //A chess piece
        protected Piece(int color)
        {
            this.color = color;
            canEnPassant = false;
            canLeap = false;
            immune = false;
        }
        public virtual void capture(Capture capture)
        {
            capture.capturedPiece.capture();
            move(capture);
        }
        public virtual void move(Move move) {
            move.toSquare.piece = this;
            square.piece = null;
            square = move.toSquare;
        }
        public virtual void undoCapture(Capture capture)
        {
            capture.capturedPiece.revive();
            undo(capture);
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
        public virtual void capture()
        {
            square.piece = null;
        }
        public virtual bool canAttack(Square square)
        {
            return square.canAttackBy(this);
        }
        public virtual void moveEffect() { }
    }
}
