namespace vdst_v1
{
    partial class frmSync
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.boxSyncSettings = new System.Windows.Forms.GroupBox();
            this.cmbSyncFlowControl = new System.Windows.Forms.ComboBox();
            this.lblSyncFlowControl = new System.Windows.Forms.Label();
            this.cmbSyncStopBits = new System.Windows.Forms.ComboBox();
            this.lblSyncStopBits = new System.Windows.Forms.Label();
            this.cmbSyncDataBits = new System.Windows.Forms.ComboBox();
            this.lblSyncDataBits = new System.Windows.Forms.Label();
            this.chkSyncDuplex = new System.Windows.Forms.CheckBox();
            this.cmbSyncParity = new System.Windows.Forms.ComboBox();
            this.lblSyncParity = new System.Windows.Forms.Label();
            this.cmbSyncBaud = new System.Windows.Forms.ComboBox();
            this.lblSyncBaud = new System.Windows.Forms.Label();
            this.cmbSyncPort = new System.Windows.Forms.ComboBox();
            this.lblSyncPort = new System.Windows.Forms.Label();
            this.btnSyncOK = new System.Windows.Forms.Button();
            this.txtSyncCommand = new System.Windows.Forms.TextBox();
            this.lblSyncCommand = new System.Windows.Forms.Label();
            this.btnSyncSend = new System.Windows.Forms.Button();
            this.lblSyncResponse = new System.Windows.Forms.Label();
            this.txtSyncResponse = new System.Windows.Forms.TextBox();
            this.cmbSyncType = new System.Windows.Forms.ComboBox();
            this.btnSync = new System.Windows.Forms.Button();
            this.lblSyncType = new System.Windows.Forms.Label();
            this.tmrSyncError = new System.Windows.Forms.Timer(this.components);
            this.statusSync = new System.Windows.Forms.StatusStrip();
            this.statusSyncProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.statusSyncError = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSyncTimeout = new System.Windows.Forms.Label();
            this.cmbSyncTimeout = new System.Windows.Forms.ComboBox();
            this.tmrSync = new System.Windows.Forms.Timer(this.components);
            this.boxSyncSettings.SuspendLayout();
            this.statusSync.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxSyncSettings
            // 
            this.boxSyncSettings.Controls.Add(this.cmbSyncFlowControl);
            this.boxSyncSettings.Controls.Add(this.lblSyncFlowControl);
            this.boxSyncSettings.Controls.Add(this.cmbSyncStopBits);
            this.boxSyncSettings.Controls.Add(this.lblSyncStopBits);
            this.boxSyncSettings.Controls.Add(this.cmbSyncDataBits);
            this.boxSyncSettings.Controls.Add(this.lblSyncDataBits);
            this.boxSyncSettings.Controls.Add(this.chkSyncDuplex);
            this.boxSyncSettings.Controls.Add(this.cmbSyncParity);
            this.boxSyncSettings.Controls.Add(this.lblSyncParity);
            this.boxSyncSettings.Controls.Add(this.cmbSyncBaud);
            this.boxSyncSettings.Controls.Add(this.lblSyncBaud);
            this.boxSyncSettings.Controls.Add(this.cmbSyncPort);
            this.boxSyncSettings.Controls.Add(this.lblSyncPort);
            this.boxSyncSettings.Location = new System.Drawing.Point(29, 272);
            this.boxSyncSettings.Margin = new System.Windows.Forms.Padding(4);
            this.boxSyncSettings.Name = "boxSyncSettings";
            this.boxSyncSettings.Padding = new System.Windows.Forms.Padding(4);
            this.boxSyncSettings.Size = new System.Drawing.Size(913, 255);
            this.boxSyncSettings.TabIndex = 7;
            this.boxSyncSettings.TabStop = false;
            this.boxSyncSettings.Text = "Settings: ";
            // 
            // cmbSyncFlowControl
            // 
            this.cmbSyncFlowControl.FormattingEnabled = true;
            this.cmbSyncFlowControl.Items.AddRange(new object[] {
            "None",
            "RTS/CTS",
            "DTR/DSR",
            "RS485-rts"});
            this.cmbSyncFlowControl.Location = new System.Drawing.Point(209, 199);
            this.cmbSyncFlowControl.Name = "cmbSyncFlowControl";
            this.cmbSyncFlowControl.Size = new System.Drawing.Size(236, 40);
            this.cmbSyncFlowControl.TabIndex = 19;
            this.cmbSyncFlowControl.Text = "None";
            // 
            // lblSyncFlowControl
            // 
            this.lblSyncFlowControl.AutoSize = true;
            this.lblSyncFlowControl.Location = new System.Drawing.Point(27, 202);
            this.lblSyncFlowControl.Name = "lblSyncFlowControl";
            this.lblSyncFlowControl.Size = new System.Drawing.Size(176, 32);
            this.lblSyncFlowControl.TabIndex = 18;
            this.lblSyncFlowControl.Text = "Flow Control:";
            // 
            // cmbSyncStopBits
            // 
            this.cmbSyncStopBits.FormattingEnabled = true;
            this.cmbSyncStopBits.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmbSyncStopBits.Location = new System.Drawing.Point(459, 129);
            this.cmbSyncStopBits.Name = "cmbSyncStopBits";
            this.cmbSyncStopBits.Size = new System.Drawing.Size(121, 40);
            this.cmbSyncStopBits.TabIndex = 15;
            this.cmbSyncStopBits.Text = "1";
            // 
            // lblSyncStopBits
            // 
            this.lblSyncStopBits.AutoSize = true;
            this.lblSyncStopBits.Location = new System.Drawing.Point(320, 132);
            this.lblSyncStopBits.Name = "lblSyncStopBits";
            this.lblSyncStopBits.Size = new System.Drawing.Size(133, 32);
            this.lblSyncStopBits.TabIndex = 14;
            this.lblSyncStopBits.Text = "Stop Bits:";
            // 
            // cmbSyncDataBits
            // 
            this.cmbSyncDataBits.FormattingEnabled = true;
            this.cmbSyncDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cmbSyncDataBits.Location = new System.Drawing.Point(168, 129);
            this.cmbSyncDataBits.Name = "cmbSyncDataBits";
            this.cmbSyncDataBits.Size = new System.Drawing.Size(121, 40);
            this.cmbSyncDataBits.TabIndex = 13;
            this.cmbSyncDataBits.Text = "8";
            // 
            // lblSyncDataBits
            // 
            this.lblSyncDataBits.AutoSize = true;
            this.lblSyncDataBits.Location = new System.Drawing.Point(27, 132);
            this.lblSyncDataBits.Name = "lblSyncDataBits";
            this.lblSyncDataBits.Size = new System.Drawing.Size(135, 32);
            this.lblSyncDataBits.TabIndex = 12;
            this.lblSyncDataBits.Text = "Data Bits:";
            // 
            // chkSyncDuplex
            // 
            this.chkSyncDuplex.AutoSize = true;
            this.chkSyncDuplex.Location = new System.Drawing.Point(630, 62);
            this.chkSyncDuplex.Name = "chkSyncDuplex";
            this.chkSyncDuplex.Size = new System.Drawing.Size(185, 36);
            this.chkSyncDuplex.TabIndex = 11;
            this.chkSyncDuplex.Text = "Half Duplex";
            this.chkSyncDuplex.UseVisualStyleBackColor = true;
            // 
            // cmbSyncParity
            // 
            this.cmbSyncParity.FormattingEnabled = true;
            this.cmbSyncParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.cmbSyncParity.Location = new System.Drawing.Point(722, 129);
            this.cmbSyncParity.Name = "cmbSyncParity";
            this.cmbSyncParity.Size = new System.Drawing.Size(121, 40);
            this.cmbSyncParity.TabIndex = 17;
            this.cmbSyncParity.Text = "None";
            // 
            // lblSyncParity
            // 
            this.lblSyncParity.AutoSize = true;
            this.lblSyncParity.Location = new System.Drawing.Point(624, 132);
            this.lblSyncParity.Name = "lblSyncParity";
            this.lblSyncParity.Size = new System.Drawing.Size(92, 32);
            this.lblSyncParity.TabIndex = 16;
            this.lblSyncParity.Text = "Parity:";
            // 
            // cmbSyncBaud
            // 
            this.cmbSyncBaud.FormattingEnabled = true;
            this.cmbSyncBaud.Items.AddRange(new object[] {
            "110",
            "150",
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.cmbSyncBaud.Location = new System.Drawing.Point(459, 60);
            this.cmbSyncBaud.Name = "cmbSyncBaud";
            this.cmbSyncBaud.Size = new System.Drawing.Size(151, 40);
            this.cmbSyncBaud.TabIndex = 10;
            this.cmbSyncBaud.Text = "57600";
            // 
            // lblSyncBaud
            // 
            this.lblSyncBaud.AutoSize = true;
            this.lblSyncBaud.Location = new System.Drawing.Point(320, 63);
            this.lblSyncBaud.Name = "lblSyncBaud";
            this.lblSyncBaud.Size = new System.Drawing.Size(86, 32);
            this.lblSyncBaud.TabIndex = 9;
            this.lblSyncBaud.Text = "Baud:";
            // 
            // cmbSyncPort
            // 
            this.cmbSyncPort.FormattingEnabled = true;
            this.cmbSyncPort.Location = new System.Drawing.Point(168, 60);
            this.cmbSyncPort.Name = "cmbSyncPort";
            this.cmbSyncPort.Size = new System.Drawing.Size(121, 40);
            this.cmbSyncPort.TabIndex = 8;
            this.cmbSyncPort.TextChanged += new System.EventHandler(this.cmbSyncPort_TextChanged);
            this.cmbSyncPort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmbSyncPort_MouseDown);
            // 
            // lblSyncPort
            // 
            this.lblSyncPort.AutoSize = true;
            this.lblSyncPort.Location = new System.Drawing.Point(27, 63);
            this.lblSyncPort.Name = "lblSyncPort";
            this.lblSyncPort.Size = new System.Drawing.Size(72, 32);
            this.lblSyncPort.TabIndex = 7;
            this.lblSyncPort.Text = "Port:";
            // 
            // btnSyncOK
            // 
            this.btnSyncOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSyncOK.Location = new System.Drawing.Point(766, 635);
            this.btnSyncOK.Name = "btnSyncOK";
            this.btnSyncOK.Size = new System.Drawing.Size(176, 56);
            this.btnSyncOK.TabIndex = 8;
            this.btnSyncOK.Text = "OK";
            this.btnSyncOK.UseVisualStyleBackColor = true;
            // 
            // txtSyncCommand
            // 
            this.txtSyncCommand.Location = new System.Drawing.Point(29, 83);
            this.txtSyncCommand.Name = "txtSyncCommand";
            this.txtSyncCommand.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSyncCommand.Size = new System.Drawing.Size(783, 39);
            this.txtSyncCommand.TabIndex = 9;
            // 
            // lblSyncCommand
            // 
            this.lblSyncCommand.AutoSize = true;
            this.lblSyncCommand.Location = new System.Drawing.Point(29, 43);
            this.lblSyncCommand.Name = "lblSyncCommand";
            this.lblSyncCommand.Size = new System.Drawing.Size(147, 32);
            this.lblSyncCommand.TabIndex = 10;
            this.lblSyncCommand.Text = "Command:";
            // 
            // btnSyncSend
            // 
            this.btnSyncSend.Location = new System.Drawing.Point(821, 74);
            this.btnSyncSend.Name = "btnSyncSend";
            this.btnSyncSend.Size = new System.Drawing.Size(121, 56);
            this.btnSyncSend.TabIndex = 11;
            this.btnSyncSend.Text = "Send";
            this.btnSyncSend.UseVisualStyleBackColor = true;
            this.btnSyncSend.Click += new System.EventHandler(this.btnSyncSend_Click);
            // 
            // lblSyncResponse
            // 
            this.lblSyncResponse.AutoSize = true;
            this.lblSyncResponse.Location = new System.Drawing.Point(29, 145);
            this.lblSyncResponse.Name = "lblSyncResponse";
            this.lblSyncResponse.Size = new System.Drawing.Size(146, 32);
            this.lblSyncResponse.TabIndex = 12;
            this.lblSyncResponse.Text = "Response:";
            // 
            // txtSyncResponse
            // 
            this.txtSyncResponse.Location = new System.Drawing.Point(29, 188);
            this.txtSyncResponse.Name = "txtSyncResponse";
            this.txtSyncResponse.Size = new System.Drawing.Size(913, 39);
            this.txtSyncResponse.TabIndex = 13;
            // 
            // cmbSyncType
            // 
            this.cmbSyncType.FormattingEnabled = true;
            this.cmbSyncType.Items.AddRange(new object[] {
            "Synchronize All",
            "Synchronize User Data",
            "Synchromize Field Help",
            "Synchromize Log"});
            this.cmbSyncType.Location = new System.Drawing.Point(192, 565);
            this.cmbSyncType.Name = "cmbSyncType";
            this.cmbSyncType.Size = new System.Drawing.Size(313, 40);
            this.cmbSyncType.TabIndex = 14;
            this.cmbSyncType.Text = "Synchronize All";
            // 
            // btnSync
            // 
            this.btnSync.Enabled = false;
            this.btnSync.Location = new System.Drawing.Point(584, 635);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(176, 56);
            this.btnSync.TabIndex = 15;
            this.btnSync.Text = "Sync";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // lblSyncType
            // 
            this.lblSyncType.AutoSize = true;
            this.lblSyncType.Location = new System.Drawing.Point(35, 565);
            this.lblSyncType.Name = "lblSyncType";
            this.lblSyncType.Size = new System.Drawing.Size(151, 32);
            this.lblSyncType.TabIndex = 16;
            this.lblSyncType.Text = "Sync Type:";
            // 
            // tmrSyncError
            // 
            this.tmrSyncError.Interval = 2500;
            this.tmrSyncError.Tick += new System.EventHandler(this.tmrSyncError_Tick);
            // 
            // statusSync
            // 
            this.statusSync.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusSync.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusSyncProgress,
            this.statusSyncError});
            this.statusSync.Location = new System.Drawing.Point(0, 713);
            this.statusSync.Name = "statusSync";
            this.statusSync.Size = new System.Drawing.Size(973, 38);
            this.statusSync.SizingGrip = false;
            this.statusSync.TabIndex = 17;
            // 
            // statusSyncProgress
            // 
            this.statusSyncProgress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.statusSyncProgress.Name = "statusSyncProgress";
            this.statusSyncProgress.Size = new System.Drawing.Size(100, 32);
            this.statusSyncProgress.Tag = "";
            // 
            // statusSyncError
            // 
            this.statusSyncError.Name = "statusSyncError";
            this.statusSyncError.Size = new System.Drawing.Size(0, 33);
            // 
            // lblSyncTimeout
            // 
            this.lblSyncTimeout.AutoSize = true;
            this.lblSyncTimeout.Location = new System.Drawing.Point(552, 568);
            this.lblSyncTimeout.Name = "lblSyncTimeout";
            this.lblSyncTimeout.Size = new System.Drawing.Size(188, 32);
            this.lblSyncTimeout.TabIndex = 18;
            this.lblSyncTimeout.Text = "Timeout (sec):";
            // 
            // cmbSyncTimeout
            // 
            this.cmbSyncTimeout.FormattingEnabled = true;
            this.cmbSyncTimeout.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbSyncTimeout.Location = new System.Drawing.Point(747, 566);
            this.cmbSyncTimeout.Name = "cmbSyncTimeout";
            this.cmbSyncTimeout.Size = new System.Drawing.Size(195, 40);
            this.cmbSyncTimeout.TabIndex = 19;
            this.cmbSyncTimeout.Text = "1";
            // 
            // tmrSync
            // 
            this.tmrSync.Interval = 25;
            this.tmrSync.Tick += new System.EventHandler(this.tmrSync_Tick);
            // 
            // frmSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(973, 751);
            this.Controls.Add(this.cmbSyncTimeout);
            this.Controls.Add(this.lblSyncTimeout);
            this.Controls.Add(this.statusSync);
            this.Controls.Add(this.lblSyncType);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.cmbSyncType);
            this.Controls.Add(this.txtSyncResponse);
            this.Controls.Add(this.lblSyncResponse);
            this.Controls.Add(this.btnSyncSend);
            this.Controls.Add(this.lblSyncCommand);
            this.Controls.Add(this.txtSyncCommand);
            this.Controls.Add(this.btnSyncOK);
            this.Controls.Add(this.boxSyncSettings);
            this.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSync";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VDST - Sync";
            this.Load += new System.EventHandler(this.frmSync_Load);
            this.boxSyncSettings.ResumeLayout(false);
            this.boxSyncSettings.PerformLayout();
            this.statusSync.ResumeLayout(false);
            this.statusSync.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox boxSyncSettings;
        private System.Windows.Forms.ComboBox cmbSyncFlowControl;
        private System.Windows.Forms.Label lblSyncFlowControl;
        private System.Windows.Forms.ComboBox cmbSyncStopBits;
        private System.Windows.Forms.Label lblSyncStopBits;
        private System.Windows.Forms.ComboBox cmbSyncDataBits;
        private System.Windows.Forms.Label lblSyncDataBits;
        private System.Windows.Forms.CheckBox chkSyncDuplex;
        private System.Windows.Forms.ComboBox cmbSyncParity;
        private System.Windows.Forms.Label lblSyncParity;
        private System.Windows.Forms.ComboBox cmbSyncBaud;
        private System.Windows.Forms.Label lblSyncBaud;
        private System.Windows.Forms.ComboBox cmbSyncPort;
        private System.Windows.Forms.Label lblSyncPort;
        private System.Windows.Forms.Button btnSyncOK;
        private System.Windows.Forms.TextBox txtSyncCommand;
        private System.Windows.Forms.Label lblSyncCommand;
        private System.Windows.Forms.Button btnSyncSend;
        private System.Windows.Forms.Label lblSyncResponse;
        private System.Windows.Forms.TextBox txtSyncResponse;
        private System.Windows.Forms.ComboBox cmbSyncType;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Label lblSyncType;
        private System.Windows.Forms.Timer tmrSyncError;
        private System.Windows.Forms.StatusStrip statusSync;
        private System.Windows.Forms.ToolStripStatusLabel statusSyncError;
        private System.Windows.Forms.ToolStripProgressBar statusSyncProgress;
        private System.Windows.Forms.Label lblSyncTimeout;
        private System.Windows.Forms.ComboBox cmbSyncTimeout;
        private System.Windows.Forms.Timer tmrSync;
    }
}