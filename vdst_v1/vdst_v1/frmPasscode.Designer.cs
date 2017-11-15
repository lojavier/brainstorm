namespace vdst_v1
{
    partial class frmPasscode
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
            this.boxPasscode = new System.Windows.Forms.GroupBox();
            this.txtPasscodeOld = new System.Windows.Forms.TextBox();
            this.txtPasscodeNew = new System.Windows.Forms.TextBox();
            this.txtPasscodeRetype = new System.Windows.Forms.TextBox();
            this.lblPasscodeRetype = new System.Windows.Forms.Label();
            this.lblPasscodeNew = new System.Windows.Forms.Label();
            this.lblPasscodeOld = new System.Windows.Forms.Label();
            this.btnPasscodeCancel = new System.Windows.Forms.Button();
            this.btnPasscodeOK = new System.Windows.Forms.Button();
            this.statusStripPasscode = new System.Windows.Forms.StatusStrip();
            this.lblPasscodeStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.boxPasscode.SuspendLayout();
            this.statusStripPasscode.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxPasscode
            // 
            this.boxPasscode.Controls.Add(this.txtPasscodeOld);
            this.boxPasscode.Controls.Add(this.txtPasscodeNew);
            this.boxPasscode.Controls.Add(this.txtPasscodeRetype);
            this.boxPasscode.Controls.Add(this.lblPasscodeRetype);
            this.boxPasscode.Controls.Add(this.lblPasscodeNew);
            this.boxPasscode.Controls.Add(this.lblPasscodeOld);
            this.boxPasscode.Location = new System.Drawing.Point(27, 44);
            this.boxPasscode.Margin = new System.Windows.Forms.Padding(4);
            this.boxPasscode.Name = "boxPasscode";
            this.boxPasscode.Padding = new System.Windows.Forms.Padding(4);
            this.boxPasscode.Size = new System.Drawing.Size(489, 298);
            this.boxPasscode.TabIndex = 0;
            this.boxPasscode.TabStop = false;
            this.boxPasscode.Text = "Change Passcode";
            // 
            // txtPasscodeOld
            // 
            this.txtPasscodeOld.Location = new System.Drawing.Point(285, 99);
            this.txtPasscodeOld.Margin = new System.Windows.Forms.Padding(4);
            this.txtPasscodeOld.MaxLength = 4;
            this.txtPasscodeOld.Name = "txtPasscodeOld";
            this.txtPasscodeOld.Size = new System.Drawing.Size(132, 39);
            this.txtPasscodeOld.TabIndex = 2;
            this.txtPasscodeOld.TextChanged += new System.EventHandler(this.txtPasscode_TextChanged);
            this.txtPasscodeOld.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPasscode_KeyPress);
            // 
            // txtPasscodeNew
            // 
            this.txtPasscodeNew.Location = new System.Drawing.Point(285, 159);
            this.txtPasscodeNew.Margin = new System.Windows.Forms.Padding(4);
            this.txtPasscodeNew.MaxLength = 4;
            this.txtPasscodeNew.Name = "txtPasscodeNew";
            this.txtPasscodeNew.Size = new System.Drawing.Size(132, 39);
            this.txtPasscodeNew.TabIndex = 4;
            this.txtPasscodeNew.TextChanged += new System.EventHandler(this.txtPasscode_TextChanged);
            this.txtPasscodeNew.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPasscode_KeyPress);
            // 
            // txtPasscodeRetype
            // 
            this.txtPasscodeRetype.Location = new System.Drawing.Point(285, 219);
            this.txtPasscodeRetype.Margin = new System.Windows.Forms.Padding(4);
            this.txtPasscodeRetype.MaxLength = 4;
            this.txtPasscodeRetype.Name = "txtPasscodeRetype";
            this.txtPasscodeRetype.Size = new System.Drawing.Size(132, 39);
            this.txtPasscodeRetype.TabIndex = 6;
            this.txtPasscodeRetype.TextChanged += new System.EventHandler(this.txtPasscode_TextChanged);
            this.txtPasscodeRetype.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPasscode_KeyPress);
            // 
            // lblPasscodeRetype
            // 
            this.lblPasscodeRetype.AutoSize = true;
            this.lblPasscodeRetype.Location = new System.Drawing.Point(27, 223);
            this.lblPasscodeRetype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPasscodeRetype.Name = "lblPasscodeRetype";
            this.lblPasscodeRetype.Size = new System.Drawing.Size(237, 32);
            this.lblPasscodeRetype.TabIndex = 5;
            this.lblPasscodeRetype.Text = "Retype Passcode:";
            // 
            // lblPasscodeNew
            // 
            this.lblPasscodeNew.AutoSize = true;
            this.lblPasscodeNew.Location = new System.Drawing.Point(27, 163);
            this.lblPasscodeNew.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPasscodeNew.Name = "lblPasscodeNew";
            this.lblPasscodeNew.Size = new System.Drawing.Size(203, 32);
            this.lblPasscodeNew.TabIndex = 3;
            this.lblPasscodeNew.Text = "New Passcode:";
            // 
            // lblPasscodeOld
            // 
            this.lblPasscodeOld.AutoSize = true;
            this.lblPasscodeOld.Location = new System.Drawing.Point(27, 102);
            this.lblPasscodeOld.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPasscodeOld.Name = "lblPasscodeOld";
            this.lblPasscodeOld.Size = new System.Drawing.Size(192, 32);
            this.lblPasscodeOld.TabIndex = 1;
            this.lblPasscodeOld.Text = "Old Passcode:";
            // 
            // btnPasscodeCancel
            // 
            this.btnPasscodeCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPasscodeCancel.Location = new System.Drawing.Point(386, 378);
            this.btnPasscodeCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnPasscodeCancel.Name = "btnPasscodeCancel";
            this.btnPasscodeCancel.Size = new System.Drawing.Size(131, 59);
            this.btnPasscodeCancel.TabIndex = 8;
            this.btnPasscodeCancel.Text = "Cancel";
            this.btnPasscodeCancel.UseVisualStyleBackColor = true;
            // 
            // btnPasscodeOK
            // 
            this.btnPasscodeOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPasscodeOK.Enabled = false;
            this.btnPasscodeOK.Location = new System.Drawing.Point(247, 378);
            this.btnPasscodeOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnPasscodeOK.Name = "btnPasscodeOK";
            this.btnPasscodeOK.Size = new System.Drawing.Size(131, 59);
            this.btnPasscodeOK.TabIndex = 7;
            this.btnPasscodeOK.Text = "OK";
            this.btnPasscodeOK.UseVisualStyleBackColor = true;
            // 
            // statusStripPasscode
            // 
            this.statusStripPasscode.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStripPasscode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPasscodeStatus});
            this.statusStripPasscode.Location = new System.Drawing.Point(0, 479);
            this.statusStripPasscode.Name = "statusStripPasscode";
            this.statusStripPasscode.Size = new System.Drawing.Size(545, 22);
            this.statusStripPasscode.TabIndex = 9;
            this.statusStripPasscode.Text = "statusStrip1";
            // 
            // lblPasscodeStatus
            // 
            this.lblPasscodeStatus.ForeColor = System.Drawing.Color.Red;
            this.lblPasscodeStatus.Name = "lblPasscodeStatus";
            this.lblPasscodeStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // frmPasscode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(545, 501);
            this.Controls.Add(this.statusStripPasscode);
            this.Controls.Add(this.btnPasscodeOK);
            this.Controls.Add(this.btnPasscodeCancel);
            this.Controls.Add(this.boxPasscode);
            this.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPasscode";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VDST - Change Passcode";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin_FormClosing);
            this.boxPasscode.ResumeLayout(false);
            this.boxPasscode.PerformLayout();
            this.statusStripPasscode.ResumeLayout(false);
            this.statusStripPasscode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox boxPasscode;
        private System.Windows.Forms.TextBox txtPasscodeRetype;
        private System.Windows.Forms.Label lblPasscodeRetype;
        private System.Windows.Forms.Label lblPasscodeNew;
        private System.Windows.Forms.Button btnPasscodeCancel;
        private System.Windows.Forms.Button btnPasscodeOK;
        public System.Windows.Forms.TextBox txtPasscodeOld;
        public System.Windows.Forms.Label lblPasscodeOld;
        private System.Windows.Forms.StatusStrip statusStripPasscode;
        private System.Windows.Forms.ToolStripStatusLabel lblPasscodeStatus;
        public System.Windows.Forms.TextBox txtPasscodeNew;
    }
}