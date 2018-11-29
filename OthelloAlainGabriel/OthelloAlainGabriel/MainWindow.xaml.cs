using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
            InitializeComponent();
            
            for (int i = 0; i < 7; i ++)
            {
                tokenGrid.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < 9; j++)
                {
                    Button btn = new Button();
                    btn.ToolTip = ((Char)(j + 65)) + "" + (i+1);
                    btn.Name = btn.ToolTip.ToString();
                    btn.Click += Btn_Click;
                    if (tokenGrid.ColumnDefinitions.Count < 8)
                        tokenGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(btn, j);
                    Grid.SetRow(btn, i);
                    tokenGrid.Children.Add(btn);
                    btn.Background = Brushes.LightGreen;
                }
            }
        }
        #region Property
        Player player1, player2;
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

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine((sender as Button).Name);
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("About : GG and AG Othello");
        }
        #endregion
    }
}