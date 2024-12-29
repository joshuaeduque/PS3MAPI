using MAPILib;
using System.Text;

namespace WinFormsSample
{
    public partial class Form1 : Form
    {
        private const string CONSOLE_NOT_CONNECTED = "Console not connected";

        private readonly MAPI api;
        private uint processId;

        public Form1()
        {
            InitializeComponent();

            api = new MAPI();
        }

        private bool ConnectConsole()
        {
            string ipAddress = ipAddressTextBox.Text;
            if (string.IsNullOrEmpty(ipAddress))
                return false;

            MAPIResult result = api.Connect(ipAddress);
            if (result != MAPIResult.OK)
                return false;

            return true;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            bool connected = ConnectConsole();
            if (!connected)
            {
                MessageBox.Show("Failed to connect console");
                return;
            }

            MessageBox.Show("Connected to console");

            infoGroupBox.Enabled = true;
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            if (!api.Connected)
            {
                MessageBox.Show(CONSOLE_NOT_CONNECTED);
                return;
            }

            api.Disconnect();
            MessageBox.Show("Console disconnected");
        }

        private void getInfoButton_Click(object sender, EventArgs e)
        {
            api.GetFirmwareVersion(out string? firmwareVersion);
            firmwareVersionTextBox.Text = $"Firmware Version: {firmwareVersion ?? "N/A"}";

            api.GetTemperature(out int? cpu, out int? rsx);
            cpuTemperatureLabel.Text = $"CPU Temperature: {cpu}";
            rsxTemperatureLabel.Text = $"RSX Temperature: {rsx}";
        }

        private void getPidsButton_Click(object sender, EventArgs e)
        {
            if (!api.Connected)
            {
                MessageBox.Show(CONSOLE_NOT_CONNECTED);
                return;
            }

            pidsComboBox.Items.Clear();

            uint[]? pids = api.GetProcessIds();
            if (pids == null)
            {
                MessageBox.Show("Failed to get process IDs");
                return;
            }

            foreach (uint pid in pids)
            {
                if (pid == 0)
                    continue;
                pidsComboBox.Items.Add(pid);
            }

            if (pidsComboBox.Items.Count > 0)
                pidsComboBox.SelectedIndex = 0;
        }

        private void attachPidButton_Click(object sender, EventArgs e)
        {
            if (!api.Connected)
            {
                MessageBox.Show(CONSOLE_NOT_CONNECTED);
                return;
            }

            if (pidsComboBox.SelectedItem == null) // Get and attach current pid
            {
                uint? pid = api.GetCurrentProcessId();
                if (pid == null)
                    return;

                pidsComboBox.Items.Add(pid);
                pidsComboBox.SelectedItem = pid;

                processId = (uint)pid;
            }
            else // Attach combobox pid
            {
                processId = (uint)pidsComboBox.SelectedItem;
            }

            MessageBox.Show($"Connected to PID: {processId}");

            memoryGroupBox.Enabled = true;
        }

        private void getByteButton_Click(object sender, EventArgs e)
        {
            uint address = (uint)addressNumericUpDown.Value;

            byte[]? buffer = api.GetMemory(processId, address, 1);
            if (buffer == null)
            {
                MessageBox.Show("Failed to get memory");
                return;
            }

            valueNumericUpDown.Value = buffer[0];
        }

        private void getBytesButton_Click(object sender, EventArgs e)
        {
            uint address = (uint)addressNumericUpDown.Value;

            byte[]? buffer = api.GetMemory(processId, address, 32);
            if (buffer == null)
            {
                MessageBox.Show("Failed to get memory");
                return;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                sb.Append(Convert.ToHexString(buffer, i, 1));

                if ((i + 1) % 8 == 0)
                    sb.AppendLine();
                else
                    sb.Append(' ');
            }
            bytesRichTextBox.Text = sb.ToString();
        }

        private void setByteButton_Click(object sender, EventArgs e)
        {

        }
    }
}
