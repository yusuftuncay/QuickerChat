#region Using
using SharpDX.DirectInput;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Threading;
using System.Windows.Forms;
#endregion

namespace QuickerChat.Forms
{
    public partial class MainForm : Form
    {
        #region Variables
        /// <summary>
        /// Initializes the program by setting up a background worker to read _joystick polls in the background
        /// </summary>
        private BackgroundWorker _backgroundWorker;

        /// <summary>
        /// Represents a _joystick device for input control
        /// </summary>
        private Joystick _joystick;

        /// <summary>
        /// Controls the loop of the background worker, allowing for starting and stopping
        /// </summary>
        private bool _isBackgroundWorkerLoop;

        /// <summary>
        /// Form for changing keybindings in the program
        /// </summary>
        //private static readonly ChangeKeybindForm changeKeybindForm = new ChangeKeybindForm();

        private string _textToSpam;
        #endregion

        #region MainForm
        public MainForm()
        {
            InitializeComponent();
            InitializeController();
        }
        #endregion

        #region Import DLL
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion

        #region Controller Methods
        private void InitializeController()
        {
            LabelController.Text = "";
            LabelController.ForeColor = Color.Yellow;

            try
            {
                using (var directInput = new DirectInput())
                {
                    // Get First Joystick
                    foreach (var deviceInstance in directInput.GetDevices(DeviceClass.Keyboard, DeviceEnumerationFlags.AllDevices))
                    {
                        _joystick = new Joystick(directInput, deviceInstance.InstanceGuid);
                        break;
                    }
                }

                if (_joystick != null)
                {
                    LabelController.Text = $"Controller Connected\n{_joystick}";
                    LabelController.ForeColor = Color.Green;

                    // Enable Presets and BgWorker Loop
                    GroupBoxPresets.Enabled = true;
                    _isBackgroundWorkerLoop = true;
                } else
                {
                    LabelController.Text = "No Controller Connected";
                    LabelController.ForeColor = Color.Red;

                    // Disable Presets and BgWorker Loop
                    GroupBoxPresets.Enabled = false;
                    _isBackgroundWorkerLoop = false;
                    return;
                }

                // Acquire the _joystick
                _joystick.Properties.BufferSize = 128;
                _joystick.Acquire();

                // Start a background worker to continuously poll the controller
                _backgroundWorker = new BackgroundWorker();
                _backgroundWorker.DoWork += BackgroundWorker_DoWork;
                _backgroundWorker.RunWorkerAsync();
            } catch (Exception) { }
        }
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (_isBackgroundWorkerLoop)
            {
                _joystick.Poll();
                JoystickState data = _joystick.GetCurrentState();

                // Check button presses
                bool circleButtonPressed = data.Buttons[2]; // Circle

                if (circleButtonPressed)
                {
                    StartSpam();
                    Thread.Sleep(200);
                }

                Thread.Sleep(10);
            }
        }
        #endregion

        #region Spam Methods

        private void StartSpam()
        {
            try
            {
                // Find the Rocket League process
                var process = Process.GetProcessesByName("RocketLeague").FirstOrDefault();

                if (process == null)
                    return;

                // Wait
                process.WaitForInputIdle();

                // Focus on Rocket League
                SetForegroundWindow(process.MainWindowHandle);

                // Spam 3 times
                for (var i = 0; i < 3; i++)
                    SendKeys.SendWait($"T{_textToSpam}{{ENTER}}");

            } catch (Exception) { }
        }
        #endregion

        #region "Click" Events
        private void ButtonReset_Click(object sender, EventArgs e)
        {
            CleanStates();
        }
        private void ButtonCheckController_Click(object sender, EventArgs e)
        {
            InitializeController();
        }
        private void ButtonChangeKeybind_Click(object sender, EventArgs e)
        {
            //changeKeybindForm.Show();
        }
        #endregion

        #region "Changed" Events
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Check for custom input
            if (RadioButtonCustomPreset.Checked)
            {
                // Set "_textToSpam" property
                _textToSpam = null;
                TextBoxCustom.Enabled = true;
            } else
            {
                // Set "_textToSpam" property
                _textToSpam = ((RadioButton)sender).Text;
                TextBoxCustom.Enabled = false;
            }
        }
        private void TextBoxCustom_TextChanged(object sender, EventArgs e)
        {
            // Set "_textToSpam" property
            _textToSpam = TextBoxCustom.Text;
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Dispose Managed Resources
        /// </summary>
        private void CleanStates()
        {
            // BgWorker
            if (_backgroundWorker != null)
            {
                // Stop Loop
                _isBackgroundWorkerLoop = false;

                _backgroundWorker.DoWork -= BackgroundWorker_DoWork;
                _backgroundWorker.Dispose();
                _backgroundWorker = null;
            }

            // Joystick
            if (_joystick != null)
            {
                _joystick.Dispose();
                _joystick = null;
            }

            // Label
            LabelController.Text = "No Controller Connected";
            LabelController.ForeColor = Color.Red;

            // RadioButton
            RadioButtonPreset1.Checked = true;
        }
        #endregion

        #region MenuStrip MainPage
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose(true);
            Close();
        }
        private void GithubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/yusuftuncay");
        }
        private void EmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string recipient = "yusuf@tuncay.be";
                string subject = "QuickerChat Question";
                string mailtoUri = $"mailto:{recipient}?subject={Uri.EscapeDataString(subject)}";
                Process.Start(mailtoUri);
            } catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}