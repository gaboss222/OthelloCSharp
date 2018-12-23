using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Timers;
using System.Windows.Threading;
using System.ComponentModel;

namespace OthelloAlainGabriel
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
            InitializeBoard();

        }
        #region Property
        Player player1, player2;
        Token token1, token2;
        Board board;
        #endregion

        #region Attribute
        private bool isPlayer1;
        private int scoreP1, scoreP2;
        private Stopwatch timerP1, timerP2;
        private Timer timerUpdate;
        #endregion

        private void InitializeGame()
        {
            token1 = new Token(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Assets\Tokens\token1.png"));
            token2 = new Token(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Assets\Tokens\token2.png"));

            player1 = new Player(token1, "Gabriel", 1);
            player2 = new Player(token2, "Alain", 2);

            //Tableau de 7 lignes (row) et 9 colonnes (col)
            board = new Board(7, 9);

            isPlayer1 = true;

            //Timer pour chaque joueurs
            timerP1 = new Stopwatch();
            timerP2 = new Stopwatch();

            ///Timer pour updater les labels
            timerUpdate = new Timer(10);
            timerUpdate.Elapsed += Timer_tick;
            timerUpdate.Start();

            lblPlayer1Score.Content = lblPlayer2Score.Content = "Score : 2";
        }

        private void InitializeBoard()
        {
            tokenGrid.Background = Brushes.LightGreen;
            for (int i = 0; i < 7; i++)
            {
                tokenGrid.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < 9; j++)
                {
                    Label lbl = new Label
                    {
                        ToolTip = ((Char)(j + 65)) + "" + (i + 1)
                    };

                    lbl.Name = "i" + i + "j" + j;
                    lbl.MouseDown += OnClickLabel;
                    lbl.BorderThickness = new Thickness(0.1, 0.1, 0.1, 0.1);
                    lbl.BorderBrush = Brushes.Black;

                    if (tokenGrid.ColumnDefinitions.Count < 9)
                        tokenGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(lbl, j);
                    Grid.SetRow(lbl, i);
                    tokenGrid.Children.Add(lbl);

                    if ((i == 3 && j == 3) || (i == 4 && j == 4))
                    {
                        lbl.Background = player2.Token.ImgBrush;
                        lbl.MouseDown -= OnClickLabel;
                        board.SetTokenOnBoard(i, j, player2);
                    }
                    if ((i == 3 && j == 4) || (i == 4 && j == 3))
                    {
                        lbl.Background = player1.Token.ImgBrush;
                        lbl.MouseDown -= OnClickLabel;
                        board.SetTokenOnBoard(i, j, player1);
                    }
                }
            }

            gridPlayerTurn.Background = Brushes.LightGreen;
            lblPlayerImgTurn.Background = player1.Token.ImgBrush;
            CheckCases();
            timerP1.Start();
        }

        #region FormsFunction
        private void OnClickLabel(object sender, RoutedEventArgs e)
        {

            Label lbl = sender as Label;

            int row = (int)Char.GetNumericValue(lbl.Name[1]);
            int col = (int)Char.GetNumericValue(lbl.Name[3]);

            if (CheckCase(row, col, false))
            {
                CheckCase(row, col, true);
                if (isPlayer1)
                    ChangeBoardPlayer(row, col, lbl, player1);
                else
                    ChangeBoardPlayer(row, col, lbl, player2);
                lbl.MouseDown -= OnClickLabel;
            }
            CheckScore();
            CheckCases();
        }

        /// <summary>
        /// Called each time a label is clicked
        /// Update board (Change color, change label player, change token)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="lbl"></param>
        /// <param name="p"></param>
        private void ChangeBoardPlayer(int row, int col, Label lbl, Player p)
        {

            lbl.Background = p.Token.ImgBrush;
            board.SetTokenOnBoard(row, col, p);

            switch(p.Number)
            {
                case 1:
                    isPlayer1 = false;
                    timerP1.Stop();
                    timerP2.Start();
                    lblPlayerImgTurn.Background = player2.Token.ImgBrush;
                    break;
                case 2:
                    isPlayer1 = true;
                    timerP2.Stop();
                    timerP1.Start();
                    lblPlayerImgTurn.Background = player1.Token.ImgBrush;
                    break;
            }
        }

        /// <summary>
        /// Update continuously labelTime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_tick(object sender, ElapsedEventArgs e)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    lblPlayer1Time.Content = timerP1.Elapsed.ToString("mm\\:ss\\.ff");
                }));

                Dispatcher.Invoke(new Action(() =>
                {
                    lblPlayer2Time.Content = timerP2.Elapsed.ToString("mm\\:ss\\.ff");
                }));
            });
        }
        #endregion

        #region gameAlgo

        private void SwitchToken(int row, int col)
        {
            Label lbl = GetChildren(tokenGrid, row, col) as Label;
            //if (tabBoard[row, col] == 1)
            if (board.GetTokenOnBoard(row, col) == 1)
            {
                //tabBoard[row, col] = 2;
                board.SetTokenOnBoard(row, col, player2);
                lbl.Background = player2.Token.ImgBrush;
            }
            else
            {
                //tabBoard[row, col] = 1;
                board.SetTokenOnBoard(row, col, player1);
                lbl.Background = player1.Token.ImgBrush;
            }
        }

        private void CheckCases()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Label myLabel = GetChildren(tokenGrid, i, j) as Label;
                    //if (CheckCase(j, i, false))
                    if (CheckCase(i, j, false))
                    {
                        myLabel.Background = Brushes.Green;
                    }
                    //else if (tabBoard[i, j] == 0)
                    else if (board.CheckTokenEquals(i, j, 0))
                    {
                        myLabel.Background = Brushes.Transparent;
                    }
                }
            }
        }
        private bool CheckCase(int row, int col, bool switchTokens)
        {
            //if (tabBoard[row, col] != 0)
            if (!board.CheckTokenEquals(row, col, 0))
                return false;

            if (switchTokens)
            {
                //CheckLeft(col, row, switchTokens); CheckRight(col, row, switchTokens); CheckTop(col, row, switchTokens); CheckBottom(col, row, switchTokens); CheckTopLeft(col, row, switchTokens); CheckBottomRight(col, row, switchTokens); CheckTopRight(col, row, switchTokens); CheckBottomLeft(col, row, switchTokens);
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
        private bool CheckLeft(int row, int col, bool switchTokens)
        {
            int playerToken = GetNumberPlayer();

            //if (col == 0 || tabBoard[row, col - 1] == playerToken || tabBoard[row, col - 1] == 0)
            if (col == 0 || board.GetTokenOnBoard(row, col - 1) == playerToken || board.GetTokenOnBoard(row, col - 1) == 0)
                return false;
            int i;

            bool canPlay = false;

            for (i = col - 2; i >= 0; i--)
            {
                //if (tabBoard[row, i] == playerToken)
                if (board.CheckTokenEquals(row, i, playerToken))
                {
                    canPlay = true;
                    break;
                }
                //if (tabBoard[row, i] == 0)
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
                    //SwitchToken(j, row);
                    SwitchToken(row, j);
                }
                Console.WriteLine(i + " " + col);
            }
            return canPlay;
        }
        private bool CheckRight(int row, int col, bool switchTokens)
        {
            int playerToken = GetNumberPlayer();

            //if (col == 8 || tabBoard[row, col + 1] == playerToken || tabBoard[row, col + 1] == 0)
            if (col == 8 || board.GetTokenOnBoard(row, col + 1) == playerToken || board.GetTokenOnBoard(row, col + 1) == 0)
                return false;
            int i;
            bool canPlay = false;

            for (i = col + 2; i < 9; i++)
            {
                //if (tabBoard[row, i] == playerToken)
                if (board.CheckTokenEquals(row, i, playerToken))
                {
                    canPlay = true;
                    break;
                }
                //if (tabBoard[row, i] == 0)
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
                    //SwitchToken(j, row);
                    SwitchToken(row, j);
                }
                Console.WriteLine(i + " " + col);
            }
            return canPlay;
        }
        private bool CheckTop(int row, int col, bool switchTokens)
        {
            int playerToken = GetNumberPlayer();

            //if (row == 0 || tabBoard[row - 1, col] == playerToken || tabBoard[row - 1, col] == 0)
            if (row == 0 || board.GetTokenOnBoard(row - 1, col) == playerToken || board.GetTokenOnBoard(row - 1, col) == 0)
                return false;
            int i;
            bool canPlay = false;
            for (i = row - 2; i >= 0; i--)
            {
                //if (tabBoard[i, col] == playerToken)
                if (board.CheckTokenEquals(i, col, playerToken))
                {
                    canPlay = true;
                    break;
                }
                //if (tabBoard[i, col] == 0)
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
                    //SwitchToken(col, j);
                    SwitchToken(j, col);
                }
                Console.WriteLine(i + " " + row);
            }
            return canPlay;
        }
        private bool CheckBottom(int row, int col, bool switchTokens)
        {
            int playerToken = GetNumberPlayer();

            //if (row == 6 || tabBoard[row + 1, col] == playerToken || tabBoard[row + 1, col] == 0)
            if (row == 6 || board.GetTokenOnBoard(row + 1, col) == playerToken || board.GetTokenOnBoard(row+1, col) == 0)
                return false;
            int i;
            bool canPlay = false;

            for (i = row + 2; i < 7; i++)
            {
                //if (tabBoard[i, col] == playerToken)
                if(board.CheckTokenEquals(i, col, playerToken))
                {
                    canPlay = true;
                    break;
                }
                //if (tabBoard[i, col] == 0)
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
                    //SwitchToken(col, j);
                    SwitchToken(j, col);
                }
                Console.WriteLine(i + " " + row);
            }
            return canPlay;
        }
        private bool CheckTopLeft(int row, int col, bool switchTokens)
        {
            int playerToken = GetNumberPlayer();

            //if (row == 0 || col == 0 || tabBoard[row - 1, col - 1] == playerToken || tabBoard[row - 1, col - 1] == 0)
            if (row == 0 || col == 0 || board.GetTokenOnBoard(row - 1, col - 1) == playerToken || board.GetTokenOnBoard(row - 1, col - 1) == 0)
                return false;

            int rowBase = row;
            int colBase = col;
            bool canPlay = false;

            while (row > 0 && col > 0)
            {
                row--;
                col--;
                //if (tabBoard[row, col] == playerToken)
                if (board.CheckTokenEquals(row, col, playerToken))
                {
                    canPlay = true;
                    break;
                }
                //if (tabBoard[row, col] == 0)
                if (board.CheckTokenEquals(row, col, 0))
                {
                    canPlay = false;
                    break;
                }
            }
            if (switchTokens && canPlay)
            {
                Console.WriteLine(colBase + ";" + rowBase + " " + col + ";" + row);
                while (rowBase > row && colBase > col)
                {
                    //SwitchToken(colBase, rowBase);
                    SwitchToken(rowBase, colBase);
                    rowBase--;
                    colBase--;
                }
            }
            return canPlay;
        }
        private bool CheckBottomRight(int row, int col, bool switchTokens)
        {
            int playerToken = GetNumberPlayer();

            //if (row == 6 || col == 8 || tabBoard[row + 1, col + 1] == playerToken || tabBoard[row + 1, col + 1] == 0)
            if (row == 6 || col == 8 || board.GetTokenOnBoard(row + 1, col + 1) == playerToken || board.GetTokenOnBoard(row + 1, col + 1) == 0)
                return false;
            int rowBase = row;
            int colBase = col;
            bool canPlay = false;
            while (row < 6 && col < 8)
            {
                row++;
                col++;
                //if (tabBoard[row, col] == playerToken)
                if (board.CheckTokenEquals(row, col, playerToken))
                {
                    canPlay = true;
                    break;
                }
                //if (tabBoard[row, col] == 0)
                if (board.CheckTokenEquals(row, col, 0))
                {
                    canPlay = false;
                    break;
                }
            }
            if (switchTokens && canPlay)
            {
                Console.WriteLine(colBase + ";" + rowBase + " " + col + ";" + row);
                while (rowBase < row && colBase < col)
                {
                    //SwitchToken(colBase, rowBase);
                    SwitchToken(rowBase, colBase);
                    rowBase++;
                    colBase++;
                }
            }
            return canPlay;
        }
        private bool CheckTopRight(int row, int col, bool switchTokens)
        {
            int playerToken = GetNumberPlayer();

            //if (row == 0 || col == 8 || tabBoard[row - 1, col + 1] == playerToken || tabBoard[row - 1, col + 1] == 0)
            if (row == 0 || col == 8 || board.GetTokenOnBoard(row - 1, col + 1) == playerToken || board.GetTokenOnBoard(row - 1, col + 1) == 0)
                return false;

            int rowBase = row;
            int colBase = col;
            bool canPlay = false;

            while (row > 0 && col < 8)
            {
                row--;
                col++;
                //if (tabBoard[row, col] == playerToken)
                if (board.CheckTokenEquals(row, col, playerToken))
                {
                    canPlay = true;
                    break;
                }
                //if (tabBoard[row, col] == 0)
                if (board.CheckTokenEquals(row, col, 0))
                {
                    canPlay = false;
                    break;
                }
            }
            if (switchTokens && canPlay)
            {
                Console.WriteLine(colBase + ";" + rowBase + " " + col + ";" + row);
                while (rowBase > row && colBase < col)
                {
                    //SwitchToken(colBase, rowBase);
                    SwitchToken(rowBase, colBase);
                    rowBase--;
                    colBase++;
                }
            }
            return canPlay;
        }
        private bool CheckBottomLeft(int row, int col, bool switchTokens)
        {
            int playerToken = GetNumberPlayer();

            //if (row == 6 || col == 0 || tabBoard[row + 1, col - 1] == playerToken || tabBoard[row + 1, col - 1] == 0)
            if (row == 6 || col == 0 || board.GetTokenOnBoard(row + 1, col - 1) == playerToken || board.GetTokenOnBoard(row + 1, col - 1) == 0)
                return false;

            int rowBase = row;
            int colBase = col;
            bool canPlay = false;

            while (row < 6 && col > 0)
            {
                row++;
                col--;
                //if (tabBoard[row, col] == playerToken)
                if (board.CheckTokenEquals(row, col, playerToken))
                {
                    canPlay = true;
                    break;
                }
                //if (tabBoard[row, col] == 0)
                if (board.CheckTokenEquals(row, col, 0))
                {
                    canPlay = false;
                    break;
                }
            }
            if (switchTokens && canPlay)
            {
                Console.WriteLine(colBase + ";" + rowBase + " " + col + ";" + row);
                while (rowBase < row && colBase > col)
                {
                    //SwitchToken(colBase, rowBase);
                    SwitchToken(rowBase, colBase);
                    rowBase++;
                    colBase--;
                }
            }
            return canPlay;
        }

        private void CheckScore()
        {
            scoreP1 = scoreP2 = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    /*if (tabBoard[i, j] == 1)
                        scoreP1++;
                    else if (tabBoard[i, j] == 2)
                        scoreP2++;*/
                    if (board.CheckTokenEquals(i, j, player1.Number))
                        scoreP1++;
                    else if (board.CheckTokenEquals(i, j, player2.Number))
                        scoreP2++;
                }
            }

            player1.Score = scoreP1;
            player2.Score = scoreP2;

            lblPlayer1Score.Content = "Score : " + player1.Score;
            lblPlayer2Score.Content = "Score : " + player2.Score;
        }

        /// <summary>
        /// return number (1 or 2) of the actual player
        /// </summary>
        /// <returns>Player.number</returns>
        private int GetNumberPlayer()
        {
            return (isPlayer1 ? player1.Number : player2.Number);
        }

        private static UIElement GetChildren(Grid grid, int row, int column)
        {
            foreach (UIElement child in grid.Children)
            {
                if (Grid.GetRow(child) == row
                      &&
                   Grid.GetColumn(child) == column)
                {
                    return child;
                }
            }
            return null;
        }
        #endregion

        #region MenuFunction 

        /// <summary>
        /// Restart a new game (1vs1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // MODIFICATION ICI !!!
        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // TODO HERE CHANGE LBL BACKGROUND COLOR + RESET a ZERO LES CASES ET REMETTRES LES 4 TOKENS DU DEBUT
                    // 1 Fonction
                    Label lbl = GetChildren(tokenGrid, i, j) as Label;
                    tokenGrid.Children.Remove(lbl);
                }
            }

            //tabBoard = null;
            board = null;

            InitializeGame();
            InitializeBoard();
        }
        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Close the app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MenuUndo_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("About : GG and AG Othello");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion
    }
}