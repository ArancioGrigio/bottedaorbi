namespace testoop
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.selectCharacter1 = new System.Windows.Forms.ComboBox();
            this.labelVS = new System.Windows.Forms.Label();
            this.selectCharacter2 = new System.Windows.Forms.ComboBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.picturePlayer1 = new System.Windows.Forms.PictureBox();
            this.picturePlayer2 = new System.Windows.Forms.PictureBox();
            this.buttonMove1 = new System.Windows.Forms.Button();
            this.buttonMove2 = new System.Windows.Forms.Button();
            this.buttonMove3 = new System.Windows.Forms.Button();
            this.buttonMove4 = new System.Windows.Forms.Button();
            this.labelVita1 = new System.Windows.Forms.Label();
            this.labelVita2 = new System.Windows.Forms.Label();
            this.labelNome1 = new System.Windows.Forms.Label();
            this.labelNome2 = new System.Windows.Forms.Label();
            this.labelSpeech = new System.Windows.Forms.Label();
            this.timerEntry = new System.Windows.Forms.Timer(this.components);
            this.timerEnding = new System.Windows.Forms.Timer(this.components);
            this.bt_restart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picturePlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturePlayer2)).BeginInit();
            this.SuspendLayout();
            // 
            // selectCharacter1
            // 
            this.selectCharacter1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.selectCharacter1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectCharacter1.FormattingEnabled = true;
            this.selectCharacter1.Location = new System.Drawing.Point(229, 233);
            this.selectCharacter1.Name = "selectCharacter1";
            this.selectCharacter1.Size = new System.Drawing.Size(121, 21);
            this.selectCharacter1.TabIndex = 0;
            // 
            // labelVS
            // 
            this.labelVS.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelVS.AutoSize = true;
            this.labelVS.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVS.Location = new System.Drawing.Point(359, 203);
            this.labelVS.Name = "labelVS";
            this.labelVS.Size = new System.Drawing.Size(96, 55);
            this.labelVS.TabIndex = 1;
            this.labelVS.Text = "Vs.";
            // 
            // selectCharacter2
            // 
            this.selectCharacter2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.selectCharacter2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectCharacter2.FormattingEnabled = true;
            this.selectCharacter2.Location = new System.Drawing.Point(458, 233);
            this.selectCharacter2.Name = "selectCharacter2";
            this.selectCharacter2.Size = new System.Drawing.Size(121, 21);
            this.selectCharacter2.TabIndex = 2;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonStart.Location = new System.Drawing.Point(348, 261);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(113, 38);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "COMBATTI!";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // picturePlayer1
            // 
            this.picturePlayer1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picturePlayer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picturePlayer1.Location = new System.Drawing.Point(30, 48);
            this.picturePlayer1.Name = "picturePlayer1";
            this.picturePlayer1.Size = new System.Drawing.Size(192, 171);
            this.picturePlayer1.TabIndex = 4;
            this.picturePlayer1.TabStop = false;
            this.picturePlayer1.Visible = false;
            // 
            // picturePlayer2
            // 
            this.picturePlayer2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picturePlayer2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picturePlayer2.Location = new System.Drawing.Point(584, 48);
            this.picturePlayer2.Name = "picturePlayer2";
            this.picturePlayer2.Size = new System.Drawing.Size(192, 171);
            this.picturePlayer2.TabIndex = 5;
            this.picturePlayer2.TabStop = false;
            this.picturePlayer2.Visible = false;
            // 
            // buttonMove1
            // 
            this.buttonMove1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonMove1.Enabled = false;
            this.buttonMove1.Location = new System.Drawing.Point(30, 360);
            this.buttonMove1.Name = "buttonMove1";
            this.buttonMove1.Size = new System.Drawing.Size(253, 76);
            this.buttonMove1.TabIndex = 6;
            this.buttonMove1.Text = "button1";
            this.buttonMove1.UseVisualStyleBackColor = true;
            this.buttonMove1.Visible = false;
            this.buttonMove1.Click += new System.EventHandler(this.buttonMove1_Click);
            // 
            // buttonMove2
            // 
            this.buttonMove2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonMove2.Enabled = false;
            this.buttonMove2.Location = new System.Drawing.Point(523, 360);
            this.buttonMove2.Name = "buttonMove2";
            this.buttonMove2.Size = new System.Drawing.Size(253, 76);
            this.buttonMove2.TabIndex = 7;
            this.buttonMove2.Text = "button2";
            this.buttonMove2.UseVisualStyleBackColor = true;
            this.buttonMove2.Visible = false;
            this.buttonMove2.Click += new System.EventHandler(this.buttonMove2_Click);
            // 
            // buttonMove3
            // 
            this.buttonMove3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonMove3.Enabled = false;
            this.buttonMove3.Location = new System.Drawing.Point(30, 442);
            this.buttonMove3.Name = "buttonMove3";
            this.buttonMove3.Size = new System.Drawing.Size(253, 76);
            this.buttonMove3.TabIndex = 8;
            this.buttonMove3.Text = "button3";
            this.buttonMove3.UseVisualStyleBackColor = true;
            this.buttonMove3.Visible = false;
            this.buttonMove3.Click += new System.EventHandler(this.buttonMove3_Click);
            // 
            // buttonMove4
            // 
            this.buttonMove4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonMove4.Enabled = false;
            this.buttonMove4.Location = new System.Drawing.Point(523, 442);
            this.buttonMove4.Name = "buttonMove4";
            this.buttonMove4.Size = new System.Drawing.Size(253, 76);
            this.buttonMove4.TabIndex = 9;
            this.buttonMove4.Text = "button4";
            this.buttonMove4.UseVisualStyleBackColor = true;
            this.buttonMove4.Visible = false;
            this.buttonMove4.Click += new System.EventHandler(this.buttonMove4_Click);
            // 
            // labelVita1
            // 
            this.labelVita1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelVita1.AutoSize = true;
            this.labelVita1.Location = new System.Drawing.Point(27, 32);
            this.labelVita1.Name = "labelVita1";
            this.labelVita1.Size = new System.Drawing.Size(53, 13);
            this.labelVita1.TabIndex = 10;
            this.labelVita1.Text = "labelVita1";
            this.labelVita1.Visible = false;
            // 
            // labelVita2
            // 
            this.labelVita2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelVita2.AutoSize = true;
            this.labelVita2.Location = new System.Drawing.Point(584, 32);
            this.labelVita2.Name = "labelVita2";
            this.labelVita2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelVita2.Size = new System.Drawing.Size(53, 13);
            this.labelVita2.TabIndex = 11;
            this.labelVita2.Text = "labelVita2";
            this.labelVita2.Visible = false;
            // 
            // labelNome1
            // 
            this.labelNome1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelNome1.AutoSize = true;
            this.labelNome1.Location = new System.Drawing.Point(27, 19);
            this.labelNome1.Name = "labelNome1";
            this.labelNome1.Size = new System.Drawing.Size(63, 13);
            this.labelNome1.TabIndex = 12;
            this.labelNome1.Text = "labelNome1";
            this.labelNome1.Visible = false;
            // 
            // labelNome2
            // 
            this.labelNome2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelNome2.AutoSize = true;
            this.labelNome2.Location = new System.Drawing.Point(584, 19);
            this.labelNome2.Name = "labelNome2";
            this.labelNome2.Size = new System.Drawing.Size(63, 13);
            this.labelNome2.TabIndex = 13;
            this.labelNome2.Text = "labelNome2";
            this.labelNome2.Visible = false;
            // 
            // labelSpeech
            // 
            this.labelSpeech.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelSpeech.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSpeech.Location = new System.Drawing.Point(30, 233);
            this.labelSpeech.Name = "labelSpeech";
            this.labelSpeech.Size = new System.Drawing.Size(746, 124);
            this.labelSpeech.TabIndex = 14;
            this.labelSpeech.Visible = false;
            // 
            // timerEntry
            // 
            this.timerEntry.Interval = 6000;
            this.timerEntry.Tick += new System.EventHandler(this.timerEntry_Tick);
            // 
            // timerEnding
            // 
            this.timerEnding.Interval = 3000;
            this.timerEnding.Tick += new System.EventHandler(this.timerEnding_Tick);
            // 
            // bt_restart
            // 
            this.bt_restart.Location = new System.Drawing.Point(259, 3);
            this.bt_restart.Name = "bt_restart";
            this.bt_restart.Size = new System.Drawing.Size(299, 60);
            this.bt_restart.TabIndex = 15;
            this.bt_restart.Text = "Torna a seleziona personaggio";
            this.bt_restart.UseVisualStyleBackColor = true;
            this.bt_restart.Click += new System.EventHandler(this.bt_restart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::testoop.Properties.Resources.bottedaorbi;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(802, 521);
            this.Controls.Add(this.bt_restart);
            this.Controls.Add(this.labelSpeech);
            this.Controls.Add(this.labelNome2);
            this.Controls.Add(this.labelNome1);
            this.Controls.Add(this.labelVita2);
            this.Controls.Add(this.labelVita1);
            this.Controls.Add(this.buttonMove4);
            this.Controls.Add(this.buttonMove3);
            this.Controls.Add(this.buttonMove2);
            this.Controls.Add(this.buttonMove1);
            this.Controls.Add(this.picturePlayer2);
            this.Controls.Add(this.picturePlayer1);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.selectCharacter2);
            this.Controls.Add(this.labelVS);
            this.Controls.Add(this.selectCharacter1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Botte da Orbi - Paperino Edition";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturePlayer2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox selectCharacter1;
        private System.Windows.Forms.Label labelVS;
        private System.Windows.Forms.ComboBox selectCharacter2;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.PictureBox picturePlayer1;
        private System.Windows.Forms.PictureBox picturePlayer2;
        private System.Windows.Forms.Button buttonMove1;
        private System.Windows.Forms.Button buttonMove2;
        private System.Windows.Forms.Button buttonMove3;
        private System.Windows.Forms.Button buttonMove4;
        private System.Windows.Forms.Label labelVita1;
        private System.Windows.Forms.Label labelVita2;
        private System.Windows.Forms.Label labelNome1;
        private System.Windows.Forms.Label labelNome2;
        private System.Windows.Forms.Label labelSpeech;
        private System.Windows.Forms.Timer timerEntry;
        private System.Windows.Forms.Timer timerEnding;
        private System.Windows.Forms.Button bt_restart;
    }
}

