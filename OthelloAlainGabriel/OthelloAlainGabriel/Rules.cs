using OthelloAlainGabriel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

public class Rules
{
    #region attributes
    private Board board;
    private Player player1;
    private Player player2;
    private Grid tokenGrid;
    private bool isPlayer1 = true;
    private int NB_ROW;
    private int NB_COL;
    #endregion

    public Rules(Board board, Player player1, Player player2, Grid tokenGrid, int NB_ROW, int NB_COL)
	{
        this.board = board;
        this.player1 = player1;
        this.player2 = player2;
        this.tokenGrid = tokenGrid;
        this.NB_ROW = NB_ROW;
        this.NB_COL = NB_COL;
    }

    /// <summary>
    /// Function used to switch token when the current player locks tokens with his own
    /// </summary>
    /// <param name="row">Row</param>
    /// <param name="col">Column</param>
    public void SwitchToken(int row, int col)
    {
        
        Label lbl = GetLabel(tokenGrid, row, col);
        AnimeLabel(lbl);
        if (board.GetNumberOnBoard(row, col) == 1)
        {
            board.SetNumberOnBoard(row, col, player2);
            lbl.Background = player2.Token.ImgBrush;
        }
        else
        {
            board.SetNumberOnBoard(row, col, player1);
            lbl.Background = player1.Token.ImgBrush;
        }
    }

    /// <summary>
    /// This function will call for all case the CheckCase method
    /// </summary>
    /// <returns>if the current player can play</returns>
    public bool CheckCases()
    {
        bool canPlay = false;
        for (int i = 0; i < NB_ROW; i++)
        {
            for (int j = 0; j < NB_COL; j++)
            {
                Label myLabel = GetLabel(tokenGrid, i, j);

                if (CheckCase(i, j, false))
                {
                    myLabel.Background = board.hoverBrush;
                    canPlay = true;
                }
                else if (board.CheckTokenEquals(i, j, 0))
                {
                    myLabel.Background = Brushes.Transparent;
                }
            }
        }
        return canPlay;
    }

    /// <summary>
    /// This function will check if it is possible to play a given case.
    /// </summary>
    /// <param name="row">row of the case</param>
    /// <param name="col">column of the case</param>
    /// <param name="switchTokens">s'il faut retourner les jetons</param>
    /// <returns></returns>
    public bool CheckCase(int row, int col, bool switchTokens)
    {
        if (!board.CheckTokenEquals(row, col, 0))
            return false;

        if (switchTokens)
        {
            CheckLeft(row, col, switchTokens);
            CheckRight(row, col, switchTokens);
            CheckTop(row, col, switchTokens);
            CheckBottom(row, col, switchTokens);
            CheckTopLeft(row, col, switchTokens);
            CheckBottomRight(row, col, switchTokens);
            CheckTopRight(row, col, switchTokens);
            CheckBottomLeft(row, col, switchTokens);
            return true;
        }
        return CheckLeft(row, col, switchTokens) || CheckRight(row, col, switchTokens) || CheckTop(row, col, switchTokens) || CheckBottom(row, col, switchTokens) || CheckTopLeft(row, col, switchTokens) || CheckBottomRight(row, col, switchTokens) || CheckTopRight(row, col, switchTokens) || CheckBottomLeft(row, col, switchTokens);
    }

    /// <summary>
    /// This function will look to the left of a case to check if it's possible to play
    /// </summary>
    /// <param name="row">row of the case</param>
    /// <param name="col">column of the case</param>
    /// <param name="switchTokens">If we have to return the tokens</param>
    /// <returns></returns>
    public bool CheckLeft(int row, int col, bool switchTokens)
    {
        int playerToken = GetNumberPlayer();

        if (col == 0 || board.GetNumberOnBoard(row, col - 1) == playerToken || board.GetNumberOnBoard(row, col - 1) == 0)
            return false;
        int i;

        bool canPlay = false;

        for (i = col - 2; i >= 0; i--)
        {
            if (board.CheckTokenEquals(row, i, playerToken))
            {
                canPlay = true;
                break;
            }
            if (board.CheckTokenEquals(row, i, 0))
            {
                canPlay = false;
                break;
            }
        }
        if (switchTokens && canPlay)
        {
            for (int j = i + 1; j < col; j++)
            {
                SwitchToken(row, j);
            }
        }
        return canPlay;
    }

