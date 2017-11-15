namespace vdst_v1
{
    partial class frmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.imgAbout = new System.Windows.Forms.PictureBox();
            this.lblAboutTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAboutName = new System.Windows.Forms.Label();
            this.lblAboutVersion = new System.Windows.Forms.Label();
            this.lblAboutCopyright = new System.Windows.Forms.Label();
            this.lblAboutLegal = new System.Windows.Forms.Label();
            this.txtAboutMembers = new System.Windows.Forms.TextBox();
            this.lblAboutMembers = new System.Windows.Forms.Label();
            this.btnAboutOK = new System.Windows.Forms.Button();
            this.btnAboutCopyInfo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgAbout)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgAbout
            // 
            this.imgAbout.BackColor = System.Drawing.SystemColors.ControlDark;
            this.imgAbout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgAbout.BackgroundImage")));
            this.imgAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgAbout.Location = new System.Drawing.Point(36, 49);
            this.imgAbout.Margin = new System.Windows.Forms.Padding(4);
            this.imgAbout.Name = "imgAbout";
            this.imgAbout.Size = new System.Drawing.Size(108, 93);
            this.imgAbout.TabIndex = 0;
            this.imgAbout.TabStop = false;
            // 
            // lblAboutTitle
            // 
            this.lblAboutTitle.AutoSize = true;
            this.lblAboutTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAboutTitle.Location = new System.Drawing.Point(162, 49);
            this.lblAboutTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAboutTitle.Name = "lblAboutTitle";
            this.lblAboutTitle.Size = new System.Drawing.Size(225, 73);
            this.lblAboutTitle.TabIndex = 0;
            this.lblAboutTitle.Text = "VDST ";
            this.lblAboutTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Controls.Add(this.imgAbout);
            this.groupBox1.Controls.Add(this.lblAboutTitle);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(1034, 197);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // lblAboutName
            // 
            this.lblAboutName.AutoSize = true;
            this.lblAboutName.Location = new System.Drawing.Point(27, 218);
            this.lblAboutName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAboutName.Name = "lblAboutName";
            this.lblAboutName.Size = new System.Drawing.Size(309, 32);
            this.lblAboutName.TabIndex = 0;
            this.lblAboutName.Text = "Visual DoSomethingTool";
            // 
            // lblAboutVersion
            // 
            this.lblAboutVersion.AutoSize = true;
            this.lblAboutVersion.Location = new System.Drawing.Point(27, 259);
            this.lblAboutVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAboutVersion.Name = "lblAboutVersion";
            this.lblAboutVersion.Size = new System.Drawing.Size(174, 32);
            this.lblAboutVersion.TabIndex = 0;
            this.lblAboutVersion.Text = "Version 1.0.0";
            this.lblAboutVersion.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblAboutCopyright
            // 
            this.lblAboutCopyright.AutoSize = true;
            this.lblAboutCopyright.Location = new System.Drawing.Point(27, 301);
            this.lblAboutCopyright.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAboutCopyright.Name = "lblAboutCopyright";
            this.lblAboutCopyright.Size = new System.Drawing.Size(312, 32);
            this.lblAboutCopyright.TabIndex = 0;
            this.lblAboutCopyright.Text = "(c) 2017 BrainStorn, Inc.";
            // 
            // lblAboutLegal
            // 
            this.lblAboutLegal.AutoSize = true;
            this.lblAboutLegal.Location = new System.Drawing.Point(27, 348);
            this.lblAboutLegal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAboutLegal.Name = "lblAboutLegal";
            this.lblAboutLegal.Size = new System.Drawing.Size(241, 32);
            this.lblAboutLegal.TabIndex = 0;
            this.lblAboutLegal.Text = "All Right Reserved";
            // 
            // txtAboutMembers
            // 
            this.txtAboutMembers.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtAboutMembers.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAboutMembers.Location = new System.Drawing.Point(33, 480);
            this.txtAboutMembers.Margin = new System.Windows.Forms.Padding(4);
            this.txtAboutMembers.Multiline = true;
            this.txtAboutMembers.Name = "txtAboutMembers";
            this.txtAboutMembers.ReadOnly = true;
            this.txtAboutMembers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAboutMembers.Size = new System.Drawing.Size(716, 210);
            this.txtAboutMembers.TabIndex = 2;
            this.txtAboutMembers.Text = "Person 1\r\nPerson 2\r\nPerson 3\r\nPerson 4\r\nPerson 5\r\nPerson 6\r\nPerson 7\r\nPerson 8\r\nP" +
    "erson 9\r\nPerson 10\r\n";
            // 
            // lblAboutMembers
            // 
            this.lblAboutMembers.AutoSize = true;
            this.lblAboutMembers.Location = new System.Drawing.Point(27, 435);
            this.lblAboutMembers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAboutMembers.Name = "lblAboutMembers";
            this.lblAboutMembers.Size = new System.Drawing.Size(219, 32);
            this.lblAboutMembers.TabIndex = 8;
            this.lblAboutMembers.Text = "Group Members:";
            // 
            // btnAboutOK
            // 
            this.btnAboutOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAboutOK.Location = new System.Drawing.Point(773, 218);
            this.btnAboutOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnAboutOK.Name = "btnAboutOK";
            this.btnAboutOK.Size = new System.Drawing.Size(228, 60);
            this.btnAboutOK.TabIndex = 0;
            this.btnAboutOK.Text = "OK";
            this.btnAboutOK.UseVisualStyleBackColor = true;
            this.btnAboutOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAboutCopyInfo
            // 
            this.btnAboutCopyInfo.Location = new System.Drawing.Point(773, 480);
            this.btnAboutCopyInfo.Margin = new System.Windows.Forms.Padding(4);
            this.btnAboutCopyInfo.Name = "btnAboutCopyInfo";
            this.btnAboutCopyInfo.Size = new System.Drawing.Size(228, 60);
            this.btnAboutCopyInfo.TabIndex = 2;
            this.btnAboutCopyInfo.Text = "Copy Info";
            this.btnAboutCopyInfo.UseVisualStyleBackColor = true;
            this.btnAboutCopyInfo.Click += new System.EventHandler(this.btnAboutCopyInfo_Click);
            // 
            // frmAbout
            // 
            this.AcceptButton = this.btnAboutOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.CancelButton = this.btnAboutOK;
            this.ClientSize = new System.Drawing.Size(1035, 719);
            this.Controls.Add(this.btnAboutCopyInfo);
            this.Controls.Add(this.btnAboutOK);
            this.Controls.Add(this.lblAboutMembers);
            this.Controls.Add(this.txtAboutMembers);
            this.Controls.Add(this.lblAboutLegal);
            this.Controls.Add(this.lblAboutCopyright);
            this.Controls.Add(this.lblAboutVersion);
            this.Controls.Add(this.lblAboutName);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VDST - About";
            ((System.ComponentModel.ISupportInitialize)(this.imgAbout)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgAbout;
        private System.Windows.Forms.Label lblAboutTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblAboutName;
        private System.Windows.Forms.Label lblAboutVersion;
        private System.Windows.Forms.Label lblAboutCopyright;
        private System.Windows.Forms.Label lblAboutLegal;
        private System.Windows.Forms.TextBox txtAboutMembers;
        private System.Windows.Forms.Label lblAboutMembers;
        private System.Windows.Forms.Button btnAboutOK;
        private System.Windows.Forms.Button btnAboutCopyInfo;
    }
}