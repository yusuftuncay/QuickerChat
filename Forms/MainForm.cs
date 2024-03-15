#region Using
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
#endregion

namespace QuickerChat.Forms
{
    public partial class MainForm : Form
    {
        #region Variables
        /// <summary>
        /// Background worker for continuous controller polling
        /// </summary>
        private BackgroundWorker backgroundWorker;

        /// <summary>
        /// XInput controller for handling controller input
        /// </summary>
        private Controller controller;

        /// <summary>
        /// Flag indicating whether the background worker loop is running
        /// </summary>
        private bool isBackgroundWorkerLoop;

        /// <summary>
        /// Form for changing keybindings
        /// </summary>
        private static ChangeKeybindForm changeKeybindForm;

        /// <summary>
        /// Text to be used for spamming
        /// </summary>
        private string textToSpam;
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
        /// Initialize the controller and start the background worker
        /// </summary>
        private void InitializeController()
        {
            // Initialize UI labels
            LabelController.Text = "";
            LabelController.ForeColor = Color.Yellow;

            // Initialize XInput controller
            controller = new Controller(UserIndex.One);

            // Check if controller is connected
            if (controller.IsConnected)
            {
                LabelController.Text = $"Controller Connected\n{controller}";
                LabelController.ForeColor = Color.Green;
                GroupBoxPresets.Enabled = true;
                ButtonCheckController.Enabled = false;
                ButtonReset.Enabled = true;
                isBackgroundWorkerLoop = true;
            } else
            {
                LabelController.Text = "No Controller Connected";
                LabelController.ForeColor = Color.Red;
                GroupBoxPresets.Enabled = false;
                ButtonCheckController.Enabled = true;
                ButtonReset.Enabled = false;
                isBackgroundWorkerLoop = false;
                return;
            }

            // Start background worker for continuous controller polling
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync();
        }
        /// <summary>
        /// Background worker method for continuous controller polling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isBackgroundWorkerLoop)
            {
                // Get current state of the controller
                var state = controller.GetState();

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
                    SendKeys.SendWait($"T{textToSpam}{{ENTER}}");
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
            // Disable "changeKeybindForm"
            Enabled = false;

            // Open keybind change form
            changeKeybindForm = new ChangeKeybindForm();
            changeKeybindForm.FormClosed += (s, args) => { Enabled = true; }; // Re-enable main form when KeybindForm is closed
            changeKeybindForm.Show();
        }
        #endregion

        #region "Changed" Events
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Check for custom input
            if (RadioButtonCustomPreset.Checked)
            {
                textToSpam = null;
                TextBoxCustom.Enabled = true;
            } else
            {
                textToSpam = ((RadioButton)sender).Text;
                TextBoxCustom.Enabled = false;
            }
        }
        private void TextBoxCustom_TextChanged(object sender, EventArgs e)
        {
            textToSpam = TextBoxCustom.Text;
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Dispose managed resources
        /// </summary>
        private void CleanStates()
        {
            isBackgroundWorkerLoop = false;

            // Dispose background worker
            if (backgroundWorker != null)
            {
                backgroundWorker.DoWork -= BackgroundWorker_DoWork;
                backgroundWorker.Dispose();
                backgroundWorker = null;
            }

            // Dispose controller
            if (controller != null)
            {
                //controller.Dispose();
                controller = null;
            }

            // Update UI labels
            LabelController.Text = "No Controller Connected";
            LabelController.ForeColor = Color.Red;

            // Reset preset selection
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
        private void VersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the assembly version
            AssemblyName assemblyName  = Assembly.GetExecutingAssembly().GetName();

            // Display the version information in a MessageBox
            MessageBox.Show($"{assemblyName.Name} Version: {assemblyName.Version.Major}.{assemblyName.Version.Minor}.{assemblyName.Version.Build}");
        }
        #endregion
    }
}