    /// <summary>
    /// This function will look to the right of a case to check if it's possible to play
    /// </summary>
    /// <param name="row">row of the case</param>
    /// <param name="col">column of the case</param>
    /// <param name="switchTokens">If we have to return the tokens</param>
    /// <returns></returns>
    public bool CheckRight(int row, int col, bool switchTokens)
    {
        int playerToken = GetNumberPlayer();

        if (col == NB_COL-1 || board.GetNumberOnBoard(row, col + 1) == playerToken || board.GetNumberOnBoard(row, col + 1) == 0)
            return false;
        int i;
        bool canPlay = false;

        for (i = col + 2; i < NB_COL; i++)
        {
            if (board.CheckTokenEquals(row, i, playerToken))
            {
                canPlay = true;
                break;
            }
            if (board.CheckTokenEquals(row, i, 0))
            {
                canPlay = false;
                break;
            }
        }
        if (switchTokens && canPlay)
        {
            for (int j = i - 1; j > col; j--)
            {
                SwitchToken(row, j);
            }
        }
        return canPlay;
    }

    /// <summary>
    /// This function will look to the top of a case to check if it's possible to play
    /// </summary>
    /// <param name="row">row of the case</param>
    /// <param name="col">column of the case</param>
    /// <param name="switchTokens">If we have to return the tokens</param>
    /// <returns></returns>
    public bool CheckTop(int row, int col, bool switchTokens)
    {
        int playerToken = GetNumberPlayer();

        if (row == 0 || board.GetNumberOnBoard(row - 1, col) == playerToken || board.GetNumberOnBoard(row - 1, col) == 0)
            return false;
        int i;
        bool canPlay = false;
        for (i = row - 2; i >= 0; i--)
        {
            if (board.CheckTokenEquals(i, col, playerToken))
            {
                canPlay = true;
                break;
            }
            if (board.CheckTokenEquals(i, col, 0))
            {
                canPlay = false;
                break;
            }
        }
        if (switchTokens && canPlay)
        {
            for (int j = i + 1; j < row; j++)
            {
                SwitchToken(j, col);
            }
        }
        return canPlay;
    }

    /// <summary>
    /// This function will look to the bottom of a case to check if it's possible to play
    /// </summary>
    /// <param name="row">row of the case</param>
    /// <param name="col">column of the case</param>
    /// <param name="switchTokens">If we have to return the tokens</param>
    /// <returns></returns>
    public bool CheckBottom(int row, int col, bool switchTokens)
    {
        int playerToken = GetNumberPlayer();

        if (row == NB_ROW-1 || board.GetNumberOnBoard(row + 1, col) == playerToken || board.GetNumberOnBoard(row + 1, col) == 0)
            return false;
        int i;
        bool canPlay = false;

        for (i = row + 2; i < NB_ROW; i++)
        {
            if (board.CheckTokenEquals(i, col, playerToken))
            {
                canPlay = true;
                break;
            }
            if (board.CheckTokenEquals(i, col, 0))
            {
                canPlay = false;
                break;
            }
        }
        if (switchTokens && canPlay)
        {
            for (int j = i - 1; j > row; j--)
            {
                SwitchToken(j, col);
            }
        }
        return canPlay;
    }

    /// <summary>
    /// This function will look to the top left (diagonal) of a case to check if it's possible to play
    /// </summary>
    /// <param name="row">row of the case</param>
    /// <param name="col">column of the case</param>
    /// <param name="switchTokens">If we have to return the tokens</param>
    /// <returns></returns>
    public bool CheckTopLeft(int row, int col, bool switchTokens)
    {
        int playerToken = GetNumberPlayer();

        if (row == 0 || col == 0 || board.GetNumberOnBoard(row - 1, col - 1) == playerToken || board.GetNumberOnBoard(row - 1, col - 1) == 0)
            return false;

        int rowBase = row;
        int colBase = col;
        bool canPlay = false;

        while (row > 0 && col > 0)
        {
            row--;
            col--;
            if (board.CheckTokenEquals(row, col, playerToken))
            {
                canPlay = true;
                break;
            }
            if (board.CheckTokenEquals(row, col, 0))
            {
                canPlay = false;
                break;
            }
        }
        if (switchTokens && canPlay)
        {
            while (rowBase > row && colBase > col)
            {
                SwitchToken(rowBase, colBase);
                rowBase--;
                colBase--;
            }
        }
        return canPlay;
    }

