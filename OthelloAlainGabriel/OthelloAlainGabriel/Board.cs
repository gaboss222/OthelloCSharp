using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloAlainGabriel
{
    /// <summary>
    /// Class for the board [7, 9]
    /// </summary>
    class Board
    {
        static private int[,] tabBoard;

        public Board(int row, int col)
        {
            tabBoard = new int[row, col];
        }

        public void SetTokenOnBoard(int row, int col, Player player)
        {
            tabBoard[row, col] = player.Number;
        }

        public int GetTokenOnBoard(int row, int col)
        {
            return tabBoard[row, col];
        }

        public bool CheckTokenEquals(int row, int col, int value)
        {
            return (tabBoard[row, col] == value);
        }

        public int[,] GetBoard()
        {
            return tabBoard;
        }

        public void SetBoard(int[,] board)
        {
            tabBoard = board;
        }

        public static int[,] StrToInt(string strBoard)
        {
            int [,] board = new int[7, 9];
            int j = 0;
            // TODO HERE TRANSFORMER STR TO INT[,]
            for (int i = 0; i < strBoard.Length; i++)
            {
                board[i, j] = strBoard[i];
                if (i % 9 == 0)
                {
                    j++;
                }
            }

            return board;
        }
    }
}
