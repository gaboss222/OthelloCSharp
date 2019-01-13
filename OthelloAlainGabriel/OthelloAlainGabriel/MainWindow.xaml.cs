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
            mainBox = new MainBox();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (mainBox.CustomShow() == System.Windows.Forms.DialogResult.Yes)
            {
                Console.WriteLine(mainBox.GetPlayerName(1));
                InitializeComponent();
                InitializeGame();
                InitializeBoard(false);
            }
            else
            {
                Close();
            }

        }

        #region Property
        Player player1, player2;
        Token token1, token2;
        Board board;
        Rules rules;
        #endregion
        #region Attribute

        MainBox mainBox;
        private bool isPlayer1;
        private MyStopwatch timerP1, timerP2;
        private Timer timerUpdate;
        private int nbFreeCells;
        private int turn;
        public const int NB_COL = 9;
        public const int NB_ROW = 7;
        #endregion

        #region Game
        /// <summary>
        /// Struct defined each time a game is loaded
        /// We will put all the data read in this struct
        /// </summary>
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
                newBoard = new int[NB_ROW, NB_COL];
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

        /// <summary>
        /// Function called when a new game begins
        /// A new game can begin when a player click on "New Game" button
        /// or when a player loads a XML file that contains data from another game
        /// </summary>
        /// <param name="g">g = default if "new game" button is clicked</param>
        public void ProperlyNewGame(GameParameter g = default(GameParameter))
        {
            for (int i = 0; i < NB_ROW; i++)
            {
                for (int j = 0; j < NB_COL; j++)
                {
                    // TODO HERE CHANGE LBL BACKGROUND COLOR + RESET a ZERO LES CASES ET REMETTRES LES 4 TOKENS DU DEBUT
                    // 1 Fonction
                    Label lbl = rules.GetLabel(tokenGrid, i, j);
                    tokenGrid.Children.Remove(lbl);
                }
            }
            timerP1.Stop();
            timerP2.Stop();
            board = null;

            //New game = default GameParameter struc (no data loaded)
            if (g.Equals(default(GameParameter)))
            {
                mainBox = new MainBox();
                if (mainBox.CustomShow() == System.Windows.Forms.DialogResult.Yes)
                {
                    Console.WriteLine(mainBox.GetPlayerName(1));
                    InitializeComponent();
                    InitializeGame();
                    InitializeBoard(false);
                }
                else
                {
                    Close();
                }
            }
            else
            //data loaded from XML file
            {
                MessageBox.Show("Game loaded : \nPlayer1 : " + g.p1Name + ", Score : " + g.p1Score + "\nPlayer2 : " + g.p2Name + ", Score : " + g.p2Score);
                InitializeGame(g);
                InitializeBoard(true);
            }

        }

        /// <summary>
        /// Function that will instanciate all the parameters/attributes necessary for a game.
        /// </summary>
        /// <param name="g">g = default if "new game" button is clicked</param>
        private void InitializeGame(GameParameter g = default(GameParameter))
        {
            board = new Board(NB_ROW, NB_COL);

            token1 = new Token(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Assets\Tokens\token1.png"));
            token2 = new Token(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Assets\Tokens\token2.png"));

            //New game
            if (g.Equals(default(GameParameter)))
            {
                player1 = new Player(token1, mainBox.GetPlayerName(1), 1);
                player2 = new Player(token2, mainBox.GetPlayerName(2), 2);

                timerP1 = new MyStopwatch();
                timerP2 = new MyStopwatch();

                lblPlayer1Score.DataContext = new Player { Score = player1.Score };
                lblPlayer2Score.DataContext = new Player { Score = player2.Score };

                lblPlayerTurn.Content = player1.Name + "'s turn :";

                isPlayer1 = true;
                nbFreeCells = (NB_ROW * NB_COL) - 4;
                turn = 1;
            }
            //Loaded game. Attributes/Properties will be charged by data in GameParameter struct
            else
            {
                player1 = new Player(token1, g.p1Name, 1);
                player2 = new Player(token2, g.p2Name, 2);

                board.SetBoard(g.newBoard);

                if (g.playerTurn == 2)
                {
                    isPlayer1 = false;
                    lblPlayerTurn.Content = player2.Name + "'s turn :";
                    lblPlayerImgTurn.Background = player2.Token.ImgBrush;
                }
                else
                {
                    isPlayer1 = true;
                    lblPlayerTurn.Content = player1.Name + "'s turn :";
                    lblPlayerImgTurn.Background = player1.Token.ImgBrush;
                }

                timerP1 = g.p1Stopwatch;
                timerP2 = g.p2Stopwatch;

                if (isPlayer1)
                    timerP1.Start();
                else
                    timerP2.Start();

                nbFreeCells = board.GetFreeCells();
                turn = g.turn;
            }

            lblPlayer1.Content = player1.Name;
            lblPlayer2.Content = player2.Name;
            CheckScore();

            //Timer used to update lblPlayer1/2Time
            timerUpdate = new Timer(10);
            timerUpdate.Elapsed += Timer_tick;
            timerUpdate.Start();

            // Initialize Rules
            rules = new Rules(board, player1, player2, tokenGrid, NB_ROW, NB_COL);
        }

        /// <summary>
        /// Function used to initialize the board. Create label, define parameters and row/column
        /// </summary>
        /// <param name="loadedGame">If loadedGame, create board from data</param>
        private void InitializeBoard(bool loadedGame)
        {
            tokenGrid.Background = board.backgroundBrush;
            for (int i = 0; i < NB_ROW; i++)
            {
                tokenGrid.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < NB_COL; j++)
                {
                    Label lbl = new Label
                    {
                        Name = "i" + i + "j" + j
                    };
                    lbl.MouseDown += OnClickLabel;
                    lbl.MouseEnter += OnEnterLabel;
                    lbl.MouseLeave += OnLeaveLabel;
                    lbl.BorderThickness = new Thickness(0.1, 0.1, 0.1, 0.1);
                    lbl.BorderBrush = Brushes.White;

                    if (tokenGrid.ColumnDefinitions.Count < NB_COL)
                        tokenGrid.ColumnDefinitions.Add(new ColumnDefinition());

                    Grid.SetColumn(lbl, j);
                    Grid.SetRow(lbl, i);
                    tokenGrid.Children.Add(lbl);

                    //In this case, we put the 4 firsts tokens
                    if (loadedGame == false)
                    {
                        if ((i == 3 && j == 3) || (i == 4 && j == 4))
                        {
                            lbl.Background = player2.Token.ImgBrush;
                            lbl.MouseDown -= OnClickLabel;
                            board.SetNumberOnBoard(i, j, player2);
                        }
                        if ((i == 3 && j == 4) || (i == 4 && j == 3))
                        {
                            lbl.Background = player1.Token.ImgBrush;
                            lbl.MouseDown -= OnClickLabel;
                            board.SetNumberOnBoard(i, j, player1);
                        }
                        lblPlayerImgTurn.Background = player1.Token.ImgBrush;
                        timerP1.Start();
                    }
                    //In this case, board already exists. We need to check each case to put tokens
                    else
                    {
                        if (board.GetNumberOnBoard(i, j) == 1)
                        {
                            Console.WriteLine("TOKEN1");
                            lbl.Background = player1.Token.ImgBrush;
                            lbl.MouseDown -= OnClickLabel;
                        }
                        else if (board.GetNumberOnBoard(i, j) == 2)
                        {
                            Console.WriteLine("TOKEN2");

                            lbl.Background = player2.Token.ImgBrush;
                            lbl.MouseDown -= OnClickLabel;
                        }
                    }

                }
            }
            gridPlayerTurn.Background = board.backgroundBrush;
            rules.CheckCases();
        }

        #endregion

        #region FormsFunction
        /// <summary>
        /// OnClick function. If label is free (checkCase), put token on label (UpdateBoard())
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickLabel(object sender, RoutedEventArgs e)
        {
            Label lbl = sender as Label;

            int row = Grid.GetRow(lbl);
            int col = Grid.GetColumn(lbl);

            //If free case
            if (rules.CheckCase(row, col, false))
            {
                rules.CheckCase(row, col, true);
                if (isPlayer1)
                    UpdateBoard(row, col, lbl, player1);
                else
                    UpdateBoard(row, col, lbl, player2);

                nbFreeCells--;
                lbl.MouseDown -= OnClickLabel;
            }

            CheckScore();
            if (CheckIfWin())
            {
                FinishFunction();
                return;
            }

            if (!rules.CheckCases())
            {
                ChangeTurn();
                if (!rules.CheckCases())
                {
                    FinishFunction();
                }
            }
        }

        /// <summary>
        /// Put the player's token when the mouse flies over the label 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEnterLabel(object sender, RoutedEventArgs e)
        {
            Label lbl = sender as Label;
            int row = (int)Char.GetNumericValue(lbl.Name[1]);
            int col = (int)Char.GetNumericValue(lbl.Name[3]);
            if (rules.CheckCase(row, col, false))
            {
                lbl.Background = isPlayer1 ? player1.Token.ImgBrush : player2.Token.ImgBrush;
            }
        }

        /// <summary>
        /// Remove player's token when the mouse flies off the label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLeaveLabel(object sender, RoutedEventArgs e)
        {
            Label lbl = sender as Label;
            int row = (int)Char.GetNumericValue(lbl.Name[1]);
            int col = (int)Char.GetNumericValue(lbl.Name[3]);
            if (rules.CheckCase(row, col, false))
            {
                lbl.Background = board.hoverBrush;
            }
        }

        /// <summary>
        /// Called each time a label is clicked
        /// Update board (Change color, change label player, change token)
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="col">Column</param>
        /// <param name="lbl">Label to update</param>
        /// <param name="p">Current player</param>
        private void UpdateBoard(int row, int col, Label lbl, Player p)
        {
            lbl.Background = p.Token.ImgBrush;
            board.SetNumberOnBoard(row, col, p);
            ChangeTurn();
        }

        /// <summary>
        /// Complementary to the function above
        /// </summary>
        private void ChangeTurn()
        {
            if (isPlayer1)
            {
                isPlayer1 = false;
                timerP1.Stop();
                timerP2.Start();
                lblPlayerImgTurn.Background = player2.Token.ImgBrush;
                lblPlayerTurn.Content = player2.Name + "'s turn :";
                turn++;
            }
            else
            {
                isPlayer1 = true;
                timerP2.Stop();
                timerP1.Start();
                lblPlayerImgTurn.Background = player1.Token.ImgBrush;
                lblPlayerTurn.Content = player1.Name + "'s turn :";
            }
            rules.ChangeTurn();
        }

        /// <summary>
        /// Update continuously labelTime with time from StopWatch1 and StopWatch2 (timerP1 and timerP2)
        /// Using Dispatcher to avoid blocking
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

        /// <summary>
        /// Count number of tokenPlayer1 (1) and tokenPlayer2 (2).
        /// Update score. Update lblScore via dataBinding (DataContext)
        /// </summary>
        private void CheckScore()
        {
            player1.Score = player2.Score = 0;
            for (int i = 0; i < NB_ROW; i++)
            {
                for (int j = 0; j < NB_COL; j++)
                {
                    if (board.CheckTokenEquals(i, j, player1.Number))
                        player1.Score++;
                    else if (board.CheckTokenEquals(i, j, player2.Number))
                        player2.Score++;
                }
            }

            //DataBinding
            lblPlayer1Score.DataContext = new Player { Score = player1.Score };
            lblPlayer2Score.DataContext = new Player { Score = player2.Score };
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
            if (isPlayer1)
                timerP1.Stop();
            else
                timerP2.Stop();

            string filename = "", strBoard = "";

            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog
            {
                Title = "Save the game",
                DefaultExt = "xml",
                CheckPathExists = true,
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = saveFileDialog.FileName;

                strBoard = board.ToString();

                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = ("\t"),
                };

                //Write data on an external XML file
                try
                {
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

                        if (isPlayer1)
                            timerP1.Start();
                        else
                            timerP2.Start();
                    }
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Error while writing XML file");
                }
                
            }

           
        }

        /// <summary>
        /// Load method who reads the XML file to To fill the struct with the read data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuLoad_Click(object sender, RoutedEventArgs e)
        {
            if (isPlayer1)
                timerP1.Stop();
            else
                timerP2.Stop();

            //Create and instanciate all game parameters
            string p1Name, p2Name, p1Time, p2Time, strBoard, tmp;
            int p1Score, p2Score, turn, playerTurn;
            bool readPlayer1;
            int[,] tabBoard;

            p1Name = p2Name = p1Time = p2Time = tmp = null;
            strBoard = "";
            p1Score = p2Score = turn = playerTurn = 0;
            readPlayer1 = false;
            tabBoard = new int[NB_ROW, NB_COL];

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
                                            readPlayer1 = true;
                                            break;

                                        case "Player2":
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
                        }

                        try
                        {
                            tabBoard = board.StrToInt(strBoard);

                            TimeSpan t1 = new TimeSpan(int.Parse(p1Time.Split(':')[0]), int.Parse(p1Time.Split(':')[1]), int.Parse(p1Time.Split(':')[2]), int.Parse(p1Time.Split(':')[3]), int.Parse(p1Time.Split(':')[4]));
                            TimeSpan t2 = new TimeSpan(int.Parse(p1Time.Split(':')[0]), int.Parse(p1Time.Split(':')[1]), int.Parse(p2Time.Split(':')[2]), int.Parse(p2Time.Split(':')[3]), int.Parse(p2Time.Split(':')[4]));
                            GameParameter gameParameter = new GameParameter(p1Name, t1, p1Score, p2Name, t2, p2Score, tabBoard, turn, playerTurn);
                            ProperlyNewGame(gameParameter);
                        }
                        catch (NullReferenceException)
                        {
                            Debug.WriteLine("NullReference Exception");
                        }
                    }
                }
            }
            else
            {
                if (isPlayer1)
                    timerP1.Start();
                else
                    timerP2.Start();
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

        /// <summary>
        /// Function to cancel a move
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuUndo_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// About the app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("About : Griesser Gabriel and Graber Alain.\n Othello C# 2018-2019.\n HE-ARC");
        }

        /// <summary>
        /// Properly close application
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion

        #region OtherFunction


        /// <summary>
        /// Every time a token is put, check if the current player wins.
        /// Called each time a token is put
        /// </summary>
        /// <returns></returns>
        private bool CheckIfWin()
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

            //New game
            if (result == MessageBoxResult.Yes)
            {
                ProperlyNewGame();
            }
            else
            {
                this.Close();
            }
        }
        #endregion


    }
}