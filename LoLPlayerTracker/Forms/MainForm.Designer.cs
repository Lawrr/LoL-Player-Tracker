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
            this.PastMatchesPanel = new System.Windows.Forms.Panel();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.RegionLabel = new System.Windows.Forms.Label();
            this.RegionComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // SummonerNameTextBox
            // 
            this.SummonerNameTextBox.Location = new System.Drawing.Point(12, 24);
            this.SummonerNameTextBox.MaxLength = 16;
            this.SummonerNameTextBox.Name = "SummonerNameTextBox";
            this.SummonerNameTextBox.Size = new System.Drawing.Size(207, 20);
            this.SummonerNameTextBox.TabIndex = 0;
            this.SummonerNameTextBox.WordWrap = false;
            this.SummonerNameTextBox.TextChanged += new System.EventHandler(this.SummonerNameTextBox_TextChanged);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(225, 23);
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
            this.SummonerNameLabel.Size = new System.Drawing.Size(89, 13);
            this.SummonerNameLabel.TabIndex = 2;
            this.SummonerNameLabel.Text = "Summoner name:";
            // 
            // PastMatchesPanel
            // 
            this.PastMatchesPanel.AutoScroll = true;
            this.PastMatchesPanel.BackColor = System.Drawing.SystemColors.Control;
            this.PastMatchesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PastMatchesPanel.Location = new System.Drawing.Point(12, 215);
            this.PastMatchesPanel.Name = "PastMatchesPanel";
            this.PastMatchesPanel.Size = new System.Drawing.Size(680, 454);
            this.PastMatchesPanel.TabIndex = 3;
            this.PastMatchesPanel.MouseEnter += new System.EventHandler(this.PastMatchesPanel_MouseEnter);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(9, 199);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(43, 13);
            this.StatusLabel.TabIndex = 4;
            this.StatusLabel.Text = "Status: ";
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(12, 164);
            this.SearchTextBox.MaxLength = 16;
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(207, 20);
            this.SearchTextBox.TabIndex = 0;
            this.SearchTextBox.WordWrap = false;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(225, 163);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(9, 148);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(166, 13);
            this.SearchLabel.TabIndex = 2;
            this.SearchLabel.Text = "Search for games with summoner:";
            // 
            // RegionLabel
            // 
            this.RegionLabel.AutoSize = true;
            this.RegionLabel.Location = new System.Drawing.Point(9, 58);
            this.RegionLabel.Name = "RegionLabel";
            this.RegionLabel.Size = new System.Drawing.Size(44, 13);
            this.RegionLabel.TabIndex = 2;
            this.RegionLabel.Text = "Region:";
            // 
            // RegionComboBox
            // 
            this.RegionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RegionComboBox.FormattingEnabled = true;
            this.RegionComboBox.Items.AddRange(new object[] {
            "NA",
            "EUW",
            "EUNE",
            "OCE",
            "KR",
            "TR",
            "BR",
            "RU",
            "LAN",
            "LAS"});
            this.RegionComboBox.Location = new System.Drawing.Point(59, 55);
            this.RegionComboBox.Name = "RegionComboBox";
            this.RegionComboBox.Size = new System.Drawing.Size(62, 21);
            this.RegionComboBox.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 681);
            this.Controls.Add(this.RegionComboBox);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.PastMatchesPanel);
            this.Controls.Add(this.SearchLabel);
            this.Controls.Add(this.RegionLabel);
            this.Controls.Add(this.SummonerNameLabel);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.SearchTextBox);
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
        private System.Windows.Forms.Panel PastMatchesPanel;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.Label RegionLabel;
        private System.Windows.Forms.ComboBox RegionComboBox;
    }
}

