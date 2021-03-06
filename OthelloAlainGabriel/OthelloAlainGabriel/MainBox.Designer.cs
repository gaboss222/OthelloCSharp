﻿namespace OthelloAlainGabriel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainBox));
            this.lblName1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPlayer2Name = new System.Windows.Forms.Label();
            this.lblPlayer1Name = new System.Windows.Forms.Label();
            this.tbxNamePlayer1 = new System.Windows.Forms.TextBox();
            this.tbxNamePlayer2 = new System.Windows.Forms.TextBox();
            this.btnValidate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnToken1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnToken2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName1
            // 
            this.lblName1.AutoSize = true;
            this.lblName1.Font = new System.Drawing.Font("Arial Black", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName1.Location = new System.Drawing.Point(86, 22);
            this.lblName1.Name = "lblName1";
            this.lblName1.Size = new System.Drawing.Size(206, 38);
            this.lblName1.TabIndex = 0;
            this.lblName1.Text = "OTHELLO C#";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please enter the names for each player";
            // 
            // lblPlayer2Name
            // 
            this.lblPlayer2Name.AutoSize = true;
            this.lblPlayer2Name.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer2Name.Location = new System.Drawing.Point(225, 111);
            this.lblPlayer2Name.Name = "lblPlayer2Name";
            this.lblPlayer2Name.Size = new System.Drawing.Size(109, 27);
            this.lblPlayer2Name.TabIndex = 2;
            this.lblPlayer2Name.Text = "Player 2 :";
            // 
            // lblPlayer1Name
            // 
            this.lblPlayer1Name.AutoSize = true;
            this.lblPlayer1Name.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer1Name.Location = new System.Drawing.Point(38, 111);
            this.lblPlayer1Name.Name = "lblPlayer1Name";
            this.lblPlayer1Name.Size = new System.Drawing.Size(109, 27);
            this.lblPlayer1Name.TabIndex = 3;
            this.lblPlayer1Name.Text = "Player 1 :";
            // 
            // tbxNamePlayer1
            // 
            this.tbxNamePlayer1.Location = new System.Drawing.Point(43, 154);
            this.tbxNamePlayer1.Name = "tbxNamePlayer1";
            this.tbxNamePlayer1.Size = new System.Drawing.Size(111, 20);
            this.tbxNamePlayer1.TabIndex = 4;
            // 
            // tbxNamePlayer2
            // 
            this.tbxNamePlayer2.Location = new System.Drawing.Point(230, 154);
            this.tbxNamePlayer2.Name = "tbxNamePlayer2";
            this.tbxNamePlayer2.Size = new System.Drawing.Size(111, 20);
            this.tbxNamePlayer2.TabIndex = 5;
            // 
            // btnValidate
            // 
            this.btnValidate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidate.Location = new System.Drawing.Point(230, 315);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(111, 30);
            this.btnValidate.TabIndex = 6;
            this.btnValidate.Text = "Play !";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(43, 315);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(111, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Exit";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnToken1
            // 
            this.btnToken1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnToken1.Location = new System.Drawing.Point(43, 239);
            this.btnToken1.Name = "btnToken1";
            this.btnToken1.Size = new System.Drawing.Size(111, 30);
            this.btnToken1.TabIndex = 8;
            this.btnToken1.Text = "Choose token";
            this.btnToken1.UseVisualStyleBackColor = true;
            this.btnToken1.Click += new System.EventHandler(this.btnToken1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(316, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "Optional, choose a token for each player";
            // 
            // btnToken2
            // 
            this.btnToken2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnToken2.Location = new System.Drawing.Point(230, 239);
            this.btnToken2.Name = "btnToken2";
            this.btnToken2.Size = new System.Drawing.Size(111, 30);
            this.btnToken2.TabIndex = 10;
            this.btnToken2.Text = "Choose token";
            this.btnToken2.UseVisualStyleBackColor = true;
            this.btnToken2.Click += new System.EventHandler(this.btnToken2_Click);
            // 
            // MainBox
            // 
            this.AcceptButton = this.btnValidate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(211)))), ((int)(((byte)(179)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(393, 384);
            this.Controls.Add(this.btnToken2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnToken1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.tbxNamePlayer2);
            this.Controls.Add(this.tbxNamePlayer1);
            this.Controls.Add(this.lblPlayer1Name);
            this.Controls.Add(this.lblPlayer2Name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblName1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.Button btnToken1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnToken2;
    }
}