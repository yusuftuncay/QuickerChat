using SharpDX.XInput;
using System;
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
        /// Background worker for continuous _controller polling
        /// </summary>
        private BackgroundWorker _backgroundWorker;

        /// <summary>
        /// XInput _controller for handling _controller input
        /// </summary>
        private Controller _controller;

        /// <summary>
        /// Flag indicating whether the background worker loop is running
        /// </summary>
        private bool _isBackgroundWorkerLoop;

        /// <summary>
        /// Form for changing keybindings
        /// </summary>
        private static ChangeKeybindForm _changeKeybindForm;

        /// <summary>
        /// Text to be used for spamming
        /// </summary>
        private string _textToSpam;

        /// <summary>
        /// String representing the selected keybind
        /// </summary>
        public static string Keybind { get; internal set; }
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
        /// <summary>
        /// Initialize the _controller and start the background worker
        /// </summary>
        private void InitializeController()
        {
            // Initialize UI labels
            LabelController.Text = "";
            LabelController.ForeColor = Color.Yellow;

            // Initialize XInput _controller
            _controller = new Controller(UserIndex.One);

            // Check if _controller is connected
            if (_controller.IsConnected)
            {
                LabelController.Text = $"Controller Connected\n{_controller}";
                LabelController.ForeColor = Color.Green;
                GroupBoxPresets.Enabled = true;
                ButtonSearchController.Enabled = false;
                ButtonDisconnectController.Enabled = true;
                ButtonChangeKeybind.Enabled = true;
                _isBackgroundWorkerLoop = true;
            } else
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

            // Start background worker for continuous _controller polling
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerAsync();
        }
        /// <summary>
        /// Background worker method for continuous _controller polling
        /// </summary>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (_isBackgroundWorkerLoop)
            {
                // Get current state of the _controller
                var state = _controller.GetState();

                // Check if the correct button combination is pressed
                bool circleButtonPressed = (state.Gamepad.Buttons & GamepadButtonFlags.A) != 0;
                bool dPadUpButtonPressed = (state.Gamepad.Buttons & GamepadButtonFlags.DPadUp) != 0;

                if (circleButtonPressed && dPadUpButtonPressed)
                {
                    StartSpam();
                    Thread.Sleep(200);
                }

                // Polling interval
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
            // Disable "_changeKeybindForm"
            Enabled = false;

            // Open keybind change form
            _changeKeybindForm = new ChangeKeybindForm(_controller);
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
        /// <summary>
        /// Dispose managed resources
        /// </summary>
        private void CleanStates()
        {
            _isBackgroundWorkerLoop = false;

            // Dispose background worker
            if (_backgroundWorker != null)
            {
                _backgroundWorker.DoWork -= BackgroundWorker_DoWork;
                _backgroundWorker.Dispose();
                _backgroundWorker = null;
            }

            // Dispose _controller
            if (_controller != null)
            {
                //_controller.Dispose();
                _controller = null;
            }

            // Update UI labels
            LabelController.Text = "No Controller Connected";
            LabelController.ForeColor = Color.Red;

            // Reset preset selection
            RadioButtonPreset1.Checked = true;
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