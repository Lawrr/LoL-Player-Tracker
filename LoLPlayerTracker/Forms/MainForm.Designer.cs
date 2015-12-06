using System;

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
            this.SummonerNameLabel = new System.Windows.Forms.Label();
            this.PastMatchesPanel = new System.Windows.Forms.Panel();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.SettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.RegionComboBox = new System.Windows.Forms.ComboBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchGroupBox = new System.Windows.Forms.GroupBox();
            this.CurrentGameGroupBox = new System.Windows.Forms.GroupBox();
            this.StatusGroupBox = new System.Windows.Forms.GroupBox();
            this.LoadGameButton = new System.Windows.Forms.Button();
            this.SettingsGroupBox.SuspendLayout();
            this.SearchGroupBox.SuspendLayout();
            this.StatusGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SummonerNameTextBox
            // 
            this.SummonerNameTextBox.Location = new System.Drawing.Point(16, 42);
            this.SummonerNameTextBox.MaxLength = 16;
            this.SummonerNameTextBox.Name = "SummonerNameTextBox";
            this.SummonerNameTextBox.Size = new System.Drawing.Size(225, 20);
            this.SummonerNameTextBox.TabIndex = 0;
            this.SummonerNameTextBox.WordWrap = false;
            this.SummonerNameTextBox.TextChanged += new System.EventHandler(this.SummonerNameTextBox_TextChanged);
            // 
            // SummonerNameLabel
            // 
            this.SummonerNameLabel.AutoSize = true;
            this.SummonerNameLabel.Location = new System.Drawing.Point(13, 26);
            this.SummonerNameLabel.Name = "SummonerNameLabel";
            this.SummonerNameLabel.Size = new System.Drawing.Size(89, 13);
            this.SummonerNameLabel.TabIndex = 2;
            this.SummonerNameLabel.Text = "Summoner name:";
            // 
            // PastMatchesPanel
            // 
            this.PastMatchesPanel.AutoScroll = true;
            this.PastMatchesPanel.BackColor = System.Drawing.Color.White;
            this.PastMatchesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PastMatchesPanel.Location = new System.Drawing.Point(12, 234);
            this.PastMatchesPanel.Name = "PastMatchesPanel";
            this.PastMatchesPanel.Size = new System.Drawing.Size(680, 435);
            this.PastMatchesPanel.TabIndex = 3;
            this.PastMatchesPanel.Click += new System.EventHandler(this.PastMatchPanel_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(13, 20);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(37, 13);
            this.StatusLabel.TabIndex = 4;
            this.StatusLabel.Text = "Status";
            // 
            // SettingsGroupBox
            // 
            this.SettingsGroupBox.Controls.Add(this.RegionComboBox);
            this.SettingsGroupBox.Controls.Add(this.SummonerNameLabel);
            this.SettingsGroupBox.Controls.Add(this.SummonerNameTextBox);
            this.SettingsGroupBox.Location = new System.Drawing.Point(12, 62);
            this.SettingsGroupBox.Name = "SettingsGroupBox";
            this.SettingsGroupBox.Size = new System.Drawing.Size(339, 80);
            this.SettingsGroupBox.TabIndex = 6;
            this.SettingsGroupBox.TabStop = false;
            this.SettingsGroupBox.Text = "Settings";
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
            this.RegionComboBox.Location = new System.Drawing.Point(247, 41);
            this.RegionComboBox.Name = "RegionComboBox";
            this.RegionComboBox.Size = new System.Drawing.Size(75, 21);
            this.RegionComboBox.TabIndex = 5;
            this.RegionComboBox.SelectedIndexChanged += new System.EventHandler(this.RegionComboBox_SelectedIndexChanged);
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(247, 41);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 22);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(13, 26);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(166, 13);
            this.SearchLabel.TabIndex = 2;
            this.SearchLabel.Text = "Search for games with summoner:";
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(16, 42);
            this.SearchTextBox.MaxLength = 16;
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(225, 20);
            this.SearchTextBox.TabIndex = 0;
            this.SearchTextBox.WordWrap = false;
            // 
            // SearchGroupBox
            // 
            this.SearchGroupBox.Controls.Add(this.SearchLabel);
            this.SearchGroupBox.Controls.Add(this.SearchTextBox);
            this.SearchGroupBox.Controls.Add(this.SearchButton);
            this.SearchGroupBox.Location = new System.Drawing.Point(12, 148);
            this.SearchGroupBox.Name = "SearchGroupBox";
            this.SearchGroupBox.Size = new System.Drawing.Size(339, 80);
            this.SearchGroupBox.TabIndex = 0;
            this.SearchGroupBox.TabStop = false;
            this.SearchGroupBox.Text = "Search";
            // 
            // CurrentGameGroupBox
            // 
            this.CurrentGameGroupBox.Location = new System.Drawing.Point(357, 12);
            this.CurrentGameGroupBox.Name = "CurrentGameGroupBox";
            this.CurrentGameGroupBox.Size = new System.Drawing.Size(335, 216);
            this.CurrentGameGroupBox.TabIndex = 7;
            this.CurrentGameGroupBox.TabStop = false;
            this.CurrentGameGroupBox.Text = "Current Game";
            // 
            // StatusGroupBox
            // 
            this.StatusGroupBox.Controls.Add(this.LoadGameButton);
            this.StatusGroupBox.Controls.Add(this.StatusLabel);
            this.StatusGroupBox.Location = new System.Drawing.Point(12, 12);
            this.StatusGroupBox.Name = "StatusGroupBox";
            this.StatusGroupBox.Size = new System.Drawing.Size(339, 44);
            this.StatusGroupBox.TabIndex = 0;
            this.StatusGroupBox.TabStop = false;
            this.StatusGroupBox.Text = "Status";
            // 
            // LoadGameButton
            // 
            this.LoadGameButton.Location = new System.Drawing.Point(247, 15);
            this.LoadGameButton.Name = "LoadGameButton";
            this.LoadGameButton.Size = new System.Drawing.Size(75, 23);
            this.LoadGameButton.TabIndex = 5;
            this.LoadGameButton.Text = "Load Game";
            this.LoadGameButton.UseVisualStyleBackColor = true;
            this.LoadGameButton.Click += new System.EventHandler(this.LoadGameButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 681);
            this.Controls.Add(this.StatusGroupBox);
            this.Controls.Add(this.CurrentGameGroupBox);
            this.Controls.Add(this.SearchGroupBox);
            this.Controls.Add(this.SettingsGroupBox);
            this.Controls.Add(this.PastMatchesPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = String.Format("{0} {1}", Program.ProgramName, Program.ProgramVersion);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_FormLoad);
            this.SettingsGroupBox.ResumeLayout(false);
            this.SettingsGroupBox.PerformLayout();
            this.SearchGroupBox.ResumeLayout(false);
            this.SearchGroupBox.PerformLayout();
            this.StatusGroupBox.ResumeLayout(false);
            this.StatusGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox SummonerNameTextBox;
        private System.Windows.Forms.Label SummonerNameLabel;
        private System.Windows.Forms.Panel PastMatchesPanel;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.ComboBox RegionComboBox;
        private System.Windows.Forms.GroupBox SettingsGroupBox;
        private System.Windows.Forms.GroupBox SearchGroupBox;
        private System.Windows.Forms.GroupBox CurrentGameGroupBox;
        private System.Windows.Forms.GroupBox StatusGroupBox;
        private System.Windows.Forms.Button LoadGameButton;
    }
}

