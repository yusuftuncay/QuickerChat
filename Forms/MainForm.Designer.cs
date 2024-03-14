namespace QuickerChat.Forms
{
    partial class MainForm
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
            this.GroupBoxPresets = new System.Windows.Forms.GroupBox();
            this.RadioButtonCustomPreset = new System.Windows.Forms.RadioButton();
            this.RadioButtonPreset1 = new System.Windows.Forms.RadioButton();
            this.RadioButtonPreset2 = new System.Windows.Forms.RadioButton();
            this.RadioButtonPreset3 = new System.Windows.Forms.RadioButton();
            this.RadioButtonPreset4 = new System.Windows.Forms.RadioButton();
            this.TextBoxCustom = new System.Windows.Forms.TextBox();
            this.GroupBoxActions = new System.Windows.Forms.GroupBox();
            this.ButtonChangeKeybind = new System.Windows.Forms.Button();
            this.ButtonCheckController = new System.Windows.Forms.Button();
            this.ButtonReset = new System.Windows.Forms.Button();
            this.LabelController = new System.Windows.Forms.Label();
            this.MenuStripMainPage = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator_File = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SocialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GithubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupBoxPresets.SuspendLayout();
            this.GroupBoxActions.SuspendLayout();
            this.MenuStripMainPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxPresets
            // 
            this.GroupBoxPresets.Controls.Add(this.RadioButtonCustomPreset);
            this.GroupBoxPresets.Controls.Add(this.RadioButtonPreset1);
            this.GroupBoxPresets.Controls.Add(this.RadioButtonPreset2);
            this.GroupBoxPresets.Controls.Add(this.RadioButtonPreset3);
            this.GroupBoxPresets.Controls.Add(this.RadioButtonPreset4);
            this.GroupBoxPresets.Controls.Add(this.TextBoxCustom);
            this.GroupBoxPresets.Location = new System.Drawing.Point(13, 27);
            this.GroupBoxPresets.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBoxPresets.Name = "GroupBoxPresets";
            this.GroupBoxPresets.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBoxPresets.Size = new System.Drawing.Size(308, 220);
            this.GroupBoxPresets.TabIndex = 0;
            this.GroupBoxPresets.TabStop = false;
            this.GroupBoxPresets.Text = "Presets";
            // 
            // RadioButtonCustomPreset
            // 
            this.RadioButtonCustomPreset.AutoSize = true;
            this.RadioButtonCustomPreset.Location = new System.Drawing.Point(8, 122);
            this.RadioButtonCustomPreset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RadioButtonCustomPreset.Name = "RadioButtonCustomPreset";
            this.RadioButtonCustomPreset.Size = new System.Drawing.Size(67, 19);
            this.RadioButtonCustomPreset.TabIndex = 4;
            this.RadioButtonCustomPreset.Text = "Custom";
            this.RadioButtonCustomPreset.UseVisualStyleBackColor = true;
            this.RadioButtonCustomPreset.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // RadioButtonPreset1
            // 
            this.RadioButtonPreset1.AutoSize = true;
            this.RadioButtonPreset1.Checked = true;
            this.RadioButtonPreset1.Location = new System.Drawing.Point(8, 22);
            this.RadioButtonPreset1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RadioButtonPreset1.Name = "RadioButtonPreset1";
            this.RadioButtonPreset1.Size = new System.Drawing.Size(110, 19);
            this.RadioButtonPreset1.TabIndex = 0;
            this.RadioButtonPreset1.TabStop = true;
            this.RadioButtonPreset1.Text = "What a kkr save!";
            this.RadioButtonPreset1.UseVisualStyleBackColor = true;
            this.RadioButtonPreset1.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // RadioButtonPreset2
            // 
            this.RadioButtonPreset2.AutoSize = true;
            this.RadioButtonPreset2.Location = new System.Drawing.Point(8, 47);
            this.RadioButtonPreset2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RadioButtonPreset2.Name = "RadioButtonPreset2";
            this.RadioButtonPreset2.Size = new System.Drawing.Size(81, 19);
            this.RadioButtonPreset2.TabIndex = 1;
            this.RadioButtonPreset2.Text = "Kkr noobs!";
            this.RadioButtonPreset2.UseVisualStyleBackColor = true;
            this.RadioButtonPreset2.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // RadioButtonPreset3
            // 
            this.RadioButtonPreset3.AutoSize = true;
            this.RadioButtonPreset3.Location = new System.Drawing.Point(8, 72);
            this.RadioButtonPreset3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RadioButtonPreset3.Name = "RadioButtonPreset3";
            this.RadioButtonPreset3.Size = new System.Drawing.Size(117, 19);
            this.RadioButtonPreset3.TabIndex = 2;
            this.RadioButtonPreset3.Text = "Easiest shot ever..";
            this.RadioButtonPreset3.UseVisualStyleBackColor = true;
            this.RadioButtonPreset3.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // RadioButtonPreset4
            // 
            this.RadioButtonPreset4.AutoSize = true;
            this.RadioButtonPreset4.Location = new System.Drawing.Point(8, 97);
            this.RadioButtonPreset4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RadioButtonPreset4.Name = "RadioButtonPreset4";
            this.RadioButtonPreset4.Size = new System.Drawing.Size(56, 19);
            this.RadioButtonPreset4.TabIndex = 3;
            this.RadioButtonPreset4.Text = "Bruh..";
            this.RadioButtonPreset4.UseVisualStyleBackColor = true;
            this.RadioButtonPreset4.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // TextBoxCustom
            // 
            this.TextBoxCustom.Enabled = false;
            this.TextBoxCustom.Location = new System.Drawing.Point(8, 147);
            this.TextBoxCustom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextBoxCustom.Multiline = true;
            this.TextBoxCustom.Name = "TextBoxCustom";
            this.TextBoxCustom.Size = new System.Drawing.Size(292, 67);
            this.TextBoxCustom.TabIndex = 5;
            this.TextBoxCustom.TextChanged += new System.EventHandler(this.TextBoxCustom_TextChanged);
            // 
            // GroupBoxActions
            // 
            this.GroupBoxActions.Controls.Add(this.ButtonChangeKeybind);
            this.GroupBoxActions.Controls.Add(this.ButtonCheckController);
            this.GroupBoxActions.Controls.Add(this.ButtonReset);
            this.GroupBoxActions.Controls.Add(this.LabelController);
            this.GroupBoxActions.Location = new System.Drawing.Point(13, 253);
            this.GroupBoxActions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBoxActions.Name = "GroupBoxActions";
            this.GroupBoxActions.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBoxActions.Size = new System.Drawing.Size(308, 196);
            this.GroupBoxActions.TabIndex = 1;
            this.GroupBoxActions.TabStop = false;
            this.GroupBoxActions.Text = "Actions";
            // 
            // ButtonChangeKeybind
            // 
            this.ButtonChangeKeybind.Location = new System.Drawing.Point(7, 163);
            this.ButtonChangeKeybind.Name = "ButtonChangeKeybind";
            this.ButtonChangeKeybind.Size = new System.Drawing.Size(294, 27);
            this.ButtonChangeKeybind.TabIndex = 5;
            this.ButtonChangeKeybind.Text = "Change Keybind";
            this.ButtonChangeKeybind.UseVisualStyleBackColor = true;
            this.ButtonChangeKeybind.Click += new System.EventHandler(this.ButtonChangeKeybind_Click);
            // 
            // ButtonCheckController
            // 
            this.ButtonCheckController.Location = new System.Drawing.Point(8, 97);
            this.ButtonCheckController.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ButtonCheckController.Name = "ButtonCheckController";
            this.ButtonCheckController.Size = new System.Drawing.Size(292, 27);
            this.ButtonCheckController.TabIndex = 3;
            this.ButtonCheckController.Text = "Search Controller";
            this.ButtonCheckController.UseVisualStyleBackColor = true;
            this.ButtonCheckController.Click += new System.EventHandler(this.ButtonCheckController_Click);
            // 
            // ButtonReset
            // 
            this.ButtonReset.Location = new System.Drawing.Point(8, 130);
            this.ButtonReset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ButtonReset.Name = "ButtonReset";
            this.ButtonReset.Size = new System.Drawing.Size(292, 27);
            this.ButtonReset.TabIndex = 2;
            this.ButtonReset.Text = "Disconnect Controller";
            this.ButtonReset.UseVisualStyleBackColor = true;
            this.ButtonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // LabelController
            // 
            this.LabelController.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LabelController.ForeColor = System.Drawing.Color.Black;
            this.LabelController.Location = new System.Drawing.Point(8, 19);
            this.LabelController.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelController.Name = "LabelController";
            this.LabelController.Size = new System.Drawing.Size(292, 62);
            this.LabelController.TabIndex = 4;
            this.LabelController.Text = "...";
            this.LabelController.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MenuStripMainPage
            // 
            this.MenuStripMainPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.SocialsToolStripMenuItem});
            this.MenuStripMainPage.Location = new System.Drawing.Point(0, 0);
            this.MenuStripMainPage.Name = "MenuStripMainPage";
            this.MenuStripMainPage.Size = new System.Drawing.Size(334, 24);
            this.MenuStripMainPage.TabIndex = 2;
            this.MenuStripMainPage.Text = "MenuStripMainPage";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VersionToolStripMenuItem,
            this.ToolStripSeparator_File,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // VersionToolStripMenuItem
            // 
            this.VersionToolStripMenuItem.Name = "VersionToolStripMenuItem";
            this.VersionToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.VersionToolStripMenuItem.Text = "Version";
            this.VersionToolStripMenuItem.Click += new System.EventHandler(this.VersionToolStripMenuItem_Click);
            // 
            // ToolStripSeparator_File
            // 
            this.ToolStripSeparator_File.Name = "ToolStripSeparator_File";
            this.ToolStripSeparator_File.Size = new System.Drawing.Size(109, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // SocialsToolStripMenuItem
            // 
            this.SocialsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GithubToolStripMenuItem,
            this.EmailToolStripMenuItem});
            this.SocialsToolStripMenuItem.Name = "SocialsToolStripMenuItem";
            this.SocialsToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.SocialsToolStripMenuItem.Text = "Socials";
            // 
            // GithubToolStripMenuItem
            // 
            this.GithubToolStripMenuItem.Name = "GithubToolStripMenuItem";
            this.GithubToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.GithubToolStripMenuItem.Text = "Github";
            this.GithubToolStripMenuItem.Click += new System.EventHandler(this.GithubToolStripMenuItem_Click);
            // 
            // EmailToolStripMenuItem
            // 
            this.EmailToolStripMenuItem.Name = "EmailToolStripMenuItem";
            this.EmailToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.EmailToolStripMenuItem.Text = "Email";
            this.EmailToolStripMenuItem.Click += new System.EventHandler(this.EmailToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 461);
            this.Controls.Add(this.GroupBoxActions);
            this.Controls.Add(this.GroupBoxPresets);
            this.Controls.Add(this.MenuStripMainPage);
            this.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "QuickerChat";
            this.GroupBoxPresets.ResumeLayout(false);
            this.GroupBoxPresets.PerformLayout();
            this.GroupBoxActions.ResumeLayout(false);
            this.MenuStripMainPage.ResumeLayout(false);
            this.MenuStripMainPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Button ButtonChangeKeybind;
        private System.Windows.Forms.Button ButtonCheckController;
        private System.Windows.Forms.Button ButtonReset;
        private System.Windows.Forms.ToolStripMenuItem EmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GithubToolStripMenuItem;
        private System.Windows.Forms.GroupBox GroupBoxActions;
        private System.Windows.Forms.GroupBox GroupBoxPresets;
        private System.Windows.Forms.Label LabelController;
        private System.Windows.Forms.MenuStrip MenuStripMainPage;
        private System.Windows.Forms.RadioButton RadioButtonCustomPreset;
        private System.Windows.Forms.RadioButton RadioButtonPreset1;
        private System.Windows.Forms.RadioButton RadioButtonPreset2;
        private System.Windows.Forms.RadioButton RadioButtonPreset3;
        private System.Windows.Forms.RadioButton RadioButtonPreset4;
        private System.Windows.Forms.ToolStripMenuItem SocialsToolStripMenuItem;
        private System.Windows.Forms.TextBox TextBoxCustom;
        private System.Windows.Forms.ToolStripMenuItem VersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator_File;
    }
}