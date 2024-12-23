namespace WinFormsSample
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            infoGroupBox = new GroupBox();
            rsxTemperatureLabel = new Label();
            cpuTemperatureLabel = new Label();
            firmwareVersionTextBox = new Label();
            getInfoButton = new Button();
            groupBox2 = new GroupBox();
            attachPidButton = new Button();
            getPidsButton = new Button();
            pidsComboBox = new ComboBox();
            label2 = new Label();
            disconnectButton = new Button();
            connectButton = new Button();
            ipAddressTextBox = new TextBox();
            label1 = new Label();
            memoryGroupBox = new GroupBox();
            bytesRichTextBox = new RichTextBox();
            getBytesButton = new Button();
            setByteButton = new Button();
            getByteButton = new Button();
            valueNumericUpDown = new NumericUpDown();
            label4 = new Label();
            addressNumericUpDown = new NumericUpDown();
            label3 = new Label();
            infoGroupBox.SuspendLayout();
            groupBox2.SuspendLayout();
            memoryGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)valueNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)addressNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // infoGroupBox
            // 
            infoGroupBox.Controls.Add(rsxTemperatureLabel);
            infoGroupBox.Controls.Add(cpuTemperatureLabel);
            infoGroupBox.Controls.Add(firmwareVersionTextBox);
            infoGroupBox.Controls.Add(getInfoButton);
            infoGroupBox.Enabled = false;
            infoGroupBox.Location = new Point(12, 196);
            infoGroupBox.Name = "infoGroupBox";
            infoGroupBox.Size = new Size(200, 242);
            infoGroupBox.TabIndex = 3;
            infoGroupBox.TabStop = false;
            infoGroupBox.Text = "Info.";
            // 
            // rsxTemperatureLabel
            // 
            rsxTemperatureLabel.AutoSize = true;
            rsxTemperatureLabel.Location = new Point(6, 78);
            rsxTemperatureLabel.Name = "rsxTemperatureLabel";
            rsxTemperatureLabel.Size = new Size(124, 15);
            rsxTemperatureLabel.TabIndex = 7;
            rsxTemperatureLabel.Text = "RSX Temperature: N/A";
            // 
            // cpuTemperatureLabel
            // 
            cpuTemperatureLabel.AutoSize = true;
            cpuTemperatureLabel.Location = new Point(6, 63);
            cpuTemperatureLabel.Name = "cpuTemperatureLabel";
            cpuTemperatureLabel.Size = new Size(127, 15);
            cpuTemperatureLabel.TabIndex = 6;
            cpuTemperatureLabel.Text = "CPU Temperature: N/A";
            // 
            // firmwareVersionTextBox
            // 
            firmwareVersionTextBox.AutoSize = true;
            firmwareVersionTextBox.Location = new Point(6, 48);
            firmwareVersionTextBox.Name = "firmwareVersionTextBox";
            firmwareVersionTextBox.Size = new Size(125, 15);
            firmwareVersionTextBox.TabIndex = 5;
            firmwareVersionTextBox.Text = "Firmware Version: N/A";
            // 
            // getInfoButton
            // 
            getInfoButton.Location = new Point(6, 22);
            getInfoButton.Name = "getInfoButton";
            getInfoButton.Size = new Size(75, 23);
            getInfoButton.TabIndex = 5;
            getInfoButton.Text = "Get Info.";
            getInfoButton.UseVisualStyleBackColor = true;
            getInfoButton.Click += getInfoButton_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(attachPidButton);
            groupBox2.Controls.Add(getPidsButton);
            groupBox2.Controls.Add(pidsComboBox);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(disconnectButton);
            groupBox2.Controls.Add(connectButton);
            groupBox2.Controls.Add(ipAddressTextBox);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 177);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Connection";
            // 
            // attachPidButton
            // 
            attachPidButton.Location = new Point(87, 136);
            attachPidButton.Name = "attachPidButton";
            attachPidButton.Size = new Size(75, 23);
            attachPidButton.TabIndex = 9;
            attachPidButton.Text = "Attach PID";
            attachPidButton.UseVisualStyleBackColor = true;
            attachPidButton.Click += attachPidButton_Click;
            // 
            // getPidsButton
            // 
            getPidsButton.Location = new Point(6, 136);
            getPidsButton.Name = "getPidsButton";
            getPidsButton.Size = new Size(75, 23);
            getPidsButton.TabIndex = 8;
            getPidsButton.Text = "Get PIDs";
            getPidsButton.UseVisualStyleBackColor = true;
            getPidsButton.Click += getPidsButton_Click;
            // 
            // pidsComboBox
            // 
            pidsComboBox.FormattingEnabled = true;
            pidsComboBox.Location = new Point(6, 107);
            pidsComboBox.Name = "pidsComboBox";
            pidsComboBox.Size = new Size(121, 23);
            pidsComboBox.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 89);
            label2.Name = "label2";
            label2.Size = new Size(25, 15);
            label2.TabIndex = 6;
            label2.Text = "PID";
            // 
            // disconnectButton
            // 
            disconnectButton.Location = new Point(87, 66);
            disconnectButton.Name = "disconnectButton";
            disconnectButton.Size = new Size(75, 23);
            disconnectButton.TabIndex = 5;
            disconnectButton.Text = "Disconnect";
            disconnectButton.UseVisualStyleBackColor = true;
            disconnectButton.Click += disconnectButton_Click;
            // 
            // connectButton
            // 
            connectButton.Location = new Point(6, 66);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(75, 23);
            connectButton.TabIndex = 5;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // ipAddressTextBox
            // 
            ipAddressTextBox.Location = new Point(6, 37);
            ipAddressTextBox.Name = "ipAddressTextBox";
            ipAddressTextBox.Size = new Size(100, 23);
            ipAddressTextBox.TabIndex = 4;
            ipAddressTextBox.Text = "192.168.1.1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 3;
            label1.Text = "IP Address";
            // 
            // memoryGroupBox
            // 
            memoryGroupBox.Controls.Add(bytesRichTextBox);
            memoryGroupBox.Controls.Add(getBytesButton);
            memoryGroupBox.Controls.Add(setByteButton);
            memoryGroupBox.Controls.Add(getByteButton);
            memoryGroupBox.Controls.Add(valueNumericUpDown);
            memoryGroupBox.Controls.Add(label4);
            memoryGroupBox.Controls.Add(addressNumericUpDown);
            memoryGroupBox.Controls.Add(label3);
            memoryGroupBox.Enabled = false;
            memoryGroupBox.Location = new Point(218, 12);
            memoryGroupBox.Name = "memoryGroupBox";
            memoryGroupBox.Size = new Size(216, 426);
            memoryGroupBox.TabIndex = 5;
            memoryGroupBox.TabStop = false;
            memoryGroupBox.Text = "Memory";
            // 
            // bytesRichTextBox
            // 
            bytesRichTextBox.Location = new Point(6, 169);
            bytesRichTextBox.Name = "bytesRichTextBox";
            bytesRichTextBox.Size = new Size(156, 96);
            bytesRichTextBox.TabIndex = 11;
            bytesRichTextBox.Text = "";
            // 
            // getBytesButton
            // 
            getBytesButton.Location = new Point(6, 140);
            getBytesButton.Name = "getBytesButton";
            getBytesButton.Size = new Size(75, 23);
            getBytesButton.TabIndex = 10;
            getBytesButton.Text = "Get Bytes";
            getBytesButton.UseVisualStyleBackColor = true;
            getBytesButton.Click += getBytesButton_Click;
            // 
            // setByteButton
            // 
            setByteButton.Location = new Point(87, 111);
            setByteButton.Name = "setByteButton";
            setByteButton.Size = new Size(75, 23);
            setByteButton.TabIndex = 9;
            setByteButton.Text = "Set Byte";
            setByteButton.UseVisualStyleBackColor = true;
            setByteButton.Click += setByteButton_Click;
            // 
            // getByteButton
            // 
            getByteButton.Location = new Point(6, 111);
            getByteButton.Name = "getByteButton";
            getByteButton.Size = new Size(75, 23);
            getByteButton.TabIndex = 8;
            getByteButton.Text = "Get Byte";
            getByteButton.UseVisualStyleBackColor = true;
            getByteButton.Click += getByteButton_Click;
            // 
            // valueNumericUpDown
            // 
            valueNumericUpDown.Hexadecimal = true;
            valueNumericUpDown.Location = new Point(6, 81);
            valueNumericUpDown.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            valueNumericUpDown.Name = "valueNumericUpDown";
            valueNumericUpDown.Size = new Size(120, 23);
            valueNumericUpDown.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 63);
            label4.Name = "label4";
            label4.Size = new Size(68, 15);
            label4.TabIndex = 6;
            label4.Text = "Value (HEX)";
            // 
            // addressNumericUpDown
            // 
            addressNumericUpDown.Hexadecimal = true;
            addressNumericUpDown.Location = new Point(6, 37);
            addressNumericUpDown.Maximum = new decimal(new int[] { -1, 0, 0, 0 });
            addressNumericUpDown.Name = "addressNumericUpDown";
            addressNumericUpDown.Size = new Size(120, 23);
            addressNumericUpDown.TabIndex = 5;
            addressNumericUpDown.Value = new decimal(new int[] { 65536, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 19);
            label3.Name = "label3";
            label3.Size = new Size(82, 15);
            label3.TabIndex = 4;
            label3.Text = "Address (HEX)";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(memoryGroupBox);
            Controls.Add(groupBox2);
            Controls.Add(infoGroupBox);
            Name = "Form1";
            Text = "Form1";
            infoGroupBox.ResumeLayout(false);
            infoGroupBox.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            memoryGroupBox.ResumeLayout(false);
            memoryGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)valueNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)addressNumericUpDown).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox infoGroupBox;
        private Label firmwareVersionTextBox;
        private Button getInfoButton;
        private GroupBox groupBox2;
        private Button disconnectButton;
        private Button connectButton;
        private TextBox ipAddressTextBox;
        private Label label1;
        private GroupBox memoryGroupBox;
        private Label rsxTemperatureLabel;
        private Label cpuTemperatureLabel;
        private Button setByteButton;
        private Button getByteButton;
        private NumericUpDown valueNumericUpDown;
        private Label label4;
        private NumericUpDown addressNumericUpDown;
        private Label label3;
        private Button attachPidButton;
        private Button getPidsButton;
        private ComboBox pidsComboBox;
        private Label label2;
        private Button getBytesButton;
        private RichTextBox bytesRichTextBox;
    }
}
