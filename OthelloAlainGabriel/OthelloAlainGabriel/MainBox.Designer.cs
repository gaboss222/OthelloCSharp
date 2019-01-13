namespace OthelloAlainGabriel
{
    partial class MainBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPlayer2Name = new System.Windows.Forms.Label();
            this.lblPlayer1Name = new System.Windows.Forms.Label();
            this.tbxNamePlayer1 = new System.Windows.Forms.TextBox();
            this.tbxNamePlayer2 = new System.Windows.Forms.TextBox();
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName1
            // 
            this.lblName1.AutoSize = true;
            this.lblName1.Font = new System.Drawing.Font("Arial Black", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName1.Location = new System.Drawing.Point(115, 27);
            this.lblName1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName1.Name = "lblName1";
            this.lblName1.Size = new System.Drawing.Size(257, 48);
            this.lblName1.TabIndex = 0;
            this.lblName1.Text = "OTHELLO C#";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(365, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Veuillez rentrer les noms des joueurs :";
            // 
            // lblPlayer2Name
            // 
            this.lblPlayer2Name.AutoSize = true;
            this.lblPlayer2Name.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer2Name.Location = new System.Drawing.Point(300, 137);
            this.lblPlayer2Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlayer2Name.Name = "lblPlayer2Name";
            this.lblPlayer2Name.Size = new System.Drawing.Size(145, 33);
            this.lblPlayer2Name.TabIndex = 2;
            this.lblPlayer2Name.Text = "Joueur 2 :";
            // 
            // lblPlayer1Name
            // 
            this.lblPlayer1Name.AutoSize = true;
            this.lblPlayer1Name.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer1Name.Location = new System.Drawing.Point(51, 137);
            this.lblPlayer1Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlayer1Name.Name = "lblPlayer1Name";
            this.lblPlayer1Name.Size = new System.Drawing.Size(145, 33);
            this.lblPlayer1Name.TabIndex = 3;
            this.lblPlayer1Name.Text = "Joueur 1 :";
            // 
            // tbxNamePlayer1
            // 
            this.tbxNamePlayer1.Location = new System.Drawing.Point(57, 190);
            this.tbxNamePlayer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxNamePlayer1.Name = "tbxNamePlayer1";
            this.tbxNamePlayer1.Size = new System.Drawing.Size(147, 22);
            this.tbxNamePlayer1.TabIndex = 4;
            // 
            // tbxNamePlayer2
            // 
            this.tbxNamePlayer2.Location = new System.Drawing.Point(307, 190);
            this.tbxNamePlayer2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxNamePlayer2.Name = "tbxNamePlayer2";
            this.tbxNamePlayer2.Size = new System.Drawing.Size(147, 22);
            this.tbxNamePlayer2.TabIndex = 5;
            // 
            // btnValidate
            // 
            this.btnValidate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidate.Location = new System.Drawing.Point(307, 236);
            this.btnValidate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(148, 37);
            this.btnValidate.TabIndex = 6;
            this.btnValidate.Text = "Valider";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(57, 236);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(148, 37);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Quitter";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // MainBox
            // 
            this.AcceptButton = this.btnValidate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(525, 311);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.tbxNamePlayer2);
            this.Controls.Add(this.tbxNamePlayer1);
            this.Controls.Add(this.lblPlayer1Name);
            this.Controls.Add(this.lblPlayer2Name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblName1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "MainBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello C#";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPlayer2Name;
        private System.Windows.Forms.Label lblPlayer1Name;
        private System.Windows.Forms.TextBox tbxNamePlayer1;
        private System.Windows.Forms.TextBox tbxNamePlayer2;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Button btnCancel;
    }
}