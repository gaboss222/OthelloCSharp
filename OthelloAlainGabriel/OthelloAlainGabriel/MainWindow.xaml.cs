using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OthelloAlainGabriel
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializePlayer();
            InitializeComponent();
            InitializeButton();
        }
        #region Property
        Player player1, player2;
        Token token1, token2;
        #endregion

        #region Attribute
        bool isPlayer1;
        #endregion

        private void InitializePlayer()
        {
            token1 = new Token(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Assets\Tokens\token1.png"));
            token2 = new Token(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Assets\Tokens\token2.png"));

            player1 = new Player(token1);
            player2 = new Player(token2);

            isPlayer1 = true;
        }

        private void InitializeButton()
        {
            for (int i = 0; i < 7; i++)
            {
                tokenGrid.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < 9; j++)
                {

                    Label lbl = new Label();
                    lbl.ToolTip = ((Char)(j + 65)) + "" + (i + 1);
                    lbl.Name = lbl.ToolTip.ToString();
                    lbl.MouseDown += Btn_Click;
                    lbl.BorderThickness = new Thickness(0.1, 0.1, 0.1, 0.1);
                    lbl.BorderBrush = Brushes.LightGray;
                    if (tokenGrid.ColumnDefinitions.Count < 8)
                        tokenGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(lbl, j);
                    Grid.SetRow(lbl, i);
                    tokenGrid.Children.Add(lbl);

                    if ((i == 3 && j == 3) || (i == 4 && j == 4))
                    {
                        lbl.Background = player2.Token.ImgBrush;
                        lbl.MouseDown -= Btn_Click;
                    }
                    if ((i == 3 && j == 4) || (i == 4 && j == 3))
                    {
                        lbl.Background = player1.Token.ImgBrush;
                        lbl.MouseDown -= Btn_Click;
                    }
                }
            }

            //HERE --> Ajouter les tokens de base (les 4 du centre)
            //HERE --> Gérer clic du bouton (quand on clic, ajout d'un token --> Player à update
        }

        #region ButtonFunction
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Label lbl = sender as Label;
            Console.WriteLine((sender as Label).Name);
            if (isPlayer1)
            {
                lbl.Background = player1.Token.ImgBrush;
                isPlayer1 = false;
            }
            else
            {
                lbl.Background = player2.Token.ImgBrush;
                isPlayer1 = true;
            }
            lbl.MouseDown -= Btn_Click;
        }
        #endregion

        #region MenuFunction       
        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {

        }
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
        #endregion
    }
}