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
            this.LabelKeybindOutput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(12, 339);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(108, 23);
            this.ButtonSave.TabIndex = 0;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            // 
            // ButtonReset
            // 
            this.ButtonReset.Location = new System.Drawing.Point(12, 368);
            this.ButtonReset.Name = "ButtonReset";
            this.ButtonReset.Size = new System.Drawing.Size(108, 23);
            this.ButtonReset.TabIndex = 1;
            this.ButtonReset.Text = "Reset";
            this.ButtonReset.UseVisualStyleBackColor = true;
            // 
            // LabelKeybindOutput
            // 
            this.LabelKeybindOutput.AutoSize = true;
            this.LabelKeybindOutput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LabelKeybindOutput.Location = new System.Drawing.Point(126, 341);
            this.LabelKeybindOutput.MinimumSize = new System.Drawing.Size(100, 50);
            this.LabelKeybindOutput.Name = "LabelKeybindOutput";
            this.LabelKeybindOutput.Size = new System.Drawing.Size(100, 50);
            this.LabelKeybindOutput.TabIndex = 2;
            this.LabelKeybindOutput.Text = "...";
            this.LabelKeybindOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChangeKeybindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(526, 403);
            this.Controls.Add(this.LabelKeybindOutput);
            this.Controls.Add(this.ButtonReset);
            this.Controls.Add(this.ButtonSave);
            this.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "ChangeKeybindForm";
            this.Text = "Change Keybind";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Button ButtonReset;
        private System.Windows.Forms.Label LabelKeybindOutput;
    }
}