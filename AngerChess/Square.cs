namespace MadChess
{
    class Square
    {
        public Board board { get; set; } 
        public int row { get; set; } 
        public int col { get; set; }
        public int invincible { get; set; }
        public Square[] connectedSquares
        {
            get;
        }
        public bool water { get; set; } 
		static int[,] connDirs = { { -1, 0}, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 1 }, { 1, 1 }, { 1, -1 }, { -1, -1 }, { -2, 1 }, { -1, 2 }, { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 }, { -1, -2 }, { -2, -1 } };
		public int[,] dirs
		{
			get { return connDirs; }
		}
        public Piece piece
        {
            get;
            set;
        }
        public Square(int row, int col, Board board) {
			this.row = row;
			this.col = col;
            water = false;
            this.board = board;
            invincible = 0;
            connectedSquares = new Square[16];
		}
		public void connect(Square square, int connectionType)
		{
			connectedSquares[connectionType] = square;
		}
        public bool canPass(Piece movePiece)
        {
            if (piece == null) return true;
            return piece.canPass(movePiece);
        }
        public bool canAttackBy(Piece movePiece)
        {
            if (piece == null)
            {
                //en-passant-ers can attack this square only if it is the enPassant square and the square the piece is on is not invincible
                if (movePiece.canEnPassant && board.enPassant == this && board.enPassantPiece.square.invincible > 0)
                {
                    return board.enPassantPiece.canAttackBy(movePiece);
                }
                return false;
            }
            return (invincible==0 || piece.immune) && piece.canAttackBy(movePiece);
        }
        public bool canMove(Piece movePiece)
        {
            if (piece == null) return true;
            return piece.canMove(movePiece);
        }
        public void win(int color)
        {
            board.win(color);
        }
        public void undoWin()
        {
            board.win(-2);
        }
	}
}
