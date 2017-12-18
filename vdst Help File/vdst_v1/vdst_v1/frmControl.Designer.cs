namespace vdst_v1
{
    partial class frmControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmControl));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabControlTempPage = new System.Windows.Forms.TabPage();
            this.imgControlTempBell = new System.Windows.Forms.PictureBox();
            this.lblControlTempHelp = new System.Windows.Forms.Label();
            this.txtControlTemp = new System.Windows.Forms.TextBox();
            this.txtControlTempAlarm5 = new System.Windows.Forms.TextBox();
            this.txtControlTempAlarm4 = new System.Windows.Forms.TextBox();
            this.txtControlTempAlarm3 = new System.Windows.Forms.TextBox();
            this.txtControlTempAlarm2 = new System.Windows.Forms.TextBox();
            this.txtControlTempAlarm1 = new System.Windows.Forms.TextBox();
            this.lblControlTempAlarms = new System.Windows.Forms.Label();
            this.btnControlTempType = new System.Windows.Forms.Button();
            this.lblControlTemp = new System.Windows.Forms.Label();
            this.tabControlLaserPage = new System.Windows.Forms.TabPage();
            this.txtControlLaserPulse = new System.Windows.Forms.TextBox();
            this.imgControlLaserStatus = new System.Windows.Forms.PictureBox();
            this.lblControlLaserIntensity = new System.Windows.Forms.Label();
            this.imgControlLaserCalibrateRight = new System.Windows.Forms.PictureBox();
            this.imgControlLaserCalibrateLeft = new System.Windows.Forms.PictureBox();
            this.lblControlLaserHelp = new System.Windows.Forms.Label();
            this.btnControlLaserPulse = new System.Windows.Forms.Button();
            this.lblControlLaserPulse = new System.Windows.Forms.Label();
            this.barControlLaserIntensity = new System.Windows.Forms.TrackBar();
            this.txtControlLaserPower5 = new System.Windows.Forms.TextBox();
            this.txtControlLaserPower4 = new System.Windows.Forms.TextBox();
            this.txtControlLaserPower3 = new System.Windows.Forms.TextBox();
            this.txtControlLaserPower2 = new System.Windows.Forms.TextBox();
            this.txtControlLaserPower1 = new System.Windows.Forms.TextBox();
            this.lblControlLaserIntensities = new System.Windows.Forms.Label();
            this.tabControlMotorPage = new System.Windows.Forms.TabPage();
            this.txtControlMotorPulse = new System.Windows.Forms.TextBox();
            this.imgControlMotorStatus = new System.Windows.Forms.PictureBox();
            this.lblControlMotorSpeedLeft = new System.Windows.Forms.Label();
            this.lblControlMotorSpeedRight = new System.Windows.Forms.Label();
            this.imgControlMotorCalibrateRight = new System.Windows.Forms.PictureBox();
            this.imgControlMotorCalibrateLeft = new System.Windows.Forms.PictureBox();
            this.lblControlMotorHelp = new System.Windows.Forms.Label();
            this.lblControlMotorPulse = new System.Windows.Forms.Label();
            this.btnControlMotorPulse = new System.Windows.Forms.Button();
            this.barControlMotorSpeed = new System.Windows.Forms.TrackBar();
            this.txtControlMotorSpeed5 = new System.Windows.Forms.TextBox();
            this.txtControlMotorSpeed4 = new System.Windows.Forms.TextBox();
            this.txtControlMotorSpeed3 = new System.Windows.Forms.TextBox();
            this.txtControlMotorSpeed2 = new System.Windows.Forms.TextBox();
            this.txtControlMotorSpeed1 = new System.Windows.Forms.TextBox();
            this.lblControlMotorSpeeds = new System.Windows.Forms.Label();
            this.btnControlOK = new System.Windows.Forms.Button();
            this.ControlTempImageList = new System.Windows.Forms.ImageList(this.components);
            this.btnControlCalibrate = new System.Windows.Forms.Button();
            this.btnControlCom = new System.Windows.Forms.Button();
            this.imgControlConnectStatus = new System.Windows.Forms.PictureBox();
            this.tmrControlTemp = new System.Windows.Forms.Timer(this.components);
            this.tmrControlLaser = new System.Windows.Forms.Timer(this.components);
            this.tmrControlMotor = new System.Windows.Forms.Timer(this.components);
            this.tmrSync = new System.Windows.Forms.Timer(this.components);
            this.tmrSyncError = new System.Windows.Forms.Timer(this.components);
            this.tabControl.SuspendLayout();
            this.tabControlTempPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlTempBell)).BeginInit();
            this.tabControlLaserPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlLaserStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlLaserCalibrateRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlLaserCalibrateLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barControlLaserIntensity)).BeginInit();
            this.tabControlMotorPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlMotorStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlMotorCalibrateRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlMotorCalibrateLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barControlMotorSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlConnectStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabControlTempPage);
            this.tabControl.Controls.Add(this.tabControlLaserPage);
            this.tabControl.Controls.Add(this.tabControlMotorPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(755, 631);
            this.tabControl.TabIndex = 22;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabControlTempPage
            // 
            this.tabControlTempPage.Controls.Add(this.imgControlTempBell);
            this.tabControlTempPage.Controls.Add(this.lblControlTempHelp);
            this.tabControlTempPage.Controls.Add(this.txtControlTemp);
            this.tabControlTempPage.Controls.Add(this.txtControlTempAlarm5);
            this.tabControlTempPage.Controls.Add(this.txtControlTempAlarm4);
            this.tabControlTempPage.Controls.Add(this.txtControlTempAlarm3);
            this.tabControlTempPage.Controls.Add(this.txtControlTempAlarm2);
            this.tabControlTempPage.Controls.Add(this.txtControlTempAlarm1);
            this.tabControlTempPage.Controls.Add(this.lblControlTempAlarms);
            this.tabControlTempPage.Controls.Add(this.btnControlTempType);
            this.tabControlTempPage.Controls.Add(this.lblControlTemp);
            this.tabControlTempPage.Location = new System.Drawing.Point(8, 46);
            this.tabControlTempPage.Name = "tabControlTempPage";
            this.tabControlTempPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabControlTempPage.Size = new System.Drawing.Size(739, 577);
            this.tabControlTempPage.TabIndex = 0;
            this.tabControlTempPage.Text = "Temperature";
            this.tabControlTempPage.UseVisualStyleBackColor = true;
            // 
            // imgControlTempBell
            // 
            this.imgControlTempBell.BackColor = System.Drawing.Color.Transparent;
            this.imgControlTempBell.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgControlTempBell.BackgroundImage")));
            this.imgControlTempBell.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgControlTempBell.Location = new System.Drawing.Point(633, 471);
            this.imgControlTempBell.Name = "imgControlTempBell";
            this.imgControlTempBell.Size = new System.Drawing.Size(100, 100);
            this.imgControlTempBell.TabIndex = 3;
            this.imgControlTempBell.TabStop = false;
            this.imgControlTempBell.Visible = false;
            this.imgControlTempBell.Click += new System.EventHandler(this.imgControlTempBell_Click);
            // 
            // lblControlTempHelp
            // 
            this.lblControlTempHelp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlTempHelp.ForeColor = System.Drawing.Color.Red;
            this.lblControlTempHelp.Location = new System.Drawing.Point(43, 429);
            this.lblControlTempHelp.Name = "lblControlTempHelp";
            this.lblControlTempHelp.Size = new System.Drawing.Size(656, 145);
            this.lblControlTempHelp.TabIndex = 10;
            this.lblControlTempHelp.Text = "Enter the actual (measured) temperature in the tempterature text box. Hit Enter t" +
    "o save adjusted value.";
            this.lblControlTempHelp.Visible = false;
            this.lblControlTempHelp.Click += new System.EventHandler(this.lblControlTempHelp_Click);
            // 
            // txtControlTemp
            // 
            this.txtControlTemp.Font = new System.Drawing.Font("Courier New", 70.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtControlTemp.Location = new System.Drawing.Point(44, 144);
            this.txtControlTemp.Name = "txtControlTemp";
            this.txtControlTemp.Size = new System.Drawing.Size(417, 219);
            this.txtControlTemp.TabIndex = 8;
            this.txtControlTemp.Tag = "Allow Negative";
            this.txtControlTemp.Visible = false;
            this.txtControlTemp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtControlTemp_KeyDown);
            this.txtControlTemp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlTemp.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // txtControlTempAlarm5
            // 
            this.txtControlTempAlarm5.Enabled = false;
            this.txtControlTempAlarm5.Location = new System.Drawing.Point(533, 50);
            this.txtControlTempAlarm5.Name = "txtControlTempAlarm5";
            this.txtControlTempAlarm5.Size = new System.Drawing.Size(90, 39);
            this.txtControlTempAlarm5.TabIndex = 6;
            this.txtControlTempAlarm5.Tag = "Allow Negative";
            this.txtControlTempAlarm5.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlTempAlarm5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlTempAlarm5.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // txtControlTempAlarm4
            // 
            this.txtControlTempAlarm4.Enabled = false;
            this.txtControlTempAlarm4.Location = new System.Drawing.Point(437, 50);
            this.txtControlTempAlarm4.Name = "txtControlTempAlarm4";
            this.txtControlTempAlarm4.Size = new System.Drawing.Size(90, 39);
            this.txtControlTempAlarm4.TabIndex = 5;
            this.txtControlTempAlarm4.Tag = "Allow Negative";
            this.txtControlTempAlarm4.TextChanged += new System.EventHandler(this.txtControlTempAlarm4_TextChanged);
            this.txtControlTempAlarm4.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlTempAlarm4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlTempAlarm4.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // txtControlTempAlarm3
            // 
            this.txtControlTempAlarm3.Enabled = false;
            this.txtControlTempAlarm3.Location = new System.Drawing.Point(341, 50);
            this.txtControlTempAlarm3.Name = "txtControlTempAlarm3";
            this.txtControlTempAlarm3.Size = new System.Drawing.Size(90, 39);
            this.txtControlTempAlarm3.TabIndex = 4;
            this.txtControlTempAlarm3.Tag = "Allow Negative";
            this.txtControlTempAlarm3.TextChanged += new System.EventHandler(this.txtControlTempAlarm3_TextChanged);
            this.txtControlTempAlarm3.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlTempAlarm3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlTempAlarm3.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // txtControlTempAlarm2
            // 
            this.txtControlTempAlarm2.Enabled = false;
            this.txtControlTempAlarm2.Location = new System.Drawing.Point(245, 50);
            this.txtControlTempAlarm2.Name = "txtControlTempAlarm2";
            this.txtControlTempAlarm2.Size = new System.Drawing.Size(90, 39);
            this.txtControlTempAlarm2.TabIndex = 3;
            this.txtControlTempAlarm2.Tag = "Allow Negative";
            this.txtControlTempAlarm2.TextChanged += new System.EventHandler(this.txtControlTempAlarm2_TextChanged);
            this.txtControlTempAlarm2.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlTempAlarm2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlTempAlarm2.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // txtControlTempAlarm1
            // 
            this.txtControlTempAlarm1.Location = new System.Drawing.Point(149, 50);
            this.txtControlTempAlarm1.Name = "txtControlTempAlarm1";
            this.txtControlTempAlarm1.Size = new System.Drawing.Size(90, 39);
            this.txtControlTempAlarm1.TabIndex = 2;
            this.txtControlTempAlarm1.Tag = "Allow Negative";
            this.txtControlTempAlarm1.TextChanged += new System.EventHandler(this.txtControlTempAlarm1_TextChanged);
            this.txtControlTempAlarm1.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlTempAlarm1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlTempAlarm1.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // lblControlTempAlarms
            // 
            this.lblControlTempAlarms.AutoSize = true;
            this.lblControlTempAlarms.Location = new System.Drawing.Point(37, 53);
            this.lblControlTempAlarms.Name = "lblControlTempAlarms";
            this.lblControlTempAlarms.Size = new System.Drawing.Size(107, 32);
            this.lblControlTempAlarms.TabIndex = 1;
            this.lblControlTempAlarms.Text = "Alarms:";
            // 
            // btnControlTempType
            // 
            this.btnControlTempType.Font = new System.Drawing.Font("Arial", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnControlTempType.Location = new System.Drawing.Point(462, 141);
            this.btnControlTempType.Margin = new System.Windows.Forms.Padding(0);
            this.btnControlTempType.Name = "btnControlTempType";
            this.btnControlTempType.Size = new System.Drawing.Size(237, 225);
            this.btnControlTempType.TabIndex = 9;
            this.btnControlTempType.Text = "°C";
            this.btnControlTempType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnControlTempType.UseVisualStyleBackColor = true;
            this.btnControlTempType.Click += new System.EventHandler(this.btnControlTempType_Click);
            // 
            // lblControlTemp
            // 
            this.lblControlTemp.BackColor = System.Drawing.Color.Black;
            this.lblControlTemp.Font = new System.Drawing.Font("Courier New", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlTemp.ForeColor = System.Drawing.Color.Lime;
            this.lblControlTemp.Location = new System.Drawing.Point(43, 141);
            this.lblControlTemp.Margin = new System.Windows.Forms.Padding(0);
            this.lblControlTemp.Name = "lblControlTemp";
            this.lblControlTemp.Size = new System.Drawing.Size(453, 225);
            this.lblControlTemp.TabIndex = 7;
            this.lblControlTemp.Text = "000";
            this.lblControlTemp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblControlTemp.Click += new System.EventHandler(this.label1_Click);
            // 
            // tabControlLaserPage
            // 
            this.tabControlLaserPage.Controls.Add(this.txtControlLaserPulse);
            this.tabControlLaserPage.Controls.Add(this.imgControlLaserStatus);
            this.tabControlLaserPage.Controls.Add(this.lblControlLaserIntensity);
            this.tabControlLaserPage.Controls.Add(this.imgControlLaserCalibrateRight);
            this.tabControlLaserPage.Controls.Add(this.imgControlLaserCalibrateLeft);
            this.tabControlLaserPage.Controls.Add(this.lblControlLaserHelp);
            this.tabControlLaserPage.Controls.Add(this.btnControlLaserPulse);
            this.tabControlLaserPage.Controls.Add(this.lblControlLaserPulse);
            this.tabControlLaserPage.Controls.Add(this.barControlLaserIntensity);
            this.tabControlLaserPage.Controls.Add(this.txtControlLaserPower5);
            this.tabControlLaserPage.Controls.Add(this.txtControlLaserPower4);
            this.tabControlLaserPage.Controls.Add(this.txtControlLaserPower3);
            this.tabControlLaserPage.Controls.Add(this.txtControlLaserPower2);
            this.tabControlLaserPage.Controls.Add(this.txtControlLaserPower1);
            this.tabControlLaserPage.Controls.Add(this.lblControlLaserIntensities);
            this.tabControlLaserPage.Location = new System.Drawing.Point(8, 46);
            this.tabControlLaserPage.Name = "tabControlLaserPage";
            this.tabControlLaserPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabControlLaserPage.Size = new System.Drawing.Size(739, 577);
            this.tabControlLaserPage.TabIndex = 1;
            this.tabControlLaserPage.Text = "Laser";
            this.tabControlLaserPage.UseVisualStyleBackColor = true;
            // 
            // txtControlLaserPulse
            // 
            this.txtControlLaserPulse.Location = new System.Drawing.Point(289, 307);
            this.txtControlLaserPulse.Name = "txtControlLaserPulse";
            this.txtControlLaserPulse.Size = new System.Drawing.Size(90, 39);
            this.txtControlLaserPulse.TabIndex = 19;
            this.txtControlLaserPulse.Visible = false;
            this.txtControlLaserPulse.TextChanged += new System.EventHandler(this.txtControlLaserPulse_TextChanged);
            this.txtControlLaserPulse.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlLaserPulse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlLaserPulse.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // imgControlLaserStatus
            // 
            this.imgControlLaserStatus.BackColor = System.Drawing.Color.Transparent;
            this.imgControlLaserStatus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgControlLaserStatus.BackgroundImage")));
            this.imgControlLaserStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgControlLaserStatus.Location = new System.Drawing.Point(4, 172);
            this.imgControlLaserStatus.Name = "imgControlLaserStatus";
            this.imgControlLaserStatus.Size = new System.Drawing.Size(50, 50);
            this.imgControlLaserStatus.TabIndex = 28;
            this.imgControlLaserStatus.TabStop = false;
            this.imgControlLaserStatus.Click += new System.EventHandler(this.imgControlLaserStatus_Click);
            // 
            // lblControlLaserIntensity
            // 
            this.lblControlLaserIntensity.AutoSize = true;
            this.lblControlLaserIntensity.Font = new System.Drawing.Font("Arial", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlLaserIntensity.Location = new System.Drawing.Point(684, 181);
            this.lblControlLaserIntensity.Name = "lblControlLaserIntensity";
            this.lblControlLaserIntensity.Size = new System.Drawing.Size(22, 24);
            this.lblControlLaserIntensity.TabIndex = 27;
            this.lblControlLaserIntensity.Text = "0";
            this.lblControlLaserIntensity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblControlLaserIntensity.Click += new System.EventHandler(this.lblControlLaserIntensityLeft_Click);
            // 
            // imgControlLaserCalibrateRight
            // 
            this.imgControlLaserCalibrateRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgControlLaserCalibrateRight.BackgroundImage")));
            this.imgControlLaserCalibrateRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgControlLaserCalibrateRight.Location = new System.Drawing.Point(705, 177);
            this.imgControlLaserCalibrateRight.Name = "imgControlLaserCalibrateRight";
            this.imgControlLaserCalibrateRight.Size = new System.Drawing.Size(30, 33);
            this.imgControlLaserCalibrateRight.TabIndex = 26;
            this.imgControlLaserCalibrateRight.TabStop = false;
            this.imgControlLaserCalibrateRight.Visible = false;
            this.imgControlLaserCalibrateRight.Click += new System.EventHandler(this.imgControlLaserCalibrateRight_Click);
            // 
            // imgControlLaserCalibrateLeft
            // 
            this.imgControlLaserCalibrateLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgControlLaserCalibrateLeft.BackgroundImage")));
            this.imgControlLaserCalibrateLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgControlLaserCalibrateLeft.Location = new System.Drawing.Point(3, 177);
            this.imgControlLaserCalibrateLeft.Name = "imgControlLaserCalibrateLeft";
            this.imgControlLaserCalibrateLeft.Size = new System.Drawing.Size(30, 33);
            this.imgControlLaserCalibrateLeft.TabIndex = 25;
            this.imgControlLaserCalibrateLeft.TabStop = false;
            this.imgControlLaserCalibrateLeft.Visible = false;
            this.imgControlLaserCalibrateLeft.Click += new System.EventHandler(this.imgControlLaserCalibrateLeft_Click);
            // 
            // lblControlLaserHelp
            // 
            this.lblControlLaserHelp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlLaserHelp.ForeColor = System.Drawing.Color.Red;
            this.lblControlLaserHelp.Location = new System.Drawing.Point(40, 413);
            this.lblControlLaserHelp.Name = "lblControlLaserHelp";
            this.lblControlLaserHelp.Size = new System.Drawing.Size(652, 111);
            this.lblControlLaserHelp.TabIndex = 21;
            this.lblControlLaserHelp.Tag = "To callibrate maximum power, move slider to the right until laser intensity no lo" +
    "nger increases, and click push pin on the right.";
            this.lblControlLaserHelp.Text = "To callibrate minimum power value, move slider to the left until laser is off, an" +
    "d click the push pin on the left of the slider.";
            this.lblControlLaserHelp.Visible = false;
            this.lblControlLaserHelp.Click += new System.EventHandler(this.lblControlLaserHelp_Click);
            // 
            // btnControlLaserPulse
            // 
            this.btnControlLaserPulse.Enabled = false;
            this.btnControlLaserPulse.Location = new System.Drawing.Point(395, 307);
            this.btnControlLaserPulse.Name = "btnControlLaserPulse";
            this.btnControlLaserPulse.Size = new System.Drawing.Size(115, 50);
            this.btnControlLaserPulse.TabIndex = 20;
            this.btnControlLaserPulse.Text = "Start";
            this.btnControlLaserPulse.UseVisualStyleBackColor = true;
            this.btnControlLaserPulse.Visible = false;
            this.btnControlLaserPulse.Click += new System.EventHandler(this.btnControlLaserPulse_Click);
            // 
            // lblControlLaserPulse
            // 
            this.lblControlLaserPulse.AutoSize = true;
            this.lblControlLaserPulse.Location = new System.Drawing.Point(34, 310);
            this.lblControlLaserPulse.Name = "lblControlLaserPulse";
            this.lblControlLaserPulse.Size = new System.Drawing.Size(249, 32);
            this.lblControlLaserPulse.TabIndex = 22;
            this.lblControlLaserPulse.Text = "Pulse Interval (ms):";
            this.lblControlLaserPulse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblControlLaserPulse.Visible = false;
            this.lblControlLaserPulse.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // barControlLaserIntensity
            // 
            this.barControlLaserIntensity.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.barControlLaserIntensity.Location = new System.Drawing.Point(40, 177);
            this.barControlLaserIntensity.Maximum = 400;
            this.barControlLaserIntensity.Name = "barControlLaserIntensity";
            this.barControlLaserIntensity.Size = new System.Drawing.Size(652, 90);
            this.barControlLaserIntensity.TabIndex = 18;
            this.barControlLaserIntensity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.barControlLaserIntensity.Scroll += new System.EventHandler(this.barControlLaserIntensity_Scroll);
            this.barControlLaserIntensity.ValueChanged += new System.EventHandler(this.barControlLaserIntensity_ValueChanged);
            // 
            // txtControlLaserPower5
            // 
            this.txtControlLaserPower5.Enabled = false;
            this.txtControlLaserPower5.Location = new System.Drawing.Point(602, 84);
            this.txtControlLaserPower5.Name = "txtControlLaserPower5";
            this.txtControlLaserPower5.Size = new System.Drawing.Size(90, 39);
            this.txtControlLaserPower5.TabIndex = 17;
            this.txtControlLaserPower5.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlLaserPower5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlLaserPower5.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // txtControlLaserPower4
            // 
            this.txtControlLaserPower4.Enabled = false;
            this.txtControlLaserPower4.Location = new System.Drawing.Point(499, 84);
            this.txtControlLaserPower4.Name = "txtControlLaserPower4";
            this.txtControlLaserPower4.Size = new System.Drawing.Size(90, 39);
            this.txtControlLaserPower4.TabIndex = 16;
            this.txtControlLaserPower4.TextChanged += new System.EventHandler(this.txtControlLaserPower4_TextChanged);
            this.txtControlLaserPower4.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlLaserPower4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlLaserPower4.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // txtControlLaserPower3
            // 
            this.txtControlLaserPower3.Enabled = false;
            this.txtControlLaserPower3.Location = new System.Drawing.Point(395, 84);
            this.txtControlLaserPower3.Name = "txtControlLaserPower3";
            this.txtControlLaserPower3.Size = new System.Drawing.Size(90, 39);
            this.txtControlLaserPower3.TabIndex = 15;
            this.txtControlLaserPower3.TextChanged += new System.EventHandler(this.txtControlLaserPower3_TextChanged);
            this.txtControlLaserPower3.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlLaserPower3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlLaserPower3.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // txtControlLaserPower2
            // 
            this.txtControlLaserPower2.Enabled = false;
            this.txtControlLaserPower2.Location = new System.Drawing.Point(291, 84);
            this.txtControlLaserPower2.Name = "txtControlLaserPower2";
            this.txtControlLaserPower2.Size = new System.Drawing.Size(90, 39);
            this.txtControlLaserPower2.TabIndex = 14;
            this.txtControlLaserPower2.TextChanged += new System.EventHandler(this.txtControlLaserPower2_TextChanged);
            this.txtControlLaserPower2.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlLaserPower2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlLaserPower2.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // txtControlLaserPower1
            // 
            this.txtControlLaserPower1.Location = new System.Drawing.Point(187, 84);
            this.txtControlLaserPower1.Name = "txtControlLaserPower1";
            this.txtControlLaserPower1.Size = new System.Drawing.Size(90, 39);
            this.txtControlLaserPower1.TabIndex = 13;
            this.txtControlLaserPower1.TextChanged += new System.EventHandler(this.txtControlLaserPower1_TextChanged);
            this.txtControlLaserPower1.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlLaserPower1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlLaserPower1.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // lblControlLaserIntensities
            // 
            this.lblControlLaserIntensities.AutoSize = true;
            this.lblControlLaserIntensities.Location = new System.Drawing.Point(34, 87);
            this.lblControlLaserIntensities.Name = "lblControlLaserIntensities";
            this.lblControlLaserIntensities.Size = new System.Drawing.Size(147, 32);
            this.lblControlLaserIntensities.TabIndex = 12;
            this.lblControlLaserIntensities.Text = "Intensities:";
            // 
            // tabControlMotorPage
            // 
            this.tabControlMotorPage.Controls.Add(this.txtControlMotorPulse);
            this.tabControlMotorPage.Controls.Add(this.imgControlMotorStatus);
            this.tabControlMotorPage.Controls.Add(this.lblControlMotorSpeedLeft);
            this.tabControlMotorPage.Controls.Add(this.lblControlMotorSpeedRight);
            this.tabControlMotorPage.Controls.Add(this.imgControlMotorCalibrateRight);
            this.tabControlMotorPage.Controls.Add(this.imgControlMotorCalibrateLeft);
            this.tabControlMotorPage.Controls.Add(this.lblControlMotorHelp);
            this.tabControlMotorPage.Controls.Add(this.lblControlMotorPulse);
            this.tabControlMotorPage.Controls.Add(this.btnControlMotorPulse);
            this.tabControlMotorPage.Controls.Add(this.barControlMotorSpeed);
            this.tabControlMotorPage.Controls.Add(this.txtControlMotorSpeed5);
            this.tabControlMotorPage.Controls.Add(this.txtControlMotorSpeed4);
            this.tabControlMotorPage.Controls.Add(this.txtControlMotorSpeed3);
            this.tabControlMotorPage.Controls.Add(this.txtControlMotorSpeed2);
            this.tabControlMotorPage.Controls.Add(this.txtControlMotorSpeed1);
            this.tabControlMotorPage.Controls.Add(this.lblControlMotorSpeeds);
            this.tabControlMotorPage.Location = new System.Drawing.Point(8, 46);
            this.tabControlMotorPage.Name = "tabControlMotorPage";
            this.tabControlMotorPage.Size = new System.Drawing.Size(739, 577);
            this.tabControlMotorPage.TabIndex = 2;
            this.tabControlMotorPage.Text = "Motor";
            this.tabControlMotorPage.UseVisualStyleBackColor = true;
            this.tabControlMotorPage.Click += new System.EventHandler(this.tabControlMotorPage_Click);
            // 
            // txtControlMotorPulse
            // 
            this.txtControlMotorPulse.Location = new System.Drawing.Point(290, 307);
            this.txtControlMotorPulse.Name = "txtControlMotorPulse";
            this.txtControlMotorPulse.Size = new System.Drawing.Size(90, 39);
            this.txtControlMotorPulse.TabIndex = 31;
            this.txtControlMotorPulse.Visible = false;
            this.txtControlMotorPulse.TextChanged += new System.EventHandler(this.txtControlMotorPulse_TextChanged);
            this.txtControlMotorPulse.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlMotorPulse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlMotorPulse.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // imgControlMotorStatus
            // 
            this.imgControlMotorStatus.BackColor = System.Drawing.Color.Transparent;
            this.imgControlMotorStatus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgControlMotorStatus.BackgroundImage")));
            this.imgControlMotorStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgControlMotorStatus.Location = new System.Drawing.Point(341, 217);
            this.imgControlMotorStatus.Name = "imgControlMotorStatus";
            this.imgControlMotorStatus.Size = new System.Drawing.Size(50, 50);
            this.imgControlMotorStatus.TabIndex = 43;
            this.imgControlMotorStatus.TabStop = false;
            this.imgControlMotorStatus.Click += new System.EventHandler(this.imgControlMotorStatus_Click);
            // 
            // lblControlMotorSpeedLeft
            // 
            this.lblControlMotorSpeedLeft.AutoSize = true;
            this.lblControlMotorSpeedLeft.Font = new System.Drawing.Font("Arial", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlMotorSpeedLeft.Location = new System.Drawing.Point(36, 181);
            this.lblControlMotorSpeedLeft.Name = "lblControlMotorSpeedLeft";
            this.lblControlMotorSpeedLeft.Size = new System.Drawing.Size(22, 24);
            this.lblControlMotorSpeedLeft.TabIndex = 42;
            this.lblControlMotorSpeedLeft.Text = "0";
            this.lblControlMotorSpeedLeft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblControlMotorSpeedLeft.Visible = false;
            // 
            // lblControlMotorSpeedRight
            // 
            this.lblControlMotorSpeedRight.AutoSize = true;
            this.lblControlMotorSpeedRight.Font = new System.Drawing.Font("Arial", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlMotorSpeedRight.Location = new System.Drawing.Point(684, 181);
            this.lblControlMotorSpeedRight.Name = "lblControlMotorSpeedRight";
            this.lblControlMotorSpeedRight.Size = new System.Drawing.Size(22, 24);
            this.lblControlMotorSpeedRight.TabIndex = 41;
            this.lblControlMotorSpeedRight.Text = "0";
            this.lblControlMotorSpeedRight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblControlMotorSpeedRight.Visible = false;
            // 
            // imgControlMotorCalibrateRight
            // 
            this.imgControlMotorCalibrateRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgControlMotorCalibrateRight.BackgroundImage")));
            this.imgControlMotorCalibrateRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgControlMotorCalibrateRight.Location = new System.Drawing.Point(705, 177);
            this.imgControlMotorCalibrateRight.Name = "imgControlMotorCalibrateRight";
            this.imgControlMotorCalibrateRight.Size = new System.Drawing.Size(30, 33);
            this.imgControlMotorCalibrateRight.TabIndex = 40;
            this.imgControlMotorCalibrateRight.TabStop = false;
            this.imgControlMotorCalibrateRight.Visible = false;
            this.imgControlMotorCalibrateRight.Click += new System.EventHandler(this.imgControlMotorCalibrateRight_Click);
            // 
            // imgControlMotorCalibrateLeft
            // 
            this.imgControlMotorCalibrateLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgControlMotorCalibrateLeft.BackgroundImage")));
            this.imgControlMotorCalibrateLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgControlMotorCalibrateLeft.Location = new System.Drawing.Point(3, 177);
            this.imgControlMotorCalibrateLeft.Name = "imgControlMotorCalibrateLeft";
            this.imgControlMotorCalibrateLeft.Size = new System.Drawing.Size(30, 33);
            this.imgControlMotorCalibrateLeft.TabIndex = 39;
            this.imgControlMotorCalibrateLeft.TabStop = false;
            this.imgControlMotorCalibrateLeft.Visible = false;
            this.imgControlMotorCalibrateLeft.Click += new System.EventHandler(this.imgControlMotorCalibrateLeft_Click);
            // 
            // lblControlMotorHelp
            // 
            this.lblControlMotorHelp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlMotorHelp.ForeColor = System.Drawing.Color.Red;
            this.lblControlMotorHelp.Location = new System.Drawing.Point(40, 413);
            this.lblControlMotorHelp.Name = "lblControlMotorHelp";
            this.lblControlMotorHelp.Size = new System.Drawing.Size(652, 111);
            this.lblControlMotorHelp.TabIndex = 33;
            this.lblControlMotorHelp.Tag = "To callibrate maximum speed for clockwise motor movement, move slider to the righ" +
    "t until speed no longer increases, and click push pin on the right.";
            this.lblControlMotorHelp.Text = "To callibrate maximum speed for counter-clockwise motor movement, move slider to " +
    "the left until speed no longer increases, and click push pin on the left.";
            this.lblControlMotorHelp.Visible = false;
            // 
            // lblControlMotorPulse
            // 
            this.lblControlMotorPulse.AutoSize = true;
            this.lblControlMotorPulse.Location = new System.Drawing.Point(34, 310);
            this.lblControlMotorPulse.Name = "lblControlMotorPulse";
            this.lblControlMotorPulse.Size = new System.Drawing.Size(249, 32);
            this.lblControlMotorPulse.TabIndex = 30;
            this.lblControlMotorPulse.Text = "Pulse Interval (ms):";
            this.lblControlMotorPulse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblControlMotorPulse.Visible = false;
            // 
            // btnControlMotorPulse
            // 
            this.btnControlMotorPulse.Enabled = false;
            this.btnControlMotorPulse.Location = new System.Drawing.Point(397, 305);
            this.btnControlMotorPulse.Name = "btnControlMotorPulse";
            this.btnControlMotorPulse.Size = new System.Drawing.Size(115, 53);
            this.btnControlMotorPulse.TabIndex = 32;
            this.btnControlMotorPulse.Text = "Start";
            this.btnControlMotorPulse.UseVisualStyleBackColor = true;
            this.btnControlMotorPulse.Visible = false;
            this.btnControlMotorPulse.Click += new System.EventHandler(this.btnControlMotorPulse_Click);
            // 
            // barControlMotorSpeed
            // 
            this.barControlMotorSpeed.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.barControlMotorSpeed.Location = new System.Drawing.Point(40, 177);
            this.barControlMotorSpeed.Maximum = 100;
            this.barControlMotorSpeed.Minimum = -100;
            this.barControlMotorSpeed.Name = "barControlMotorSpeed";
            this.barControlMotorSpeed.Size = new System.Drawing.Size(652, 90);
            this.barControlMotorSpeed.TabIndex = 29;
            this.barControlMotorSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.barControlMotorSpeed.ValueChanged += new System.EventHandler(this.barControlMotorSpeed_ValueChanged);
            // 
            // txtControlMotorSpeed5
            // 
            this.txtControlMotorSpeed5.Enabled = false;
            this.txtControlMotorSpeed5.Location = new System.Drawing.Point(602, 84);
            this.txtControlMotorSpeed5.Name = "txtControlMotorSpeed5";
            this.txtControlMotorSpeed5.Size = new System.Drawing.Size(90, 39);
            this.txtControlMotorSpeed5.TabIndex = 28;
            this.txtControlMotorSpeed5.Tag = "Allow Negative";
            this.txtControlMotorSpeed5.TextChanged += new System.EventHandler(this.txtControlMotorSpeed5_TextChanged);
            this.txtControlMotorSpeed5.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlMotorSpeed5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlMotorSpeed5.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // txtControlMotorSpeed4
            // 
            this.txtControlMotorSpeed4.Enabled = false;
            this.txtControlMotorSpeed4.Location = new System.Drawing.Point(499, 84);
            this.txtControlMotorSpeed4.Name = "txtControlMotorSpeed4";
            this.txtControlMotorSpeed4.Size = new System.Drawing.Size(90, 39);
            this.txtControlMotorSpeed4.TabIndex = 27;
            this.txtControlMotorSpeed4.Tag = "Allow Negative";
            this.txtControlMotorSpeed4.TextChanged += new System.EventHandler(this.txtControlMotorSpeed4_TextChanged);
            this.txtControlMotorSpeed4.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlMotorSpeed4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlMotorSpeed4.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // txtControlMotorSpeed3
            // 
            this.txtControlMotorSpeed3.Enabled = false;
            this.txtControlMotorSpeed3.Location = new System.Drawing.Point(395, 84);
            this.txtControlMotorSpeed3.Name = "txtControlMotorSpeed3";
            this.txtControlMotorSpeed3.Size = new System.Drawing.Size(90, 39);
            this.txtControlMotorSpeed3.TabIndex = 26;
            this.txtControlMotorSpeed3.Tag = "Allow Negative";
            this.txtControlMotorSpeed3.TextChanged += new System.EventHandler(this.txtControlMotorSpeed3_TextChanged);
            this.txtControlMotorSpeed3.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlMotorSpeed3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlMotorSpeed3.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // txtControlMotorSpeed2
            // 
            this.txtControlMotorSpeed2.Enabled = false;
            this.txtControlMotorSpeed2.Location = new System.Drawing.Point(291, 84);
            this.txtControlMotorSpeed2.Name = "txtControlMotorSpeed2";
            this.txtControlMotorSpeed2.Size = new System.Drawing.Size(90, 39);
            this.txtControlMotorSpeed2.TabIndex = 25;
            this.txtControlMotorSpeed2.Tag = "Allow Negative";
            this.txtControlMotorSpeed2.TextChanged += new System.EventHandler(this.txtControlMotorSpeed2_TextChanged);
            this.txtControlMotorSpeed2.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlMotorSpeed2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlMotorSpeed2.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // txtControlMotorSpeed1
            // 
            this.txtControlMotorSpeed1.Location = new System.Drawing.Point(187, 84);
            this.txtControlMotorSpeed1.Name = "txtControlMotorSpeed1";
            this.txtControlMotorSpeed1.Size = new System.Drawing.Size(90, 39);
            this.txtControlMotorSpeed1.TabIndex = 24;
            this.txtControlMotorSpeed1.Tag = "Allow Negative";
            this.txtControlMotorSpeed1.TextChanged += new System.EventHandler(this.txtControlMotorSpeed1_TextChanged);
            this.txtControlMotorSpeed1.GotFocus += new System.EventHandler(this.txtControlNumeric_GotFocus);
            this.txtControlMotorSpeed1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtControlNumeric_KeyPress);
            this.txtControlMotorSpeed1.LostFocus += new System.EventHandler(this.txtControlNumeric_LostFocus);
            // 
            // lblControlMotorSpeeds
            // 
            this.lblControlMotorSpeeds.AutoSize = true;
            this.lblControlMotorSpeeds.Location = new System.Drawing.Point(34, 87);
            this.lblControlMotorSpeeds.Name = "lblControlMotorSpeeds";
            this.lblControlMotorSpeeds.Size = new System.Drawing.Size(115, 32);
            this.lblControlMotorSpeeds.TabIndex = 23;
            this.lblControlMotorSpeeds.Text = "Speeds:";
            // 
            // btnControlOK
            // 
            this.btnControlOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnControlOK.Location = new System.Drawing.Point(637, 648);
            this.btnControlOK.Name = "btnControlOK";
            this.btnControlOK.Size = new System.Drawing.Size(107, 53);
            this.btnControlOK.TabIndex = 36;
            this.btnControlOK.Text = "OK";
            this.btnControlOK.UseVisualStyleBackColor = true;
            this.btnControlOK.Click += new System.EventHandler(this.btnControlOK_Click);
            // 
            // ControlTempImageList
            // 
            this.ControlTempImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ControlTempImageList.ImageStream")));
            this.ControlTempImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ControlTempImageList.Images.SetKeyName(0, "Bell.Gray.jpg");
            this.ControlTempImageList.Images.SetKeyName(1, "Bell.Black.Ns.jpg");
            this.ControlTempImageList.Images.SetKeyName(2, "Bell.Black.jpg");
            this.ControlTempImageList.Images.SetKeyName(3, "Bell.Red.Ns.jpg");
            this.ControlTempImageList.Images.SetKeyName(4, "Bell.Red.jpg");
            this.ControlTempImageList.Images.SetKeyName(5, "connected.jpg");
            this.ControlTempImageList.Images.SetKeyName(6, "disconnected.jpg");
            this.ControlTempImageList.Images.SetKeyName(7, "power.off.small.jpg");
            this.ControlTempImageList.Images.SetKeyName(8, "power.on.small.jpg");
            // 
            // btnControlCalibrate
            // 
            this.btnControlCalibrate.Location = new System.Drawing.Point(471, 648);
            this.btnControlCalibrate.Name = "btnControlCalibrate";
            this.btnControlCalibrate.Size = new System.Drawing.Size(160, 53);
            this.btnControlCalibrate.TabIndex = 35;
            this.btnControlCalibrate.Text = "Calibrate";
            this.btnControlCalibrate.UseVisualStyleBackColor = true;
            this.btnControlCalibrate.Click += new System.EventHandler(this.btnControlCalibrate_Click);
            // 
            // btnControlCom
            // 
            this.btnControlCom.Location = new System.Drawing.Point(256, 648);
            this.btnControlCom.Name = "btnControlCom";
            this.btnControlCom.Size = new System.Drawing.Size(209, 53);
            this.btnControlCom.TabIndex = 34;
            this.btnControlCom.Text = "COM Settings";
            this.btnControlCom.UseVisualStyleBackColor = true;
            this.btnControlCom.Click += new System.EventHandler(this.btnControlConnect_Click);
            // 
            // imgControlConnectStatus
            // 
            this.imgControlConnectStatus.BackColor = System.Drawing.Color.Transparent;
            this.imgControlConnectStatus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgControlConnectStatus.BackgroundImage")));
            this.imgControlConnectStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgControlConnectStatus.ErrorImage = null;
            this.imgControlConnectStatus.InitialImage = null;
            this.imgControlConnectStatus.Location = new System.Drawing.Point(12, 648);
            this.imgControlConnectStatus.Name = "imgControlConnectStatus";
            this.imgControlConnectStatus.Size = new System.Drawing.Size(100, 50);
            this.imgControlConnectStatus.TabIndex = 5;
            this.imgControlConnectStatus.TabStop = false;
            this.imgControlConnectStatus.Tag = "Disconnected";
            this.imgControlConnectStatus.Click += new System.EventHandler(this.imgControlConnectStatus_Click);
            // 
            // tmrControlTemp
            // 
            this.tmrControlTemp.Interval = 500;
            this.tmrControlTemp.Tick += new System.EventHandler(this.tmrControlTemp_Tick);
            // 
            // tmrControlLaser
            // 
            this.tmrControlLaser.Interval = 1000;
            this.tmrControlLaser.Tick += new System.EventHandler(this.tmrControlLaser_Tick);
            // 
            // tmrControlMotor
            // 
            this.tmrControlMotor.Tick += new System.EventHandler(this.tmrControlMotor_Tick);
            // 
            // tmrSync
            // 
            this.tmrSync.Interval = 500;
            this.tmrSync.Tick += new System.EventHandler(this.tmrSync_Tick);
            // 
            // tmrSyncError
            // 
            this.tmrSyncError.Interval = 250;
            this.tmrSyncError.Tick += new System.EventHandler(this.tmrSyncError_Tick);
            // 
            // frmControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(755, 716);
            this.Controls.Add(this.imgControlConnectStatus);
            this.Controls.Add(this.btnControlCom);
            this.Controls.Add(this.btnControlCalibrate);
            this.Controls.Add(this.btnControlOK);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmControl";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VDST - Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmControl_FormClosing);
            this.Load += new System.EventHandler(this.frmControl_Load);
            this.tabControl.ResumeLayout(false);
            this.tabControlTempPage.ResumeLayout(false);
            this.tabControlTempPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlTempBell)).EndInit();
            this.tabControlLaserPage.ResumeLayout(false);
            this.tabControlLaserPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlLaserStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlLaserCalibrateRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlLaserCalibrateLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barControlLaserIntensity)).EndInit();
            this.tabControlMotorPage.ResumeLayout(false);
            this.tabControlMotorPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlMotorStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlMotorCalibrateRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlMotorCalibrateLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barControlMotorSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgControlConnectStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabControlTempPage;
        private System.Windows.Forms.TabPage tabControlLaserPage;
        private System.Windows.Forms.Button btnControlOK;
        private System.Windows.Forms.TabPage tabControlMotorPage;
        private System.Windows.Forms.Label lblControlTemp;
        private System.Windows.Forms.Label lblControlTempAlarms;
        private System.Windows.Forms.PictureBox imgControlTempBell;
        public System.Windows.Forms.Button btnControlTempType;
        public System.Windows.Forms.TextBox txtControlTempAlarm5;
        public System.Windows.Forms.TextBox txtControlTempAlarm4;
        public System.Windows.Forms.TextBox txtControlTempAlarm3;
        public System.Windows.Forms.TextBox txtControlTempAlarm2;
        public System.Windows.Forms.TextBox txtControlTempAlarm1;
        private System.Windows.Forms.ImageList ControlTempImageList;
        private System.Windows.Forms.Button btnControlCalibrate;
        private System.Windows.Forms.Button btnControlCom;
        private System.Windows.Forms.PictureBox imgControlConnectStatus;
        public System.Windows.Forms.TextBox txtControlLaserPower5;
        public System.Windows.Forms.TextBox txtControlLaserPower4;
        public System.Windows.Forms.TextBox txtControlLaserPower3;
        public System.Windows.Forms.TextBox txtControlLaserPower2;
        public System.Windows.Forms.TextBox txtControlLaserPower1;
        private System.Windows.Forms.Label lblControlLaserIntensities;
        private System.Windows.Forms.TrackBar barControlLaserIntensity;
        private System.Windows.Forms.Button btnControlLaserPulse;
        private System.Windows.Forms.Label lblControlLaserPulse;
        public System.Windows.Forms.TextBox txtControlLaserPulse;
        private System.Windows.Forms.TextBox txtControlTemp;
        private System.Windows.Forms.Label lblControlTempHelp;
        public System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Label lblControlLaserHelp;
        private System.Windows.Forms.PictureBox imgControlLaserCalibrateRight;
        private System.Windows.Forms.PictureBox imgControlLaserCalibrateLeft;
        private System.Windows.Forms.Label lblControlLaserIntensity;
        private System.Windows.Forms.Label lblControlMotorSpeedLeft;
        private System.Windows.Forms.Label lblControlMotorSpeedRight;
        private System.Windows.Forms.PictureBox imgControlMotorCalibrateRight;
        private System.Windows.Forms.PictureBox imgControlMotorCalibrateLeft;
        private System.Windows.Forms.Label lblControlMotorHelp;
        public System.Windows.Forms.TextBox txtControlMotorPulse;
        private System.Windows.Forms.Label lblControlMotorPulse;
        private System.Windows.Forms.Button btnControlMotorPulse;
        private System.Windows.Forms.TrackBar barControlMotorSpeed;
        public System.Windows.Forms.TextBox txtControlMotorSpeed5;
        public System.Windows.Forms.TextBox txtControlMotorSpeed4;
        public System.Windows.Forms.TextBox txtControlMotorSpeed3;
        public System.Windows.Forms.TextBox txtControlMotorSpeed2;
        public System.Windows.Forms.TextBox txtControlMotorSpeed1;
        private System.Windows.Forms.Label lblControlMotorSpeeds;
        private System.Windows.Forms.PictureBox imgControlLaserStatus;
        private System.Windows.Forms.PictureBox imgControlMotorStatus;
        private System.Windows.Forms.Timer tmrControlTemp;
        private System.Windows.Forms.Timer tmrControlLaser;
        private System.Windows.Forms.Timer tmrControlMotor;
        private System.Windows.Forms.Timer tmrSync;
        private System.Windows.Forms.Timer tmrSyncError;
    }
}