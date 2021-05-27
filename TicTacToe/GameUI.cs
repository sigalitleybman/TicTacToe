using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TicTacToe
{
   public class GameUI//add program ?
    {
        public static GameLogic s_TicTacToeGame = new GameLogic();
        public static BoardGame s_GameMatrix = new BoardGame(InputSize());
        public static Player s_ComputerPlayerO;
        public static Player s_HumanPlayerX;
        public static Player s_HumanPlayerO;

        public static void Main()
        {
           EntryPoint();
        }

        public static void EntryPoint()
        {
            int size = s_GameMatrix.Size;
            bool quit = false;
            string computerOrHuman = ComputerOrHuman();
            
            if (computerOrHuman == "1")
            {
                s_ComputerPlayerO = new Player("computer (sign O)", size);
                s_HumanPlayerX = new Player("human (sign X)", size);
                DoYouWantToQuit();
                CreateBoard(size, s_GameMatrix.GameMatrix);
                quit = s_TicTacToeGame.ComputerInputXO(s_GameMatrix.GameMatrix, s_GameMatrix.Size, 
                                        s_ComputerPlayerO, s_HumanPlayerX);
                PrintScore(s_ComputerPlayerO, s_HumanPlayerX);
                while (ToContinue())
                {
                    s_GameMatrix.ClearGameBoard();
                    s_ComputerPlayerO.InitializeCounter();
                    s_HumanPlayerX.InitializeCounter();
                    DoYouWantToQuit();
                    CreateBoard(size, s_GameMatrix.GameMatrix);
                    quit = s_TicTacToeGame.ComputerInputXO(s_GameMatrix.GameMatrix, s_GameMatrix.Size,
                                            s_ComputerPlayerO, s_HumanPlayerX);
                    PrintScore(s_ComputerPlayerO, s_HumanPlayerX);
                }
            }
            else
            {
                s_HumanPlayerX = new Player("human (sign X)", size);
                s_HumanPlayerO = new Player("human (sign O)", size);
                DoYouWantToQuit();
                CreateBoard(size, s_GameMatrix.GameMatrix);
                quit = s_TicTacToeGame.HumanInputXO(s_GameMatrix.GameMatrix, s_GameMatrix.Size,
                            s_HumanPlayerX, s_HumanPlayerO);
                PrintScore(s_HumanPlayerX, s_HumanPlayerO);
                while (ToContinue())
                {
                    s_HumanPlayerX.InitializeCounter();
                    s_HumanPlayerO.InitializeCounter();
                    s_GameMatrix.ClearGameBoard();
                    DoYouWantToQuit();
                    CreateBoard(size, s_GameMatrix.GameMatrix);
                    quit = s_TicTacToeGame.HumanInputXO(s_GameMatrix.GameMatrix, s_GameMatrix.Size,
                            s_HumanPlayerX, s_HumanPlayerO);
                    PrintScore(s_HumanPlayerX, s_HumanPlayerO);
                }
            }
        }

        public static string GetInput(string i_Name1, string i_Name2, Enum i_eReason)
        {
            ePrintReason reason = (ePrintReason)i_eReason;
            switch (reason)
            {
                case ePrintReason.Clear:
                    //Ex02.ConsoleUtils.Screen.Clear(); /// return to this
                    Console.Clear();
                    break;
                case ePrintReason.Define:
                    PlayersCharDefinition(i_Name1, i_Name2);
                    break;
                case ePrintReason.BoardState:
                    CreateBoard(s_GameMatrix.Size, s_GameMatrix.GameMatrix);
                    break;
                case ePrintReason.Winner:
                    WinnerPlayer(i_Name1);
                    break;
                case ePrintReason.HumanX:
                    Turn(i_Name1);
                    break;
                case ePrintReason.HumanO:
                    Turn(i_Name1);
                    break;
                case ePrintReason.Computer:
                    ComputerPlayed(i_Name1);
                    break;
                case ePrintReason.Row:
                    ChooseRow();
                    break;
                case ePrintReason.Column:
                    ChooseColumn();
                    break;
                case ePrintReason.PlaceTaken:
                    PlaceIsTaken();
                    break;
                case ePrintReason.Quit:
                    DecidedToQuit();
                    break;
                case ePrintReason.Tie:
                    Tie();
                    break;
                case ePrintReason.Input:
                    return InputFromTheUser();
                case ePrintReason.WrongInput:
                    WrongInput();
                    break;
                default:
                    break;
            }

            return string.Empty;
        }
   
        public static int InputSize()
        {
            int size = 0;
            bool digit = false;

            AskForBoardSize();
            digit = int.TryParse(InputFromTheUser(), out size);
            while (size < 3 || size > 9 || digit == false)
            {
                WrongInput();
                AskForBoardSize();
                digit = int.TryParse(InputFromTheUser(), out size);
            }

            return size;
        }

        public static string ComputerOrHuman()
        {
            string choice = string.Empty;
            
            SelectGameMode();
            choice = InputFromTheUser();
            while (!choice.Equals("1") && !choice.Equals("2"))
            {
                WrongChoice();
                choice = InputFromTheUser();
            }

            return choice;
        }

        public static void CreateBoard(int i_Size, char[,] i_GameMatrix)
        {
            int row = 0, column = 0;
            string equalLine = new string('=', (i_Size * 4) + 1);
            StringBuilder line = new StringBuilder();

            line.Append("  ");
            for (int i = 1 ; i <= i_Size ; i++)
            {
                line.Append(i).Append("   ");
            }

            line.Append("\n");
            for (row = 1 ; row <= i_Size ; row++)
            {
                for (column = 1 ; column <= i_Size ; column++)
                {
                    if (column == 1)
                    {
                        line.Append(row).Append("|");
                    }
                  
                    line.Append(" ").Append(i_GameMatrix[row, column]).Append(" |");
                }
                
                line.Append("\n").Append(" ").Append(equalLine).Append("\n");
            }

            PrintStringBuilder(line);
        }

        public static void PrintScore(Player i_Player1, Player i_Player2)
        {
            string separateLine = string.Empty;
            StringBuilder scoreTable = new StringBuilder();
            string spaceLine = new string(' ', 11);
            int size = i_Player1.Id.Length;

            scoreTable.Append("\n ").Append(i_Player1.Id).Append(" | ").Append(i_Player2.Id).Append("\n");
            separateLine = new string('-', scoreTable.Length);
            scoreTable.Append(separateLine).Append("\n");
            scoreTable.Append(spaceLine).Append(i_Player1.Score);
            spaceLine.Replace(" ", "    ");
            scoreTable.Append(spaceLine).Append(i_Player2.Score);
            PrintStringBuilder(scoreTable);
        }

        public static bool ToContinue()
        {
            string answer = string.Empty;
            bool toContinue = false;

            DoYouWantToContinue();
            answer = InputFromTheUser();
            while (!answer.Equals("1") && !answer.Equals("2"))
            {
                WrongInput();
                answer = InputFromTheUser();
            }

            if (answer.Equals("1"))
            {
                toContinue = true;
            }
            else
            {
                Console.WriteLine("\nGAME OVER !");
                Thread.Sleep(1300);
                Environment.Exit(0);
            }

            return toContinue;
        }

        /////////print and input functions 
        public static void SelectGameMode()
        {
            Console.WriteLine("Select 1 -> computer vs human OR Select 2 -> human vs human");
        }

        public static void WrongChoice()
        {
            Console.WriteLine("You entered a wrong choice. Please enter again");
        }

        public static void WrongInput()
        {
            Console.WriteLine("You entered a wrong input. Please enter again");
        }

        public static void AskForBoardSize()
        {
            Console.WriteLine("Please enter a size:");
        }

        public static void DoYouWantToQuit()
        {
            Console.WriteLine("If you want to quit - enter Q");
        }

        public static string InputFromTheUser()
        {
            return Console.ReadLine();
        }

        public static void ChooseRow()
        {
            Console.WriteLine("Please choose a row");
        }

        public static void ChooseColumn()
        {
            Console.WriteLine("Please choose a column");
        }

        public static void PlaceIsTaken()
        {
            Console.WriteLine("this place is taken");
        }

        public static void DecidedToQuit()
        {
            Console.WriteLine("YOU DECIDED TO QUIT THE GAME");
        }

        public static void PlayersCharDefinition(string i_ID1, string i_ID2)
        {
            Console.WriteLine("Player1: " + i_ID1 + "  Player2: " + i_ID2 + "\n");
        }

        public static void Turn(string i_ID)
        {
            Console.WriteLine("It's Player " + i_ID + " turn");
        }

        public static void WinnerPlayer(string i_ID)
        {
            Console.WriteLine("Player " + i_ID + " is the winner !!! \nCongratulations!!! :)");
        }

        public static void Tie()
        {
            Console.WriteLine("Game is over there was a tie");
        }

        public static void ComputerPlayed(string i_ID)
        {
            Console.WriteLine(i_ID + " played");
        }

        public static void PrintStringBuilder(StringBuilder i_StringToPrint)
        {
            Console.WriteLine(i_StringToPrint);
        }

        public static void DoYouWantToContinue()
        {
            Console.WriteLine("\nDo you want to continue? For YES -> 1 For NO -> 2");
        }
    }
}
