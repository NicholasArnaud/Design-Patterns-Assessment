namespace FSMAssessment
{
    partial class Form1
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
            this.PlayerHealth = new System.Windows.Forms.ProgressBar();
            this.EnemyHealth = new System.Windows.Forms.ProgressBar();
            this.TextLog = new System.Windows.Forms.RichTextBox();
            this.AtkButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.PlayerName = new System.Windows.Forms.MaskedTextBox();
            this.EnemyName = new System.Windows.Forms.MaskedTextBox();
            this.PotionButton = new System.Windows.Forms.Button();
            this.PassTurn = new System.Windows.Forms.Button();
            this.HelpButton = new System.Windows.Forms.Button();
            this.Playerlvl = new System.Windows.Forms.TextBox();
            this.Enemylvl = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // PlayerHealth
            // 
            this.PlayerHealth.Location = new System.Drawing.Point(12, 166);
            this.PlayerHealth.Name = "PlayerHealth";
            this.PlayerHealth.Size = new System.Drawing.Size(100, 12);
            this.PlayerHealth.TabIndex = 0;
            this.PlayerHealth.Value = 100;
            // 
            // EnemyHealth
            // 
            this.EnemyHealth.Location = new System.Drawing.Point(329, 166);
            this.EnemyHealth.Name = "EnemyHealth";
            this.EnemyHealth.Size = new System.Drawing.Size(100, 12);
            this.EnemyHealth.TabIndex = 1;
            this.EnemyHealth.Value = 100;
            // 
            // TextLog
            // 
            this.TextLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextLog.Location = new System.Drawing.Point(12, 13);
            this.TextLog.Name = "TextLog";
            this.TextLog.ReadOnly = true;
            this.TextLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.TextLog.Size = new System.Drawing.Size(439, 113);
            this.TextLog.TabIndex = 2;
            this.TextLog.Text = "";
            // 
            // AtkButton
            // 
            this.AtkButton.Location = new System.Drawing.Point(187, 132);
            this.AtkButton.Name = "AtkButton";
            this.AtkButton.Size = new System.Drawing.Size(75, 34);
            this.AtkButton.TabIndex = 3;
            this.AtkButton.Text = "Attack";
            this.AtkButton.UseVisualStyleBackColor = true;
            this.AtkButton.Click += new System.EventHandler(this.AtkButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(12, 248);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 27);
            this.LoadButton.TabIndex = 4;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(344, 248);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(85, 27);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(187, 248);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 6;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // PlayerName
            // 
            this.PlayerName.Location = new System.Drawing.Point(12, 140);
            this.PlayerName.Name = "PlayerName";
            this.PlayerName.ReadOnly = true;
            this.PlayerName.Size = new System.Drawing.Size(100, 20);
            this.PlayerName.TabIndex = 7;
            // 
            // EnemyName
            // 
            this.EnemyName.Location = new System.Drawing.Point(329, 140);
            this.EnemyName.Name = "EnemyName";
            this.EnemyName.ReadOnly = true;
            this.EnemyName.Size = new System.Drawing.Size(100, 20);
            this.EnemyName.TabIndex = 8;
            // 
            // PotionButton
            // 
            this.PotionButton.Location = new System.Drawing.Point(12, 194);
            this.PotionButton.Name = "PotionButton";
            this.PotionButton.Size = new System.Drawing.Size(86, 34);
            this.PotionButton.TabIndex = 9;
            this.PotionButton.Text = "Potion";
            this.PotionButton.UseVisualStyleBackColor = true;
            this.PotionButton.Click += new System.EventHandler(this.Potion_Click);
            // 
            // PassTurn
            // 
            this.PassTurn.Location = new System.Drawing.Point(187, 194);
            this.PassTurn.Name = "PassTurn";
            this.PassTurn.Size = new System.Drawing.Size(75, 23);
            this.PassTurn.TabIndex = 10;
            this.PassTurn.Text = "Pass Turn";
            this.PassTurn.UseVisualStyleBackColor = true;
            this.PassTurn.Click += new System.EventHandler(this.PassTurn_Click);
            // 
            // HelpButton
            // 
            this.HelpButton.Location = new System.Drawing.Point(329, 194);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(100, 34);
            this.HelpButton.TabIndex = 11;
            this.HelpButton.Text = "Help";
            this.HelpButton.UseVisualStyleBackColor = true;
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // Playerlvl
            // 
            this.Playerlvl.Location = new System.Drawing.Point(118, 132);
            this.Playerlvl.Name = "Playerlvl";
            this.Playerlvl.ReadOnly = true;
            this.Playerlvl.Size = new System.Drawing.Size(53, 20);
            this.Playerlvl.TabIndex = 12;
            // 
            // Enemylvl
            // 
            this.Enemylvl.Location = new System.Drawing.Point(268, 132);
            this.Enemylvl.Name = "Enemylvl";
            this.Enemylvl.ReadOnly = true;
            this.Enemylvl.Size = new System.Drawing.Size(55, 20);
            this.Enemylvl.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 287);
            this.Controls.Add(this.Enemylvl);
            this.Controls.Add(this.Playerlvl);
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.PassTurn);
            this.Controls.Add(this.PotionButton);
            this.Controls.Add(this.EnemyName);
            this.Controls.Add(this.PlayerName);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.AtkButton);
            this.Controls.Add(this.TextLog);
            this.Controls.Add(this.EnemyHealth);
            this.Controls.Add(this.PlayerHealth);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Brightest Dungeon";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar PlayerHealth;
        private System.Windows.Forms.ProgressBar EnemyHealth;
        private System.Windows.Forms.RichTextBox TextLog;
        private System.Windows.Forms.Button AtkButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.MaskedTextBox PlayerName;
        private System.Windows.Forms.MaskedTextBox EnemyName;
        private System.Windows.Forms.Button PotionButton;
        private System.Windows.Forms.Button PassTurn;
        private new System.Windows.Forms.Button HelpButton;
        private System.Windows.Forms.TextBox Playerlvl;
        private System.Windows.Forms.TextBox Enemylvl;
    }
}

