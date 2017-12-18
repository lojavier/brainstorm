namespace vdst_v1
{
    partial class frmLog
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
            this.btnLogOK = new System.Windows.Forms.Button();
            this.btnLogCopy = new System.Windows.Forms.Button();
            this.btnLogExport = new System.Windows.Forms.Button();
            this.lblLogFilter = new System.Windows.Forms.Label();
            this.txtLogFilter = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnLogOK
            // 
            this.btnLogOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnLogOK.Location = new System.Drawing.Point(849, 731);
            this.btnLogOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogOK.Name = "btnLogOK";
            this.btnLogOK.Size = new System.Drawing.Size(81, 44);
            this.btnLogOK.TabIndex = 5;
            this.btnLogOK.Text = "OK";
            this.btnLogOK.UseVisualStyleBackColor = true;
            this.btnLogOK.Click += new System.EventHandler(this.btnLogOK_Click);
            // 
            // btnLogCopy
            // 
            this.btnLogCopy.Location = new System.Drawing.Point(183, 731);
            this.btnLogCopy.Name = "btnLogCopy";
            this.btnLogCopy.Size = new System.Drawing.Size(279, 44);
            this.btnLogCopy.TabIndex = 4;
            this.btnLogCopy.Text = "Copy to Clipboard";
            this.btnLogCopy.UseVisualStyleBackColor = true;
            this.btnLogCopy.Click += new System.EventHandler(this.btnLogCopy_Click);
            // 
            // btnLogExport
            // 
            this.btnLogExport.Location = new System.Drawing.Point(28, 731);
            this.btnLogExport.Name = "btnLogExport";
            this.btnLogExport.Size = new System.Drawing.Size(149, 44);
            this.btnLogExport.TabIndex = 3;
            this.btnLogExport.Text = "Export";
            this.btnLogExport.UseVisualStyleBackColor = true;
            this.btnLogExport.Click += new System.EventHandler(this.btnLogExport_Click);
            // 
            // lblLogFilter
            // 
            this.lblLogFilter.AutoSize = true;
            this.lblLogFilter.Location = new System.Drawing.Point(22, 47);
            this.lblLogFilter.Name = "lblLogFilter";
            this.lblLogFilter.Size = new System.Drawing.Size(84, 32);
            this.lblLogFilter.TabIndex = 0;
            this.lblLogFilter.Text = "Filter:";
            this.lblLogFilter.Click += new System.EventHandler(this.lblLogFilter_Click);
            // 
            // txtLogFilter
            // 
            this.txtLogFilter.Location = new System.Drawing.Point(112, 47);
            this.txtLogFilter.Name = "txtLogFilter";
            this.txtLogFilter.Size = new System.Drawing.Size(818, 39);
            this.txtLogFilter.TabIndex = 1;
            this.txtLogFilter.TextChanged += new System.EventHandler(this.txtLogFilter_TextChanged);
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtLog.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(28, 103);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(901, 603);
            this.txtLog.TabIndex = 2;
            this.txtLog.WordWrap = false;
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            // 
            // frmLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(954, 801);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtLogFilter);
            this.Controls.Add(this.lblLogFilter);
            this.Controls.Add(this.btnLogExport);
            this.Controls.Add(this.btnLogCopy);
            this.Controls.Add(this.btnLogOK);
            this.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VDST - Log";
            this.Activated += new System.EventHandler(this.frmLog_Activated);
            this.Load += new System.EventHandler(this.frmLog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogOK;
        private System.Windows.Forms.Button btnLogCopy;
        private System.Windows.Forms.Button btnLogExport;
        private System.Windows.Forms.Label lblLogFilter;
        private System.Windows.Forms.TextBox txtLogFilter;
        private System.Windows.Forms.TextBox txtLog;
    }
}