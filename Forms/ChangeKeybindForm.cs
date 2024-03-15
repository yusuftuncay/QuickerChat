using SharpDX.DirectInput;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QuickerChat.Forms
{
    public partial class ChangeKeybindForm : Form
    {
        #region Variables
        /// <summary>
        /// Background worker for continuous _joystick polling
        /// </summary>
        private BackgroundWorker _backgroundWorker;

        /// <summary>
        /// XInput controller for capturing controller input
        /// </summary>
        private readonly Joystick _joystick;

        /// <summary>
        /// String representing the selected keybind
        /// </summary>
        private string _keybind;
        #endregion

        #region ChangeKeybindForm
        public ChangeKeybindForm(Joystick joystick)
        {
            InitializeComponent();
            _joystick = joystick;

            // Start background worker for continuous _joystick polling
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerAsync();
        }
        #endregion

        private void ChangeKeybindForm_Load(object sender, EventArgs e)
        {
            // Display instructions to the user using a message box
            LabelInfo.Text = "Press the desired key combination on your controller to set the keybind";
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // Check if a keybind has been set
            if (!string.IsNullOrEmpty(_keybind))
            {
                // Set the keybind in MainForm using a static property
                MainForm.Keybind = _keybind;

                // Display the selected keybind in LabelKeybindOutput
                LabelKeybindOutput.Text = _keybind;
            } else
            {
                // Prompt the user to select a keybind
                MessageBox.Show("Please select a keybind first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            // Reset the keybind
            _keybind = null;

            // Clear the label displaying the keybind
            LabelKeybindOutput.Text = "...";
        }

        /// <summary>
        /// Background worker method for _joystick polling
        /// </summary>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //
            LabelKeybindOutput.ForeColor = Color.GreenYellow;
            
            //if (_joystick.IsConnected)
            //{
            //    // Get the current state of the controller
            //    var state = _joystick.GetState();

            //    // Check if any button is pressed
            //    if (state.Gamepad.Buttons != GamepadButtonFlags.None)
            //    {
            //        // Construct the keybind string using the pressed buttons
            //        _keybind = state.Gamepad.Buttons.ToString();

            //        // Display the keybind in LabelKeybindOutput
            //        LabelKeybindOutput.Text = _keybind;
            //    }
            //} else
            //{
            //    // Prompt the user to connect a controller
            //    MessageBox.Show("Please connect a controller to set the keybind", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        //public bool IsKeyPress(Keys key)
        //{
        //    bool isCurrentlyPressed = WaveServices.Input.KeyboardState.IsKeyPressed(key);
        //    bool previouslyReleased = this.previousKeyStates[key] == ButtonState.Release;

        //    this.previousKeyStates[key] = isCurrentlyPressed ? ButtonState.Pressed : ButtonState.Release;

        //    return isCurrentlyPressed && previouslyReleased;
        //}
    }
}
