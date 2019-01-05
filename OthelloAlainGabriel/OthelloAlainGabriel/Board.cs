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
    }
}
