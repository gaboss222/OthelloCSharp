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
    public partial class MainBox : Form
    {
        public MainBox()
        {
            InitializeComponent();
        }
        static DialogResult result = DialogResult.No;
        static MainBox mainBox;
        static string player1Name, player2Name;

        public DialogResult CustomShow()
        {
            mainBox = new MainBox();
            mainBox.ShowDialog();
            return result;
        }

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public string GetPlayerName(int player)
        {
            if (player == 1)
                return player1Name;
            else
                return player2Name;
        }
    }
}
