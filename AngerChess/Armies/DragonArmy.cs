using System;
using System.Collections.Generic;
using System.Text;

namespace MadChess
{
    class DragonArmy : Army
    {
        //number of heads in army
        public int heads { get; set; }
        //whether heads can double move
        public bool headDouble { get; set; }
        public DragonArmy(int color) : base(color)
        {
            name = "dragon";
            loadPieces();
            linkPieces();
            heads = 3;
            headDouble = true;
        }
        private void loadPieces()
        {
            for (int i = 0; i < 2; i++) pieces[i] = new PawnDragon(color);
            for (int i = 2; i < 5; i++) pieces[i] = new Head(color);
            for (int i = 5; i < 8; i++) pieces[i] = new PawnDragon(color);
            pieces[8] = new Egg(color);
            pieces[9] = new Drake(color);
            pieces[10] = new Wyvern(color);
            pieces[11] = new Hydra(color);
            pieces[12] = new King(color);
            pieces[13] = new Wyvern(color);
            pieces[14] = new Drake(color);
            pieces[15] = new Egg(color);
        }
    }
}
