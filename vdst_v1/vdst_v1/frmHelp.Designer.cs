namespace vdst_v1
{
    partial class frmHelp
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
            this.lblHelpTopic = new System.Windows.Forms.Label();
            this.txtHelpSearch = new System.Windows.Forms.TextBox();
            this.txtHelpList = new System.Windows.Forms.ListBox();
            this.txtHelpText = new System.Windows.Forms.TextBox();
            this.btnHelpOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHelpTopic
            // 
            this.lblHelpTopic.AutoSize = true;
            this.lblHelpTopic.Location = new System.Drawing.Point(24, 25);
            this.lblHelpTopic.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHelpTopic.Name = "lblHelpTopic";
            this.lblHelpTopic.Size = new System.Drawing.Size(86, 32);
            this.lblHelpTopic.TabIndex = 0;
            this.lblHelpTopic.Text = "Topic:";
            // 
            // txtHelpSearch
            // 
            this.txtHelpSearch.Location = new System.Drawing.Point(30, 61);
            this.txtHelpSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtHelpSearch.Name = "txtHelpSearch";
            this.txtHelpSearch.Size = new System.Drawing.Size(406, 39);
            this.txtHelpSearch.TabIndex = 1;
            this.txtHelpSearch.TextChanged += new System.EventHandler(this.txtHelpSearch_TextChanged);
            // 
            // txtHelpList
            // 
            this.txtHelpList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHelpList.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHelpList.FormattingEnabled = true;
            this.txtHelpList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtHelpList.ItemHeight = 32;
            this.txtHelpList.Location = new System.Drawing.Point(30, 118);
            this.txtHelpList.Margin = new System.Windows.Forms.Padding(4);
            this.txtHelpList.Name = "txtHelpList";
            this.txtHelpList.Size = new System.Drawing.Size(406, 674);
            this.txtHelpList.TabIndex = 2;
            this.txtHelpList.SelectedIndexChanged += new System.EventHandler(this.txtHelpList_SelectedIndexChanged);
            // 
            // txtHelpText
            // 
            this.txtHelpText.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHelpText.Location = new System.Drawing.Point(454, 61);
            this.txtHelpText.Margin = new System.Windows.Forms.Padding(4);
            this.txtHelpText.Multiline = true;
            this.txtHelpText.Name = "txtHelpText";
            this.txtHelpText.Size = new System.Drawing.Size(695, 731);
            this.txtHelpText.TabIndex = 3;
            // 
            // btnHelpOK
            // 
            this.btnHelpOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnHelpOK.Location = new System.Drawing.Point(977, 810);
            this.btnHelpOK.Name = "btnHelpOK";
            this.btnHelpOK.Size = new System.Drawing.Size(172, 57);
            this.btnHelpOK.TabIndex = 4;
            this.btnHelpOK.Text = "OK";
            this.btnHelpOK.UseVisualStyleBackColor = true;
            // 
            // frmHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.CancelButton = this.btnHelpOK;
            this.ClientSize = new System.Drawing.Size(1179, 900);
            this.Controls.Add(this.txtHelpText);
            this.Controls.Add(this.lblHelpTopic);
            this.Controls.Add(this.txtHelpList);
            this.Controls.Add(this.txtHelpSearch);
            this.Controls.Add(this.btnHelpOK);
            this.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHelp";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VDST - Help";
            this.Load += new System.EventHandler(this.frmHelp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHelpTopic;
        private System.Windows.Forms.TextBox txtHelpSearch;
        private System.Windows.Forms.ListBox txtHelpList;
        private System.Windows.Forms.TextBox txtHelpText;
        private System.Windows.Forms.Button btnHelpOK;
    }
}