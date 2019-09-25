using System;

namespace Minesweeper
{
	class Program
	{
		static int mineFlag = 5;
		static int dangerMine = 7;
		static int secure1 = 1;
		static int secure2 = 2;
		static int secure3 = 3;
		static int boardHeight = 11;
		static int boardWidth = 9;
		static int[,] boardArray = new int[boardHeight, boardWidth];
		static int[,] visibleArray = new int[boardHeight, boardWidth];
		static int[,] flagArray = new int[boardHeight, boardWidth];
		static int cursorX = 0;
		static int cursorY = 0;

		static void Main(string[] args)
		{
			//Ændre 0'erne til "?"
			//Ændre miner til "*"
			//Lav en randomizer for minerne (måske)
			//Lav en grid som brugeren kan benytte sig af (brugervenlighed) Check.
			//Vise hvor mange der rører et "safepoint"
			//0'erne skal være "safepoints"
			//Skal kunne sætte flag på minerne


			
			

			//Placing of Mines
			placeMine(2, 1);
			placeMine(3, 3);
			placeMine(4, 3);
			placeMine(0, 5);
			placeMine(1, 9);
			placeMine(4, 6);
			placeMine(5, 5);
			placeMine(6, 8);
			placeMine(6, 3);
			placeMine(7, 1);
			placeMine(8, 6);

			//place 1 secure
			placeSecureOne(0, 4);
			placeSecureOne(0, 6);
			placeSecureOne(0, 8);
			placeSecureOne(0, 9);
			placeSecureOne(0, 10);
			placeSecureOne(1, 0);
			placeSecureOne(1, 1);
			placeSecureOne(1, 2);
			placeSecureOne(1, 4);
			placeSecureOne(1, 5);
			placeSecureOne(1, 7);
			placeSecureOne(1, 10);
 
			placeSecureOne(2, 0);
			placeSecureOne(2, 3);
			placeSecureOne(2, 4);
			placeSecureOne(2, 6);
			placeSecureOne(2, 9);
			placeSecureOne(2, 10);
 
			placeSecureOne(3, 0);
			placeSecureOne(3, 1);
			placeSecureOne(3, 8);
 
			placeSecureOne(4, 7);

			
			placeSecureOne(5, 8);
			placeSecureOne(5, 9);

			placeSecureOne(6, 0);
			placeSecureOne(6, 1);
			placeSecureOne(6, 5);
			placeSecureOne(6, 6);
			placeSecureOne(6, 7);
			placeSecureOne(6, 9);

			placeSecureOne(7, 0);
			placeSecureOne(7, 3);
			placeSecureOne(7, 4);
			placeSecureOne(7, 5);
			placeSecureOne(7, 6);
			placeSecureOne(7, 8);
			placeSecureOne(7, 9);

			placeSecureOne(8, 0);
			placeSecureOne(8, 1);
			placeSecureOne(8, 2);
			placeSecureOne(8, 5);
			placeSecureOne(8, 7);
 
			//Place 2 secure
			placeSecureTwo(1, 6);
			placeSecureTwo(1, 8);

			placeSecureTwo(2, 2);
			placeSecureTwo(2, 8);

			
			placeSecureTwo(3, 4);
			placeSecureTwo(3, 5);
			placeSecureTwo(3, 6);
			placeSecureTwo(3, 7);
 
			placeSecureTwo(4, 2);
			placeSecureTwo(4, 5);

			placeSecureTwo(5, 2);
			placeSecureTwo(5, 3);
			placeSecureTwo(5, 6);
			placeSecureTwo(5, 7);

			placeSecureTwo(6, 2);
			placeSecureTwo(6, 4);

			placeSecureTwo(7, 2);
			placeSecureTwo(7, 4);
			placeSecureTwo(7, 7);

			//place 3 secure
			placeSecureThree(3, 2);

			placeSecureThree(4, 4);

			placeSecureThree(5, 4);


			showMinefield();

			Console.ReadLine();

		}


		static void showMinefield()
		{
			
			while (true)
			{
				Console.SetCursorPosition(0, 11);
				ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
				Console.Clear();

				

				Drawboard();

				
				

				if (consoleKeyInfo.KeyChar == 'w')
				{
					cursorY--;
					if (cursorY < 0)
					{
						cursorY = 0;
					}
				}

				if (consoleKeyInfo.KeyChar == 's')
				{
					cursorY++;
					if (cursorY > boardHeight - 1)
					{
						cursorY = boardHeight - 1;
					}
				}

				if (consoleKeyInfo.KeyChar == 'a')
				{
					cursorX--;
					if (cursorX < 0)
					{
						cursorX = 0;
					}
				}

				if (consoleKeyInfo.KeyChar == 'd')
				{
					cursorX++;
					if (cursorX > boardWidth - 1)
					{
						cursorX = boardWidth - 1;
					}
				}

				if (Convert.ToInt32(consoleKeyInfo.Key) == 32)
				{
					toggleFlag(cursorX, cursorY);

										
				}
				

				if (Convert.ToInt32(consoleKeyInfo.Key) == 13)
				{

					int clickedCell = boardArray[cursorY, cursorX];
					
					revealCell(cursorX, cursorY);
						
					if (clickedCell == dangerMine)
					{
						Console.Clear();
						Console.WriteLine("A mine exploded!");
						return;

					}
				
				}
				Console.WriteLine($"x:{cursorX} y:{cursorY}");

				Console.SetCursorPosition(cursorX * 2, cursorY);

				Console.Write("#");




			}
		}

		static void Drawboard()
		{
			for (int y = 0; y < boardArray.GetLength(0); y++)
			{
				for (int x = 0; x < boardArray.GetLength(1); x++)
				{
					if (flagArray[y, x] == 1)
					{
						Console.Write("* ");
					}
					else
					{
						if (visibleArray[y, x] == 1)
						{
							Console.Write(boardArray[y, x] + " ");
						}
						else
						{
							Console.Write("? ");
						}
					}
					

					
					
				}
				Console.WriteLine();
			}
		}
		static void toggleFlag(int x, int y)
		{
			if (flagArray[y,x] == 1)
			{
				removeFlag(x, y);
			}
			else
			{
				placeFlag(x, y);
			}
		}
		static void removeFlag(int x, int y)
		{
			flagArray[y, x] = 0;
		}
		static void placeFlag(int x, int y)
		{
			flagArray[y, x] = 1;
			
		}
		static void revealCell(int x, int y)
		{
			visibleArray[y, x] = 1;
		}

		static void hideCell(int x, int y)
		{
			visibleArray[y, x] = 0;
		}

		static void placeSecureOne(int x, int y)
		{
			boardArray[y, x] = secure1;
		}
		static void placeSecureTwo(int x, int y)
		{
			boardArray[y, x] = secure2;
		}

		static void placeSecureThree(int x, int y)
		{
			boardArray[y, x] = secure3;
		}
		static void placeMine(int x, int y)
		{
			boardArray[y, x] = dangerMine;
		}
    }
}
