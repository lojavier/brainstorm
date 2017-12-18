using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace vdst_v1
{
    public partial class frmSync : Form
    {
        private static SerialComm conn = null;
        private static int direction = 5;

        public frmSync()
        {
            InitializeComponent();

            try
            {
                cmbSyncPort.Text = frmMain.tblDefaults["cmbSyncPort"];
                cmbSyncBaud.Text = frmMain.tblDefaults["cmbSyncBaud"];
                chkSyncDuplex.Checked = (frmMain.tblDefaults["chkSyncDuplex"] == "true");
                cmbSyncDataBits.Text = frmMain.tblDefaults["cmbSyncDataBits"];
                cmbSyncStopBits.Text = frmMain.tblDefaults["cmbSyncStopBits"];
                cmbSyncParity.Text = frmMain.tblDefaults["cmbSyncParity"];
                cmbSyncFlowControl.Text = frmMain.tblDefaults["cmbSyncFlowControl"];
                cmbSyncTimeout.Text = frmMain.tblDefaults["cmbSyncTimeout"];
            }
            catch (Exception) { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmSync_Load(object sender, EventArgs e)
        {
            
        }

        private void tmrSyncError_Tick(object sender, EventArgs e)
        {
            tmrSyncError.Stop();
            statusSyncError.Text = "";
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            if (btnSync.Text == "Sync")
            {
                conn = new SerialComm(cmbSyncPort.Text, cmbSyncBaud.Text, chkSyncDuplex.Checked, cmbSyncDataBits.Text, cmbSyncStopBits.Text, cmbSyncParity.Text, cmbSyncFlowControl.Text);
                conn.Start(Int32.Parse(cmbSyncTimeout.Text) * (1000/tmrSync.Interval));
                btnSync.Text = "Stop";
                tmrSync.Start();
            }
            else
            {
                tmrSync.Stop();
                statusSyncError.Text = "Serial connection aborted.";
                tmrSyncError.Start();
                btnSync.Text = "Sync";
                statusSyncProgress.Value = 0;
                direction = 5;
            }
        }

        private void cmbSyncPort_TextChanged(object sender, EventArgs e)
        {
            btnSync.Enabled = (cmbSyncPort.Text != "");
        }

        private void tmrSync_Tick(object sender, EventArgs e)
        {            
            statusSyncProgress.Value += direction;

            if (statusSyncProgress.Value <= statusSyncProgress.Minimum || statusSyncProgress.Value >= statusSyncProgress.Maximum)
                direction = -direction;

            conn.tick();

            if (conn.ConnectionFailed)
            {
                tmrSync.Stop();
                statusSyncError.Text = "Serial connection timed out.";
                tmrSyncError.Start();
                btnSync.Text = "Sync";
                statusSyncProgress.Value = 0;
                direction = 5;
            }

        }

        private void cmbSyncPort_MouseDown(object sender, MouseEventArgs e)
        {
            string sPort = cmbSyncPort.Text;

            cmbSyncPort.Items.Clear();
            foreach (String strPort in SerialPort.GetPortNames())
            {
                cmbSyncPort.Items.Add(strPort);
            }

            cmbSyncPort.Text = sPort;
        }

        private void btnSyncSend_Click(object sender, EventArgs e)
        {
            if (conn == null) conn = new SerialComm(cmbSyncPort.Text, cmbSyncBaud.Text, chkSyncDuplex.Checked, cmbSyncDataBits.Text, cmbSyncStopBits.Text, cmbSyncParity.Text, cmbSyncFlowControl.Text);

            conn.write(txtSyncCommand.Text);
         }
    }
}
