using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vdst_v1
{
    public partial class frmLog : Form
    {
        public frmLog()
        {
            InitializeComponent();
        }

        private void frmLog_Load(object sender, EventArgs e)
        {
            setLogText("");

        }

        private void frmLog_Activated(object sender, EventArgs e)
        {
            
        }

        private void lblLogFilter_Click(object sender, EventArgs e)
        {

        }

        private void btnLogOK_Click(object sender, EventArgs e)
        {

        }

        private void txtLogFilter_TextChanged(object sender, EventArgs e)
        {
            setLogText(txtLogFilter.Text);
        }

        private void setLogText(String strFilter)
        {
            bool isNotFirstEntry = false;
            txtLog.Clear();

            foreach (String strEntry in frmMain.lstLog)
            { 
                if (strEntry.IndexOf(strFilter) >= 0)
                {
                    if (isNotFirstEntry) txtLog.AppendText(Environment.NewLine);
                    txtLog.AppendText(strEntry);
                    isNotFirstEntry = true;
                }
            }
        }

        private void btnLogCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtLog.Text);
        }

        private void btnLogExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlgSaveLog = new SaveFileDialog();
            dlgSaveLog.Filter = "Text File|*.txt";
            dlgSaveLog.Title = this.Text + " - Export";
            dlgSaveLog.ShowDialog();

            if (dlgSaveLog.FileName != "")
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(dlgSaveLog.FileName))
                    {
                        writer.Write(txtLog.Text);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("An error occured writing the file.", this.Text + " - Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }              
            }
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
