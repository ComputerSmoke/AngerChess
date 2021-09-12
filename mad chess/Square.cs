namespace MadChess
{
    class Square
    {
        Board board;
        public int row { get; set; } 
        public int col { get; set; }
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
            isWater = false;
            this.board = board;
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
        public bool canAttack(Piece movePiece)
        {
            if (piece == null) return false;
            return piece.canAttack(movePiece);
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
