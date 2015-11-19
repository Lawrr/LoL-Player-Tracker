namespace LoLPlayerTracker {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.SummonerNameTextBox = new System.Windows.Forms.TextBox();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.SummonerNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SummonerNameTextBox
            // 
            this.SummonerNameTextBox.Location = new System.Drawing.Point(12, 24);
            this.SummonerNameTextBox.MaxLength = 16;
            this.SummonerNameTextBox.Name = "SummonerNameTextBox";
            this.SummonerNameTextBox.Size = new System.Drawing.Size(253, 20);
            this.SummonerNameTextBox.TabIndex = 0;
            this.SummonerNameTextBox.WordWrap = false;
            this.SummonerNameTextBox.TextChanged += new System.EventHandler(this.SummonerNameTextBox_TextChanged);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(271, 21);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateButton.TabIndex = 1;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // SummonerNameLabel
            // 
            this.SummonerNameLabel.AutoSize = true;
            this.SummonerNameLabel.Location = new System.Drawing.Point(9, 8);
            this.SummonerNameLabel.Name = "SummonerNameLabel";
            this.SummonerNameLabel.Size = new System.Drawing.Size(88, 13);
            this.SummonerNameLabel.TabIndex = 2;
            this.SummonerNameLabel.Text = "Summoner Name";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 681);
            this.Controls.Add(this.SummonerNameLabel);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.SummonerNameTextBox);
            this.Name = "MainForm";
            this.Text = "LoL Player Tracker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SummonerNameTextBox;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Label SummonerNameLabel;
    }
}

