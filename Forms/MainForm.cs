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
        /// Background worker for continuous directInput polling
        /// </summary>
        private static BackgroundWorker backgroundWorker;

        /// <summary>
        /// SharpDX.DirectInput for handling directInput input
        /// </summary>
        private static DirectInput directInput;

        /// <summary>
        /// SharpDX.Joystick for handling directInput input
        /// </summary>
        private static Joystick joystick;

        /// <summary>
        /// Flag indicating whether the background worker loop is running
        /// </summary>
        private static bool isBackgroundWorkerLoop;

        /// <summary>
        /// Form for changing keybindings
        /// </summary>
        private static ChangeKeybindForm changeKeybindForm;

        /// <summary>
        /// Text to be used for spamming
        /// </summary>
        private static string textToSpam;

        /// <summary>
        /// String representing the selected keybind
        /// </summary>
        public static string[] Keybind { get; set; } = new string[2];

        /// <summary>
        /// Gets the states of all buttons on the gamepad
        /// </summary>
        /// <param name="state">The current state of the gamepad</param>
        /// <returns>A dictionary containing the names and states of all buttons, along with a value of whether a button is pressed</returns>
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
                        joystick = new Joystick(directInput, deviceInstance.InstanceGuid);
                        break;
                    }
                }

                // Update UI
                if (joystick != null)
                {
                    // Label
                    LabelController.Text = $"Controller Connected\n{joystick.Information.InstanceName}";
                    LabelController.ForeColor = Color.Green;

                    // GroupBox
                    GroupBoxPresets.Enabled = true;
                    ButtonSearchController.Enabled = false;
                    ButtonDisconnectController.Enabled = true;
                    ButtonChangeKeybind.Enabled = true;
                    isBackgroundWorkerLoop = true;
                    textToSpam = RadioButtonPreset1.Text;
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
                    isBackgroundWorkerLoop = false;
                    return;
                }

                // Acquire the joystick
                joystick.Properties.BufferSize = 128;
                joystick.Acquire();

                // Start a background worker to continuously poll the controller
                backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += BackgroundWorker_DoWork;
                backgroundWorker.RunWorkerAsync();
            } catch (Exception) { }
        }
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isBackgroundWorkerLoop)
            {
                try
                {
                    if (joystick == null)
                        return;

                    // Poll
                    joystick.Poll();

                    // Get the current controller state
                    JoystickState state = joystick.GetCurrentState();

                    // Get button states
                    Dictionary<string, bool> buttonStates = GetButtonStates(state);

                    // Check
                    bool isKeybind0Pressed = Keybind[0] != null && buttonStates.TryGetValue(Keybind[0], out bool value0) && value0;
                    bool isKeybind1Pressed = Keybind[1] != null && buttonStates.TryGetValue(Keybind[1], out bool value1) && value1;

                    if (isKeybind0Pressed && isKeybind1Pressed)
                    {
                        Invoke((MethodInvoker)(() =>
                        {
                            // Find the Rocket League process
                            var process = Process.GetProcessesByName("RocketLeague").FirstOrDefault();

                            if (process == null)
                                return;

                            // Wait for process to be ready for input
                            process.WaitForInputIdle();

                            // Set focus to Rocket League window (to be sure)
                            SetForegroundWindow(process.MainWindowHandle);

                            // Send 3 key presses
                            for (int i = 0; i < 3; i++)
                            {
                                SendKeys.Send("T");
                                SendKeys.Send(textToSpam);
                                SendKeys.Send("{ENTER}");
                            }
                        }));
                    }

                    Thread.Sleep(10);
                } catch (Exception)
                {
                    CleanStates();
                }
            }
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
            isBackgroundWorkerLoop = false;

            // Disable MainForm
            Enabled = false;

            // Open ChangeKeybindForm
            changeKeybindForm = new ChangeKeybindForm();
            changeKeybindForm.FormClosed += ChangeKeybindForm_FormClosed;
            changeKeybindForm.Show();
        }
        private void ChangeKeybindForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Re-enable MainForm when ChangeKeybindForm is closed
            Enabled = true;

            // Enable background loop
            isBackgroundWorkerLoop = true;
            backgroundWorker.RunWorkerAsync();
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

        #region MenuStrip
        private void VersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the assembly version
            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();

            // Display the version information in a MessageBox
            MessageBox.Show($"{assemblyName.Name} Version: {assemblyName.Version.Major}.{assemblyName.Version.Minor}.{assemblyName.Version.Build}");
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CleanStates();
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

        #region CleanStates
        private void CleanStates()
        {
            // Dispose backgroundWorker
            if (backgroundWorker != null)
            {
                backgroundWorker.DoWork -= BackgroundWorker_DoWork;
                backgroundWorker.Dispose();
                backgroundWorker = null;
            }

            // Dispose directInput
            directInput?.Dispose();
            directInput = null;

            // Dispose joystick
            joystick?.Dispose();
            joystick = null;

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
            isBackgroundWorkerLoop = false;

            // Reset preset selection
            RadioButtonPreset1.Checked = true;
            textToSpam = RadioButtonPreset1.Text;
        }
        #endregion
    }
}