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
using System.Xml;

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
            InitializeBoard(false);
        }
        #region Property
        Player player1, player2;
        Token token1, token2;
        Board board;
        #endregion

        #region Attribute
        private bool isPlayer1;
        private MyStopwatch timerP1, timerP2;
        private Timer timerUpdate;
        private int nbFreeCells;
        private int turn;
        #endregion

        public struct GameParameter
        {
            public string p1Name, p2Name;
            public int p1Score, p2Score, turn, playerTurn;
            public int[,] newBoard;
            TimeSpan p1Time, p2Time;
            public MyStopwatch p1Stopwatch, p2Stopwatch;

            public GameParameter(string p1Name, TimeSpan p1Time, int p1Score, string p2Name, TimeSpan p2Time, int p2Score, int[,] tabBoard, int turn, int playerTurn)
            {
                this.p1Name = p1Name;
                this.p1Time = p1Time;
                this.p1Score = p1Score;
                this.p2Name = p2Name;
                this.p2Time = p2Time;
                this.p2Score = p2Score;
                this.turn = turn;
                newBoard = new int[7, 9];
                this.p1Time = p1Time;
                this.p2Time = p2Time;
                this.playerTurn = playerTurn;
                p1Stopwatch = new MyStopwatch(p1Time);
                p2Stopwatch = new MyStopwatch(p2Time);
                CopyBoard(tabBoard);

            }

            public void CopyBoard(int[,] b)
            {
                newBoard = b.Clone() as int[,];
            }
        }


        private void InitializeGame(GameParameter g = default(GameParameter))
        {
            token1 = new Token(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Assets\Tokens\token1.png"));
            token2 = new Token(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Assets\Tokens\token2.png"));

            board = new Board(7, 9);

            if (g.Equals(default(GameParameter)))
            {
                player1 = new Player(token1, "Gabriel", 1);
                player2 = new Player(token2, "Alain", 2);      

                //Timer pour chaque joueurs
                timerP1 = new MyStopwatch();
                timerP2 = new MyStopwatch();

                lblPlayer1Score.Content = lblPlayer2Score.Content = "Score : 2";
                isPlayer1 = true;
                nbFreeCells = (7 * 9) - 4;
                turn = 1;
            }
            else
            {

                player1 = new Player(token1, g.p1Name, 1);
                player2 = new Player(token2, g.p2Name, 2);

                board.SetBoard(g.newBoard);

                if (g.playerTurn == 2)
                    isPlayer1 = false;

                //Timer pour chaque joueurs
                timerP1 = g.p1Stopwatch;
                timerP2 = g.p2Stopwatch;

                lblPlayer1Score.Content = "Score : " + g.p1Score.ToString();
                lblPlayer2Score.Content = "Score : " + g.p2Score.ToString();

                nbFreeCells = board.GetFreeCells();
                turn = g.turn;
            }

            ///Timer pour updater les labels
            timerUpdate = new Timer(10);
            timerUpdate.Elapsed += Timer_tick;
            timerUpdate.Start();


        }

        private void InitializeBoard(bool loadedGame)
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
                    lbl.MouseEnter += OnEnterLabel;
                    lbl.MouseLeave += OnLeaveLabel;
                    lbl.BorderThickness = new Thickness(0.1, 0.1, 0.1, 0.1);
                    lbl.BorderBrush = Brushes.Black;

                    if (tokenGrid.ColumnDefinitions.Count < 9)
                        tokenGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(lbl, j);
                    Grid.SetRow(lbl, i);
                    tokenGrid.Children.Add(lbl);
                    if (loadedGame == false)
                    {
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
                    else
                    {
                        if (board.GetTokenOnBoard(i, j) == 1)
                        {
                            Console.WriteLine("TOKEN1");
                            lbl.Background = player1.Token.ImgBrush;
                            lbl.MouseDown -= OnClickLabel;
                        }
                        else if (board.GetTokenOnBoard(i, j) == 2)
                        {
                            Console.WriteLine("TOKEN2");

                            lbl.Background = player2.Token.ImgBrush;
                            lbl.MouseDown -= OnClickLabel;
                        }
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

                nbFreeCells--;

                lbl.MouseDown -= OnClickLabel;

                Console.WriteLine("Case libres : " + nbFreeCells);
            }

            CheckScore();
            if (CheckIfFinish())
            {
                FinishFunction();
                return;
            }

            if (!CheckCases())
            {
                // Fonction pour changer de tour
                if (!CheckCases())
                {
                    FinishFunction();
                }
            }
        }
        private void OnEnterLabel(object sender, RoutedEventArgs e)
        {
            Label lbl = sender as Label;
            int row = (int)Char.GetNumericValue(lbl.Name[1]);
            int col = (int)Char.GetNumericValue(lbl.Name[3]);
            if (CheckCase(row, col, false))
            {
                lbl.Background = isPlayer1 ? player1.Token.ImgBrush : player2.Token.ImgBrush;
            }
        }
        private void OnLeaveLabel(object sender, RoutedEventArgs e)
        {
            Label lbl = sender as Label;
            int row = (int)Char.GetNumericValue(lbl.Name[1]);
            int col = (int)Char.GetNumericValue(lbl.Name[3]);
            if (CheckCase(row, col, false))
            {
                lbl.Background = Brushes.Green;
            }
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

            switch (p.Number)
            {
                case 1:
                    isPlayer1 = false;
                    timerP1.Stop();
                    timerP2.Start();
                    lblPlayerImgTurn.Background = player2.Token.ImgBrush;
                    turn++;
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

        private bool CheckCases()
        {
            bool canPlay = false;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Label myLabel = GetChildren(tokenGrid, i, j) as Label;

                    if (CheckCase(i, j, false))
                    {
                        myLabel.Background = Brushes.Green;
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
        /// Cette fonction va vérifier si c'est possible de jouer à une case donnée.
        /// </summary>
        /// <param name="row">ligne de la case</param>
        /// <param name="col">colonne de la case</param>
        /// <param name="switchTokens">s'il faut retourner les jetons</param>
        /// <returns></returns>
        private bool CheckCase(int row, int col, bool switchTokens)
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
        /// Cette fonction va regarder à gauche d'une case pour vérifier s'il est possible de jouer
        /// </summary>
        /// <param name="row">ligne de la case</param>
        /// <param name="col">colonne de la case</param>
        /// <param name="switchTokens">S'il faut retourner les jetons</param>
        /// <returns></returns>
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
                // Console.WriteLine(i + " " + col);
            }
            return canPlay;
        }

        /// <summary>
        /// Cette fonction va regarder à droite d'une case pour vérifier s'il est possible de jouer
        /// </summary>
        /// <param name="row">ligne de la case</param>
        /// <param name="col">colonne de la case</param>
        /// <param name="switchTokens">S'il faut retourner les jetons</param>
        /// <returns></returns>
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
                //Console.WriteLine(i + " " + col);
            }
            return canPlay;
        }

        /// <summary>
        /// Cette fonction va regarder en haut d'une case pour vérifier s'il est possible de jouer
        /// </summary>
        /// <param name="row">ligne de la case</param>
        /// <param name="col">colonne de la case</param>
        /// <param name="switchTokens">S'il faut retourner les jetons</param>
        /// <returns></returns>
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
                //Console.WriteLine(i + " " + row);
            }
            return canPlay;
        }

        /// <summary>
        /// Cette fonction va regarder en bas d'une case pour vérifier s'il est possible de jouer
        /// </summary>
        /// <param name="row">ligne de la case</param>
        /// <param name="col">colonne de la case</param>
        /// <param name="switchTokens">S'il faut retourner les jetons</param>
        /// <returns></returns>
        private bool CheckBottom(int row, int col, bool switchTokens)
        {
            int playerToken = GetNumberPlayer();

            //if (row == 6 || tabBoard[row + 1, col] == playerToken || tabBoard[row + 1, col] == 0)
            if (row == 6 || board.GetTokenOnBoard(row + 1, col) == playerToken || board.GetTokenOnBoard(row + 1, col) == 0)
                return false;
            int i;
            bool canPlay = false;

            for (i = row + 2; i < 7; i++)
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
                for (int j = i - 1; j > row; j--)
                {
                    //SwitchToken(col, j);
                    SwitchToken(j, col);
                }
                //Console.WriteLine(i + " " + row);
            }
            return canPlay;
        }

        /// <summary>
        /// Cette fonction va regarder en haut à gauche (diagonal) d'une case pour vérifier s'il est possible de jouer
        /// </summary>
        /// <param name="row">ligne de la case</param>
        /// <param name="col">colonne de la case</param>
        /// <param name="switchTokens">S'il faut retourner les jetons</param>
        /// <returns></returns>
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
                //Console.WriteLine(colBase + ";" + rowBase + " " + col + ";" + row);
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

        /// <summary>
        /// Cette fonction va regarder en bas à droite (diagonal) d'une case pour vérifier s'il est possible de jouer
        /// </summary>
        /// <param name="row">ligne de la case</param>
        /// <param name="col">colonne de la case</param>
        /// <param name="switchTokens">S'il faut retourner les jetons</param>
        /// <returns></returns>
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
                //Console.WriteLine(colBase + ";" + rowBase + " " + col + ";" + row);
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

        /// <summary>
        /// Cette fonction va regarder en haut à droite (diagonal) d'une case pour vérifier s'il est possible de jouer
        /// </summary>
        /// <param name="row">ligne de la case</param>
        /// <param name="col">colonne de la case</param>
        /// <param name="switchTokens">S'il faut retourner les jetons</param>
        /// <returns></returns>
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
                //Console.WriteLine(colBase + ";" + rowBase + " " + col + ";" + row);
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

        /// <summary>
        /// Cette fonction va regarder en bas à gauche (diagonal) d'une case pour vérifier s'il est possible de jouer
        /// </summary>
        /// <param name="row">ligne de la case</param>
        /// <param name="col">colonne de la case</param>
        /// <param name="switchTokens">S'il faut retourner les jetons</param>
        /// <returns></returns>
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
                //Console.WriteLine(colBase + ";" + rowBase + " " + col + ";" + row);
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
            player1.Score = player2.Score = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board.CheckTokenEquals(i, j, player1.Number))
                        player1.Score++;
                    else if (board.CheckTokenEquals(i, j, player2.Number))
                        player2.Score++;
                }
            }

            lblPlayer1Score.Content = "Score : " + player1.Score;
            lblPlayer2Score.Content = "Score : " + player2.Score;
        }

        #endregion

        #region OtherFunction
        /// <summary>
        /// return number (1 or 2) of the actual player
        /// </summary>
        /// <returns>Player.number</returns>
        private int GetNumberPlayer()
        {
            return (isPlayer1 ? player1.Number : player2.Number);
        }

        /// <summary>
        /// Every time a label is clicked, check if the game is finish
        /// </summary>
        /// <returns></returns>
        private bool CheckIfFinish()
        {
            if (nbFreeCells == 0)
                return true;
            return false;
        }

        /// <summary>
        /// Function called when a game is finish.
        /// Pop-up a msgBox with the winner, try again or quit ?
        /// </summary>
        private void FinishFunction()
        {
            timerP1.Stop();
            timerP2.Stop();

            /*for(int i = 0; i < 7; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    Label lbl = GetChildren(tokenGrid, i, j) as Label;
                    lbl.MouseDown -= OnClickLabel;
                }
            }*/

            string msg;
            if (player1.Score > player2.Score)
            {
                msg = "Gagnant : player 1, " + player1.Name + "\nScore : " + player1.Score + "\nVoulez-vous recommencer ?";
                lblPlayerImgTurn.Background = player1.Token.ImgBrush;
            }
            else
            {
                msg = "Gagnant : player 2, " + player2.Name + "\nScore : " + player2.Score + "\nVoulez-vous recommencer ?";
                lblPlayerImgTurn.Background = player2.Token.ImgBrush;
            }

            var result = MessageBox.Show(msg, "Win box", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                ProperlyNewGame();
            }
            else
            {
                this.Close();
            }
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
        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            ProperlyNewGame();
        }

        /// <summary>
        /// Save method who writes all parameter to an extern XML file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            string filename = "", strBoard = "";

            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog
            {
                Title = "Save the game",
                DefaultExt = "xml",
                CheckPathExists = true,
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                filename = saveFileDialog.FileName;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    strBoard += board.GetTokenOnBoard(i, j);
                }
            };
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = ("\t"),
            };

            using (XmlWriter writer = XmlWriter.Create(filename, settings))
            {

                writer.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");

                writer.WriteStartElement("Game");

                writer.WriteStartElement("Player1");
                writer.WriteElementString("Name", player1.Name);
                writer.WriteElementString("Time", timerP1.Elapsed.ToString("dd\\:hh\\:mm\\:ss\\:ff"));
                writer.WriteElementString("Score", player1.Score.ToString());
                writer.WriteEndElement();


                writer.WriteStartElement("Player2");
                writer.WriteElementString("Name", player2.Name);
                writer.WriteElementString("Time", timerP2.Elapsed.ToString("dd\\:hh\\:mm\\:ss\\:ff"));
                writer.WriteElementString("Score", player2.Score.ToString());
                writer.WriteEndElement();

                writer.WriteElementString("Turn", turn.ToString());

                writer.WriteElementString("PlayerTurn", isPlayer1 == true ? "1" : "2");

                writer.WriteElementString("Board", strBoard);

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Flush();
                writer.Close();
            }

        }
        /// <summary>
        /// Load method who reads the XML file to To fill the struct with the read data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuLoad_Click(object sender, RoutedEventArgs e)
        {
            string p1Name, p2Name, p1Time, p2Time, strBoard, tmp;
            int p1Score, p2Score, turn, playerTurn;
            bool readPlayer1;
            int[,] tabBoard;

            p1Name = p2Name = p1Time = p2Time = tmp = null;
            strBoard = "";
            p1Score = p2Score = turn = playerTurn = 0;
            readPlayer1 = false;
            tabBoard = new int[7, 9];

            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml",
                FilterIndex = 0,
                DefaultExt = "xml"
            };

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!String.Equals(Path.GetExtension(ofd.FileName),
                                   ".xml",
                                   StringComparison.OrdinalIgnoreCase))
                {
                    // Invalid file type selected; display an error.
                    MessageBox.Show("You must select an XML file.",
                                    "Invalid File Type",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                }
                else
                {
                    XmlReaderSettings settings = new XmlReaderSettings
                    {
                        Async = true
                    };

                    using (XmlReader reader = XmlReader.Create(ofd.FileName, settings))
                    {
                        try
                        {
                            while (reader.Read())
                            {
                                if (reader.IsStartElement())
                                {
                                    switch (reader.Name)
                                    {
                                        case "Player1":
                                            Console.WriteLine("Start <Player1> element.");
                                            readPlayer1 = true;
                                            break;

                                        case "Player2":
                                            Console.WriteLine("Start <Player2> element.");
                                            readPlayer1 = false;
                                            break;

                                        case "Name":
                                            if (reader.Read())
                                                tmp = reader.Value.Trim();
                                            if (readPlayer1)
                                                p1Name = tmp;
                                            else
                                                p2Name = tmp;
                                            break;
                                        case "Time":
                                            if (reader.Read())
                                                tmp = reader.Value.Trim();
                                            if (readPlayer1)
                                                p1Time = tmp;
                                            else
                                                p2Time = tmp;
                                            break;
                                        case "Score":
                                            if (reader.Read())
                                                tmp = reader.Value.Trim();
                                            if (readPlayer1)
                                                int.TryParse(tmp, out p1Score);
                                            else
                                                int.TryParse(tmp, out p2Score);
                                            break;

                                        case "Board":
                                            if (reader.Read())
                                            {
                                                strBoard = reader.Value.Trim();
                                            }
                                            break;

                                        case "Turn":
                                            if (reader.Read())
                                                int.TryParse(reader.Value.Trim(), out turn);
                                            break;

                                        case "PlayerTurn":
                                            if (reader.Read())
                                                playerTurn = (reader.Value.Trim() == "1" ? 1 : 2);
                                            break;

                                    }
                                }
                            }
                        }
                        catch (XmlException)
                        {
                            MessageBox.Show("Error while reading XML file, please chose another one");
                            Debug.WriteLine("Error while reading XML file");
                        }

                        try
                        {
                            tabBoard = Board.StrToInt(strBoard);

                            TimeSpan t1 = new TimeSpan(int.Parse(p1Time.Split(':')[0]), int.Parse(p1Time.Split(':')[1]), int.Parse(p1Time.Split(':')[2]), int.Parse(p1Time.Split(':')[3]), int.Parse(p1Time.Split(':')[4]));
                            TimeSpan t2 = new TimeSpan(int.Parse(p1Time.Split(':')[0]), int.Parse(p1Time.Split(':')[1]), int.Parse(p2Time.Split(':')[2]), int.Parse(p2Time.Split(':')[3]), int.Parse(p2Time.Split(':')[4]));
                            GameParameter gameParameter = new GameParameter(p1Name, t1, p1Score, p2Name, t2, p2Score, tabBoard, turn, playerTurn);
                            ProperlyNewGame(gameParameter);
                        }
                        catch (NullReferenceException)
                        {
                            Debug.WriteLine("Error e");
                        }
                    }
                }
            }
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
        public void ProperlyNewGame(GameParameter g = default(GameParameter))
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

            board = null;

            if (g.Equals(default(GameParameter)))
            {
                Console.WriteLine("NEW GAME");
                InitializeGame();
                InitializeBoard(false);
            }
            else
            {
                Console.WriteLine("LOAD GAME");
                InitializeGame(g);
                InitializeBoard(true);
            }

        }

        #endregion

    }
}