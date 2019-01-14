using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OthelloAlainGabriel
{
    /// <summary>
    /// Custom Menu form
    /// </summary>
    public partial class MainBox : Form
    {
        #region attributes
        static DialogResult result = DialogResult.No;
        static MainBox mainBox;
        static private string player1Name, player2Name;
        static private string player1TokenPath = "";
        static private string player2TokenPath = "";
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainBox()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// CustomShow method. Show the form
        /// </summary>
        /// <returns>Return dialogResult (Play or not)</returns>
        public DialogResult CustomShow()
        {
            mainBox = new MainBox();
            mainBox.ShowDialog();
            return result;
        }

        /// <summary>
        /// Click function to Token1 button. Choose token for player1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToken1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "png files (*.png)|*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                player1TokenPath = ofd.FileName;
                btnToken1.Text = ofd.SafeFileName;
            }
        }

        /// <summary>
        /// Click function to Token1 button. Choose token for player1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToken2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "png files (*.png)|*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                player2TokenPath = ofd.FileName;
                btnToken2.Text = ofd.SafeFileName;
            }
        }

        /// <summary>
        /// Click function for validate button. Play the game if names are not empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxNamePlayer1.Text) || string.IsNullOrWhiteSpace(tbxNamePlayer2.Text))
            {
                result = DialogResult.No;
                MessageBox.Show("Enter a valid name");
            }
            else
            {
                try
                {
                    player1Name = tbxNamePlayer1.Text;
                    player2Name = tbxNamePlayer2.Text;
                    result = DialogResult.Yes;
                    mainBox.Close();
                }
                catch (NullReferenceException)
                {

                }
            }
        }

        /// <summary>
        /// Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Getter for player's names
        /// </summary>
        /// <param name="player"></param>
        /// <returns>Name of player choosen</returns>
        public string GetPlayerName(int player)
        {
            if (player == 1)
                return player1Name;
            else
                return player2Name;
        }

        /// <summary>
        /// Getter for player's path
        /// </summary>
        /// <param name="player"></param>
        /// <returns>Path to token for player </returns>
        public string GetPlayerTokenPath(int player)
        {
            if (player == 1)
                return player1TokenPath;
            else
                return player2TokenPath;
        }

        /// <summary>
        /// Reset path to token for each player (used when new game)
        /// </summary>
        public void ResetPlayerTokenPath()
        {
            player1TokenPath = "";
            player2TokenPath = "";
        }
    }
}
