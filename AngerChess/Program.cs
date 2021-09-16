using System;
using System.Threading;

namespace MadChess
{
    class Program 
	{
        static int games = 10000;
		static void Main(string[] args)
        {
            Engine p0 = new Engines.Human.Human();
            Engine p1 = new Engines.Human.Human();
            playHuman();
            //playGames(p0, p1);
        }
        private static void playGames(Engine p0, Engine p1)
        {
            for(int i = 0; i < games; i++)
            {
                play(p0, p1);
                Console.WriteLine("Finished game " + (i+1));
            }
        }
		private static int play(Engine p0, Engine p1)
        {
            Army army0 = new AngelArmy(0);
            Army army1 = new AngelArmy(1);
            Board board = new Board(army0, army1);
            army0.place(board);
            army1.place(board);
            Engine toPlay = p1;
            while(board.winner == -2)
            {
                Console.WriteLine(board.moveStack.Count);
                toPlay.move(board);
                if (toPlay == p1) toPlay = p0;
                else if (toPlay == p0) toPlay = p1;
            }
            int winner = board.winner;
            board.saveData(p0, p1);
            return winner;
        }
        private static void playHuman()
        {
            Army army0 = new DragonArmy(0);
            Army army1 = new AngelArmy(1);
            Board board = new Board(army0, army1);
            army0.place(board);
            army1.place(board);
            Console.WriteLine(board.toString());
            while (board.winner == -2)
            {
                Console.WriteLine("Enter move: ");
                string moveString = Console.ReadLine();
                if (moveString.ToLower() == "undo")
                {
                    board.undo();
                }
                else
                {
                    try
                    {
                        board.move(moveString);
                    }
                    catch (InvalidMoveException err)
                    {
                        Console.WriteLine(err);
                    } catch
                    {
                        Console.WriteLine("Invalid move. Moves must be given in format: e2e4");
                        continue;
                    }
                }

                Console.WriteLine(board.toString());
            }
        }
	}
}
