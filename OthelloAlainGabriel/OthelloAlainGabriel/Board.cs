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
    public class Board
    {
        private static int[,] tabBoard;

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
            tabBoard = board.Clone() as int[,];
        }

        public int GetFreeCells()
        {
            int n = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (tabBoard[i, j] == 0)
                        n++;
                }
            }
            return n;
        }

        public static int[,] StrToInt(string strBoard)
        {
            int[,] board = new int[7, 9];
            int i = 0;
            int j = 0;

            for(int k = 0; k < strBoard.Length; k++)
            {
                board[i, j] = (int)Char.GetNumericValue(strBoard[k]);

                j++;
                if(j % 9 == 0)
                {
                    i++;
                    j = 0;
                }
            }
            return board;
        }
    }
}
