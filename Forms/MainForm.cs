using SharpDX.DirectInput;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace QuickerChat.Forms
{
    public partial class MainForm : Form
    {
        #region Variables
        /// <summary>
        /// Background worker for continuous _directInput polling
        /// </summary>
        private static BackgroundWorker _backgroundWorker;

        /// <summary>
        /// XInput DirectInput for handling _directInput input
        /// </summary>
        private static DirectInput _directInput;

        /// <summary>
        /// XInput Joystick for handling _directInput input
        /// </summary>
        private static Joystick _joystick;

        /// <summary>
        /// Flag indicating whether the background worker loop is running
        /// </summary>
        private static bool _isBackgroundWorkerLoop;

        /// <summary>
        /// Form for changing keybindings
        /// </summary>
        private static ChangeKeybindForm _changeKeybindForm;

        /// <summary>
        /// Text to be used for spamming
        /// </summary>
        private static string _textToSpam;

        /// <summary>
        /// String representing the selected keybind
        /// </summary>
        public static string[] Keybind { get; internal set; } = new string[2];
        #endregion

        #region MainForm
        public MainForm()
        {
            InitializeComponent();
            InitializeController();
        }
        #endregion

        #region DLL Import
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion

        #region Controller Methods
        private void InitializeController()
        {
            // Initialize UI labels
            LabelController.Text = "";
            LabelController.ForeColor = Color.Yellow;

            try
            {
                using (var directInput = new DirectInput())
                {
                    // Get First Joystick
                    foreach (var deviceInstance in directInput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AllDevices))
                    {
                        _joystick = new Joystick(directInput, deviceInstance.InstanceGuid);
                        break;
                    }
                }

                if (_joystick != null)
                {
                    LabelController.Text = $"Controller Connected\n{_directInput}";
                    LabelController.ForeColor = Color.Green;

                    GroupBoxPresets.Enabled = true;
                    ButtonSearchController.Enabled = false;
                    ButtonDisconnectController.Enabled = true;
                    ButtonChangeKeybind.Enabled = true;
                    _isBackgroundWorkerLoop = true;
                    _textToSpam = RadioButtonPreset1.Text;
                }
                else
                {
                    LabelController.Text = "No Controller Found";
                    LabelController.ForeColor = Color.Red;

                    GroupBoxPresets.Enabled = false;
                    ButtonSearchController.Enabled = true;
                    ButtonDisconnectController.Enabled = false;
                    ButtonChangeKeybind.Enabled = false;
                    _isBackgroundWorkerLoop = false;
                    return;
                }

                // Acquire the _joystick
                _joystick.Properties.BufferSize = 16;
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
                try
                {
                    if (_joystick == null)
                        return;

                    // Poll
                    _joystick.Poll();

                    // Get the current state
                    JoystickState state = _joystick.GetCurrentState();

                    // DPad
                    bool dPadUpPressed = state.PointOfViewControllers[0] == 0;
                    bool dPadRightPressed = state.PointOfViewControllers[0] == 9000;
                    bool dPadDownPressed = state.PointOfViewControllers[0] == 18000;
                    bool dPadLeftPressed = state.PointOfViewControllers[0] == 27000;

                    // Symbol
                    bool squarePressed = state.Buttons[0];
                    bool crossPressed = state.Buttons[1];
                    bool circlePressed = state.Buttons[2];
                    bool trianglePressed = state.Buttons[3];

                    // Trigger
                    bool L1Pressed = state.Buttons[4];
                    bool R1Pressed = state.Buttons[5];
                    bool L2Pressed = state.Buttons[6];
                    bool R2Pressed = state.Buttons[7];

                    // Share & Start
                    bool sharePressed = state.Buttons[8];
                    bool startPressed = state.Buttons[9];

                    // Joystick Buttons
                    bool L3Pressed = state.Buttons[10];
                    bool R3Pressed = state.Buttons[11];

                    // Rest
                    bool PSPressed = state.Buttons[12];
                    bool touchpadPressed = state.Buttons[13];
                    bool mutePressed = state.Buttons[14];

                    if (Keybind[0] != null && Keybind[1] != null)
                    {
                        StartSpam();
                        Thread.Sleep(200);
                    }

                    Thread.Sleep(10);
                }
                catch (Exception)
                {
                    CleanStates();
                }
            }
        }
        #endregion

        #region Spam Methods
        private void StartSpam()
        {
            try
            {
                // Find the Rocket League process
                //var process = Process.GetProcessesByName("RocketLeague").FirstOrDefault();

                //if (process == null)
                //    return;

                //// Wait for process to be ready for input
                //process.WaitForInputIdle();

                //// Set focus to Rocket League window (to be sure)
                //SetForegroundWindow(process.MainWindowHandle);

                // Send key presses
                for (var i = 0; i < 3; i++)
                    SendKeys.SendWait($"T{_textToSpam}{{ENTER}}");
            } catch (Exception) { }
        }
        #endregion

        #region "Click" Events
        private void ButtonDisconnectController_Click(object sender, EventArgs e)
        {
            CleanStates();
        }
        private void ButtonSearchController_Click(object sender, EventArgs e)
        {
            InitializeController();
        }
        private void ButtonChangeKeybind_Click(object sender, EventArgs e)
        {
            // Stop Loop
            _isBackgroundWorkerLoop = false;

            // Disable Form2
            Enabled = false;

            // Open keybind change form
            _changeKeybindForm = new ChangeKeybindForm();
            _changeKeybindForm.FormClosed += (s, args) => { Enabled = true; }; // Re-enable main form when KeybindForm is closed
            _changeKeybindForm.Show();
        }
        #endregion

        #region "Changed" Events
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Check for custom input
            if (RadioButtonCustomPreset.Checked)
            {
                _textToSpam = null;
                TextBoxCustom.Enabled = true;
            } else
            {
                _textToSpam = ((RadioButton)sender).Text;
                TextBoxCustom.Enabled = false;
            }
        }
        private void TextBoxCustom_TextChanged(object sender, EventArgs e)
        {
            _textToSpam = TextBoxCustom.Text;
        }
        #endregion

        #region Dispose
        private void CleanStates()
        {
            // Dispose background worker
            if (_backgroundWorker != null)
            {
                _backgroundWorker.DoWork -= BackgroundWorker_DoWork;
                _backgroundWorker.Dispose();
                _backgroundWorker = null;
            }

            // Dispose _directInput
            _directInput?.Dispose();
            _directInput = null;

            // Dispose _joystick
            _joystick?.Dispose();
            _joystick = null;

            // Update UI label on UI thread
            Invoke((MethodInvoker)(() =>
            {
                LabelController.Text = "No Controller Connected";
                LabelController.ForeColor = Color.Red;

                // Disable GroupBox
                GroupBoxPresets.Enabled = false;

                // Make button visible
                ButtonSearchController.Enabled = true;
                ButtonDisconnectController.Enabled = false;
                ButtonChangeKeybind.Enabled = false;
            }));

            // Stop the loop
            _isBackgroundWorkerLoop = false;

            // Reset preset selection
            RadioButtonPreset1.Checked = true;
            _textToSpam = RadioButtonPreset1.Text;
        }
        #endregion

        #region MenuStrip MainPage
        private void VersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the assembly version
            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();

            // Display the version information in a MessageBox
            MessageBox.Show($"{assemblyName.Name} Version: {assemblyName.Version.Major}.{assemblyName.Version.Minor}.{assemblyName.Version.Build}");
        }
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