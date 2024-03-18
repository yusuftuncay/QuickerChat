using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
        private static readonly ChangeKeybindForm _changeKeybindForm = new ChangeKeybindForm();

        /// <summary>
        /// Text to be used for spamming
        /// </summary>
        private static string _textToSpam;

        /// <summary>
        /// String representing the selected keybind
        /// </summary>
        public static string[] Keybind { get; set; } = new string[2];

        /// <summary>
        /// Gets the states of all buttons on the gamepad
        /// </summary>
        /// <param name="state">The current state of the gamepad.</param>
        /// <returns>A dictionary containing the names and states of all buttons, along with a summary of whether any button is pressed</returns>
        private Dictionary<string, bool> GetButtonStates(JoystickState state)
        {
            return new Dictionary<string, bool>
            {
                {"DPadUp", state.PointOfViewControllers[0] == 0},
                {"DPadRight", state.PointOfViewControllers[0] == 9000},
                {"DPadDown", state.PointOfViewControllers[0] == 18000},
                {"DPadLeft", state.PointOfViewControllers[0] == 27000},
                {"Square", state.Buttons[0]},
                {"Cross", state.Buttons[1]},
                {"Circle", state.Buttons[2]},
                {"Triangle", state.Buttons[3]},
                {"L1", state.Buttons[4]},
                {"R1", state.Buttons[5]},
                {"L2", state.Buttons[6]},
                {"R2", state.Buttons[7]},
                {"Share", state.Buttons[8]},
                {"Start", state.Buttons[9]},
                {"L3", state.Buttons[10]},
                {"R3", state.Buttons[11]},
                {"PS", state.Buttons[12]},
                {"Touchpad", state.Buttons[13]},
                {"Mute", state.Buttons[14]}
            };
        }
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

                // Update UI
                if (_joystick != null)
                {
                    // Label
                    LabelController.Text = $"Controller Connected\n{_joystick.Information.InstanceName}";
                    LabelController.ForeColor = Color.Green;

                    // GroupBox
                    GroupBoxPresets.Enabled = true;
                    ButtonSearchController.Enabled = false;
                    ButtonDisconnectController.Enabled = true;
                    ButtonChangeKeybind.Enabled = true;
                    _isBackgroundWorkerLoop = true;
                    _textToSpam = RadioButtonPreset1.Text;
                }
                else
                {
                    // Label
                    LabelController.Text = "No Controller Found";
                    LabelController.ForeColor = Color.Red;
                    
                    // GroupBox
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

                    // Get the current controller state
                    JoystickState state = _joystick.GetCurrentState();

                    // Get button states
                    Dictionary<string, bool> buttonStates = GetButtonStates(state);

                    // Check
                    bool isKeybind0Pressed = Keybind[0] != null && buttonStates.TryGetValue(Keybind[0], out bool value0) && value0;
                    bool isKeybind1Pressed = Keybind[1] != null && buttonStates.TryGetValue(Keybind[1], out bool value1) && value1;

                    if (isKeybind0Pressed && isKeybind1Pressed)
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
                var process = Process.GetProcessesByName("RocketLeague").FirstOrDefault();

                if (process == null)
                    return;

                // Wait for process to be ready for input
                process.WaitForInputIdle();

                // Set focus to Rocket League window (to be sure)
                SetForegroundWindow(process.MainWindowHandle);

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
            _changeKeybindForm.FormClosed += ChangeKeybindForm_FormClosed;
            _changeKeybindForm.Show();
        }
        private void ChangeKeybindForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Re-enable main form when KeybindForm is closed
            Enabled = true;

            // Enable background loop
            _isBackgroundWorkerLoop = true;
            _backgroundWorker.RunWorkerAsync();
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