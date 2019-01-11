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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="col">col</param>
        public Board(int row, int col)
        {
            tabBoard = new int[row, col];
        }

        /// <summary>
        /// Put player's number on board at [row,col]
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="col">col</param>
        /// <param name="player">current player</param>
        public void SetNumberOnBoard(int row, int col, Player player)
        {
            tabBoard[row, col] = player.Number;
        }

        /// <summary>
        /// Get token (1 or 2) ar [row, col]
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="col">col</param>
        /// <returns></returns>
        public int GetNumberOnBoard(int row, int col)
        {
            return tabBoard[row, col];
        }

        /// <summary>
        /// Check if board[row,col] equals to the value passed as parameter
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="col">col</param>
        /// <param name="value">value(1 or 2)</param>
        /// <returns>Return true if tabBoard[row,col] equals to value, else return false</returns>
        public bool CheckTokenEquals(int row, int col, int value)
        {
            return (tabBoard[row, col] == value);
        }

        /// <summary>
        /// Return the board
        /// </summary>
        /// <returns>board as int[,]</returns>
        public int[,] GetBoard()
        {
            return tabBoard;
        }

        /// <summary>
        /// Set a new board a board, used for loaded game.
        /// </summary>
        /// <param name="board">new board</param>
        public void SetBoard(int[,] board)
        {
            tabBoard = board.Clone() as int[,];
        }

        /// <summary>
        /// Return number of free cells. Used to check when the board is full
        /// </summary>
        /// <returns>return number of free cells</returns>
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

        /// <summary>
        /// Convert a string to board.
        /// The string passed as parameter is sequence of 0 (free cell), 1 (player1 token) or 2 (player2 token)
        /// </summary>
        /// <param name="strBoard">string equals to the board</param>
        /// <returns>Return the board as an int[,]</returns>
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

        /// <summary>
        /// Override board to convert int[,] to string
        /// Used to save board as a String for xml file
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strBoard = "";
            //Put the board into a string
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    strBoard += this.GetNumberOnBoard(i, j);
                }
            };

            return strBoard;
        }
    }
}
