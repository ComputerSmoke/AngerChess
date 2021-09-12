using System;
using System.Collections.Generic;

namespace MadChess
{
	class Board
	{
		static int rows = 8;
		static int cols = 8;
        static char[] colNames = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
		public Square[,] squares
        {
            get;
        }
        public Stack<Move> moveStack { get; set; }
        public int winner { get; set; }
        public int turn { get; set; }
        private Army army0;
        private Army army1;
		public Board(Army army0, Army army1)
		{
            turn = 1;
            winner = -2;
            squares = new Square[rows, cols];
            moveStack = new Stack<Move>();
            this.army0 = army0;
            this.army1 = army1;
            createSquares();
			connectSquares();
		}
		private void createSquares()
		{
			for(int i = 0; i < squares.GetLength(0); i++) 
			{
				for (int j = 0; j < squares.GetLength(1); j++)
				{
					squares[i,j] = new Square(i, j, this);
				}
			}
		}
		private void connectSquares()
		{
			for (int i = 0; i < squares.GetLength(0); i++)
			{
				for (int j = 0; j < squares.GetLength(1); j++)
				{
					Square square = squares[i, j];
					for (int k = 0; k < 16; k++)
					{
						int row = square.dirs[k, 0]+i;
						int col = square.dirs[k, 1]+j;
						if (row > -1 && col > -1 && row < rows && col < cols) square.connect(squares[row, col], k);
					}
				}
			}
		}
        public void win(int color)
        {
            winner = color;
        }
        public List<Move> getMoves()
        {
            List<Move> moves = new List<Move>();
            for(int i = 0; i < squares.GetLength(0); i++)
            {
                for(int j = 0; j < squares.GetLength(1); j++)
                {
                    Square square = squares[i, j];
                    Piece piece = square.piece;
                    if (piece == null || piece.color != turn) continue;
                    foreach (Move move in piece.getMoves()) moves.Add(move);
                }
            }
            return moves;
        }
        public void move(string moveString)
        {
            moveString = moveString.ToUpper();
            int c1 = moveString[0] - 65;
            int r1 = 8-int.Parse(moveString[1].ToString());
            int c2 = moveString[2] - 65;
            int r2 = 8-int.Parse(moveString[3].ToString());
            Square fromSquare = squares[r1, c1];
            Square toSquare = squares[r2, c2];
            Piece piece = fromSquare.piece;
            if (piece == null) throw new InvalidMoveException("There is no piece on " + moveString.Substring(0, 2));
            if (piece.color != turn) throw new InvalidMoveException("Cannot move enemy piece");
            List<Move> moves = piece.getMoves();
            Move thisMove = null;
            foreach(Move move in moves)
            {
                if(move.toSquare == toSquare)
                {
                    thisMove = move;
                    break;
                }
            }
            if(thisMove == null)
            {
                throw new InvalidMoveException("Piece on " + moveString.Substring(0, 2) + " cannot move to " + moveString.Substring(2, 4));
            }
            move(thisMove);
            
        }
        public void move(Move move)
        {
            moveStack.Push(move);
            move.execute();
            turn = (turn + 1) % 2;
            if (isStale() || moveStack.Count > 100) winner = -1;
        }
        public void moveNoEnd(Move move)
        {
            moveStack.Push(move);
            move.execute();
            turn = (turn + 1) % 2;
        }
        public void undo()
        {
            Move move = moveStack.Pop();
            move.undo();
            turn = (turn + 1) % 2;
        }
        public string toString()
        {
            string str = "";
            for(int i = 0; i < 8; i++)
            {
                str += (8 - i);
                for(int j = 0; j < 8; j++)
                {
                    Piece piece = squares[i, j].piece;
                    str += "\t";
                    if (piece == null)
                    {
                        str += "Null";
                    } 
                    else if (piece.color == 0)
                    {
                        str += piece.name.ToUpper();
                    } else
                    {
                        str += piece.name;
                    }
                }
                str += "\n\n";
            }
            for(int i = 0; i < 8; i++)
            {
                str += "\t" + colNames[i];
            }
            return str;
        }

        public bool canTakeKing()
        {
            List<Move> moves = getMoves();
            foreach(Move move in moves)
            {
                if(move is Capture)
                {
                    Capture capture = (Capture) move;
                    if(capture.capturedPiece is King && capture.capturedPiece.color != turn)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool isCheck()
        {
            turn = (turn + 1) % 2;
            bool check = canTakeKing();
            turn = (turn + 1) % 2;
            return check;
        }
        public bool isStale()
        {
            List<Move> moves = getMoves();
            foreach (Move move in moves)
            {
                this.moveNoEnd(move);
                if(!isCheck())
                {
                    undo();
                    return false;
                }
                undo();
            }
            return true;
        }
        /*
         * 0: stalemate
         * 1: checkmate
         * -1: neither
         */
        public int mate()
        {
            bool check = isCheck();
            bool stale = isStale();
            if (!stale) return 0;
            else if (check && stale) return 1;
            return -1;
        }
        /*
         * Bitmap format:
         * 0: blank
         * 1: black
         * 2: white
         * 3: water
         * 4: minion (pawn)
         * 5: orthogonal (rook)
         * 6: leaper (knight)
         * 7: diagonal (bishop)
         * 8: surround (queen)
         * 9: king 
         * 10: extra 1 (head)
         * 11: extra 2 (snake)
         */
        public int[,,] getBitmap()
        {
            int[,,] bitmap = new int[8, 8, 12];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Square square = squares[i, j];
                    if (square.water) bitmap[i, j, 3] = 1;
                    Piece piece = square.piece;
                    if (piece == null)
                    {
                        bitmap[i, j, 0] = 1;
                        continue;
                    }
                    bitmap[i, j, piece.color+1] = 1;
                    bitmap[i, j, piece.bitIdx + 4] = 1;
                }
            }
            return bitmap;
        }

        public int[,,] getBitmap(Move move)
        {
            this.move(move);
            int[,,] bitmap = getBitmap();
            undo();
            return bitmap;
        }

        public void saveData(Engine p0, Engine p1)
        {
            int winner = this.winner;
            Engine[] engines = { p0, p1 };
            engines[turn].saveData(getBitmap(), winner);
            while(moveStack.Count > 0)
            {
                undo();
                if(winner == 1) winner = 0;
                else if(winner == 0) winner = 1;
                engines[turn].saveData(getBitmap(), winner);
            }
        }
	}
}
