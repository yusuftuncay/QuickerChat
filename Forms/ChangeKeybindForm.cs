using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuickerChat.Forms
{
    public partial class ChangeKeybindForm : Form
    {
        #region Variables
        /// <summary>
        /// String representing the selected keybind
        /// </summary>
        private static string[] Keybind { get; set; }

        /// <summary>
        /// Define an integer variable to keep track of the number of clicks
        /// </summary>
        private int clickCount = 0;
        #endregion

        #region ChangeKeybindForm
        public ChangeKeybindForm()
        {
            InitializeComponent();
        }
        private void ChangeKeybindForm_Load(object sender, EventArgs e)
        {
            Keybind = MainForm.Keybind;

            if (Keybind[0] != null && Keybind[1] != null)
            {
                LabelInfo.Text = $"{Keybind[0]} + {Keybind[1]}";
            }
            else
            {
                LabelInfo.Text = "No Keybind Set";
            }
        }
        #endregion

        #region Click
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // Check if a keybind has been set
            if (Keybind != null && Keybind[0] != null && Keybind[1] != null)
            {
                // Set the keybind in MainForm using a static property
                MainForm.Keybind = Keybind;

                Close();
            } else
            {
                // Prompt the user to select a keybind
                MessageBox.Show("Please select a keybind first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonReset_Click(object sender, EventArgs e)
        {
            Keybind = new string[2];
            LabelInfo.Text = "No Keybind Set";
            clickCount = 0;
        }
        private void Button_Click(object sender, EventArgs e)
        {
            string buttonName = ((Label)sender).Name;

            if (buttonName == LabelInfo.Text)
                return;

            // Increment
            clickCount++;

            if (clickCount == 1)
            {
                LabelInfo.Text = buttonName;
                Keybind[0] = buttonName;
                return;
            }
            else if (clickCount == 2)
            {
                // Append the button name with a "+", if not already present
                if (!LabelInfo.Text.EndsWith(" + ") && !LabelInfo.Text.EndsWith(buttonName))
                {
                    LabelInfo.Text += " + " + buttonName;
                    Keybind[1] = buttonName;
                    clickCount = 0;
                }
            }
        }
        #endregion

        #region TextChanged
        private void LabelInfo_TextChanged(object sender, EventArgs e)
        {
            if (LabelInfo.Text == "No Keybind Set")
            {
                LabelInfo.ForeColor = Color.Red;
            }
            else
            {
                LabelInfo.ForeColor = Color.Black;
            }
        }
        #endregion
    }
}
