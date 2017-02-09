namespace TicTacToe.View
{
    partial class WelcomeScreen
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
            this.btn1P = new System.Windows.Forms.Button();
            this.btn2P = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn1P
            // 
            this.btn1P.BackColor = System.Drawing.Color.Ivory;
            this.btn1P.Location = new System.Drawing.Point(108, 35);
            this.btn1P.Name = "btn1P";
            this.btn1P.Size = new System.Drawing.Size(183, 51);
            this.btn1P.TabIndex = 0;
            this.btn1P.Text = "1 Player";
            this.btn1P.UseVisualStyleBackColor = false;
            this.btn1P.Click += new System.EventHandler(this.btn1P_Click);
            // 
            // btn2P
            // 
            this.btn2P.BackColor = System.Drawing.Color.Ivory;
            this.btn2P.Location = new System.Drawing.Point(108, 117);
            this.btn2P.Name = "btn2P";
            this.btn2P.Size = new System.Drawing.Size(183, 51);
            this.btn2P.TabIndex = 1;
            this.btn2P.Text = "2 Players";
            this.btn2P.UseVisualStyleBackColor = false;
            this.btn2P.Click += new System.EventHandler(this.btn2P_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Ivory;
            this.btnSettings.Location = new System.Drawing.Point(108, 200);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(183, 51);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Ivory;
            this.btnExit.Location = new System.Drawing.Point(109, 282);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(183, 51);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // WelcomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(401, 377);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btn2P);
            this.Controls.Add(this.btn1P);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "WelcomeScreen";
            this.Text = "Tic Tac Toe";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn1P;
        private System.Windows.Forms.Button btn2P;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnExit;
    }
}