    /// <summary>
    /// This function will look to the bottom right (diagonal) of a case to check if it's possible to play
    /// </summary>
    /// <param name="row">row of the case</param>
    /// <param name="col">column of the case</param>
    /// <param name="switchTokens">If we have to return the tokens</param>
    /// <returns></returns>
    public bool CheckBottomRight(int row, int col, bool switchTokens)
    {
        int playerToken = GetNumberPlayer();

        if (row == NB_ROW-1 || col == NB_COL-1 || board.GetNumberOnBoard(row + 1, col + 1) == playerToken || board.GetNumberOnBoard(row + 1, col + 1) == 0)
            return false;
        int rowBase = row;
        int colBase = col;
        bool canPlay = false;
        while (row < NB_ROW-1 && col < NB_ROW-1)
        {
            row++;
            col++;
            if (board.CheckTokenEquals(row, col, playerToken))
            {
                canPlay = true;
                break;
            }
            if (board.CheckTokenEquals(row, col, 0))
            {
                canPlay = false;
                break;
            }
        }
        if (switchTokens && canPlay)
        {
            while (rowBase < row && colBase < col)
            {
                SwitchToken(rowBase, colBase);
                rowBase++;
                colBase++;
            }
        }
        return canPlay;
    }

    /// <summary>
    /// This function will look to the top right (diagonal) of a case to check if it's possible to play
    /// </summary>
    /// <param name="row">row of the case</param>
    /// <param name="col">column of the case</param>
    /// <param name="switchTokens">If we have to return the tokens</param>
    /// <returns></returns>
    public bool CheckTopRight(int row, int col, bool switchTokens)
    {
        int playerToken = GetNumberPlayer();

        if (row == 0 || col == NB_COL-1 || board.GetNumberOnBoard(row - 1, col + 1) == playerToken || board.GetNumberOnBoard(row - 1, col + 1) == 0)
            return false;

        int rowBase = row;
        int colBase = col;
        bool canPlay = false;

        while (row > 0 && col < NB_COL-1)
        {
            row--;
            col++;
            if (board.CheckTokenEquals(row, col, playerToken))
            {
                canPlay = true;
                break;
            }
            if (board.CheckTokenEquals(row, col, 0))
            {
                canPlay = false;
                break;
            }
        }
        if (switchTokens && canPlay)
        {
            while (rowBase > row && colBase < col)
            {
                SwitchToken(rowBase, colBase);
                rowBase--;
                colBase++;
            }
        }
        return canPlay;
    }

    /// <summary>
    /// This function will look to the top right (diagonal) of a case to check if it's possible to play
    /// </summary>
    /// <param name="row">row of the case</param>
    /// <param name="col">column of the case</param>
    /// <param name="switchTokens">If we have to return the tokens</param>
    /// <returns></returns>
    public bool CheckBottomLeft(int row, int col, bool switchTokens)
    {
        int playerToken = GetNumberPlayer();

        if (row == NB_ROW-1 || col == 0 || board.GetNumberOnBoard(row + 1, col - 1) == playerToken || board.GetNumberOnBoard(row + 1, col - 1) == 0)
            return false;

        int rowBase = row;
        int colBase = col;
        bool canPlay = false;

        while (row < NB_ROW-1 && col > 0)
        {
            row++;
            col--;
            if (board.CheckTokenEquals(row, col, playerToken))
            {
                canPlay = true;
                break;
            }
            if (board.CheckTokenEquals(row, col, 0))
            {
                canPlay = false;
                break;
            }
        }
        if (switchTokens && canPlay)
        {
            while (rowBase < row && colBase > col)
            {
                SwitchToken(rowBase, colBase);
                rowBase++;
                colBase--;
            }
        }
        return canPlay;
    }

    /// <summary>
    /// Function used to find a Label in a Grid
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    public Label GetLabel(Grid grid, int row, int col)
    {
        foreach (Label child in grid.Children)
        {
            if (Grid.GetRow(child) == row
                  &&
               Grid.GetColumn(child) == col)
            {
                return child;
            }
        }
        return null;
    }

    /// <summary>
    /// return number (1 or 2) of the actual player
    /// </summary>
    /// <returns>Player.number</returns>
    private int GetNumberPlayer()
    {
        return (isPlayer1 ? player1.Number : player2.Number);
    }

    /// <summary>
    /// function used to change the turn
    /// </summary>
    public void ChangeTurn()
    {
        isPlayer1 = !isPlayer1;
    }

    public void AnimeLabel(Label lbl)
    {
        DoubleAnimation a = new DoubleAnimation
        {
            From = 0,
            To = 1,
            FillBehavior = FillBehavior.Stop,
            BeginTime = TimeSpan.FromSeconds(0),
            Duration = new Duration(TimeSpan.FromSeconds(0.5))
        };
        Storyboard storyboard = new Storyboard();

        storyboard.Children.Add(a);
        Storyboard.SetTarget(a, lbl);
        Storyboard.SetTargetProperty(a, new PropertyPath(MainWindow.OpacityProperty));
        //storyboard.Completed += delegate { lbl.Visibility = System.Windows.Visibility.Visible; };
        storyboard.Begin();
    }
}