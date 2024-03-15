namespace QuickerChat.Forms
{
    partial class ChangeKeybindForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeKeybindForm));
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonReset = new System.Windows.Forms.Button();
            this.LabelInfo = new System.Windows.Forms.Label();
            this.L1 = new System.Windows.Forms.Label();
            this.R1 = new System.Windows.Forms.Label();
            this.L2 = new System.Windows.Forms.Label();
            this.R2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(190, 240);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(140, 23);
            this.ButtonSave.TabIndex = 0;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonReset
            // 
            this.ButtonReset.Location = new System.Drawing.Point(190, 269);
            this.ButtonReset.Name = "ButtonReset";
            this.ButtonReset.Size = new System.Drawing.Size(140, 23);
            this.ButtonReset.TabIndex = 1;
            this.ButtonReset.Text = "Reset";
            this.ButtonReset.UseVisualStyleBackColor = true;
            this.ButtonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // LabelInfo
            // 
            this.LabelInfo.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.LabelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LabelInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LabelInfo.ForeColor = System.Drawing.Color.White;
            this.LabelInfo.Location = new System.Drawing.Point(170, 300);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(180, 95);
            this.LabelInfo.TabIndex = 3;
            this.LabelInfo.Text = "...";
            this.LabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L1
            // 
            this.L1.BackColor = System.Drawing.Color.Transparent;
            this.L1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.L1.Location = new System.Drawing.Point(45, 35);
            this.L1.Name = "L1";
            this.L1.Size = new System.Drawing.Size(29, 19);
            this.L1.TabIndex = 4;
            this.L1.Click += new System.EventHandler(this.Button_Click);
            // 
            // R1
            // 
            this.R1.BackColor = System.Drawing.Color.Transparent;
            this.R1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.R1.Location = new System.Drawing.Point(449, 35);
            this.R1.Name = "R1";
            this.R1.Size = new System.Drawing.Size(29, 19);
            this.R1.TabIndex = 5;
            this.R1.Click += new System.EventHandler(this.Button_Click);
            // 
            // L2
            // 
            this.L2.BackColor = System.Drawing.Color.Transparent;
            this.L2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.L2.Location = new System.Drawing.Point(45, 62);
            this.L2.Name = "L2";
            this.L2.Size = new System.Drawing.Size(29, 32);
            this.L2.TabIndex = 6;
            this.L2.Click += new System.EventHandler(this.Button_Click);
            // 
            // R2
            // 
            this.R2.BackColor = System.Drawing.Color.Transparent;
            this.R2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.R2.Location = new System.Drawing.Point(449, 62);
            this.R2.Name = "R2";
            this.R2.Size = new System.Drawing.Size(29, 32);
            this.R2.TabIndex = 7;
            this.R2.Click += new System.EventHandler(this.Button_Click);
            // 
            // ChangeKeybindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(526, 403);
            this.Controls.Add(this.R2);
            this.Controls.Add(this.L2);
            this.Controls.Add(this.R1);
            this.Controls.Add(this.L1);
            this.Controls.Add(this.LabelInfo);
            this.Controls.Add(this.ButtonReset);
            this.Controls.Add(this.ButtonSave);
            this.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "ChangeKeybindForm";
            this.Text = "Change Keybind";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ChangeKeybindForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Button ButtonReset;
        private System.Windows.Forms.Label LabelInfo;
        private System.Windows.Forms.Label L1;
        private System.Windows.Forms.Label R1;
        private System.Windows.Forms.Label L2;
        private System.Windows.Forms.Label R2;
    }
}