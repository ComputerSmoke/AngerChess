namespace MadChess
{
	class HumanArmy : Army
    {
        public HumanArmy(int color) : base(color)
        {
            name = "human";
            loadPieces();
        }
        void loadPieces()
        {
            for(int i = 0; i < 8; i++)
            {
                pieces[i] = new Pawn(color);
            }
            pieces[8] = new Rook(color);
            pieces[9] = new Knight(color);
            pieces[10] = new Bishop(color);
            pieces[11] = new Queen(color);
            pieces[12] = new King(color);
            pieces[13] = new Bishop(color);
            pieces[14] = new Knight(color);
            pieces[15] = new Rook(color);
        }
    }
}
