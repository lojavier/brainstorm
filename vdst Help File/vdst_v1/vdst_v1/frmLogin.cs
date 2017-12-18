using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace vdst_v1
{
    public partial class frmLogin : Form
    {
        int iTriesRemaining = vdstValues.PASSCODE_TRIES;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((sender as Form).DialogResult.ToString() == "OK")
            {
                // get user from string, load values for user, check passcode part

                bool isSuccess = false;
                string strID = txtLoginCode.Text.Substring(0, 2);
                string strPasscode = txtLoginCode.Text.Substring(2);

                foreach (KeyValuePair<string, string> kvpItem in frmMain.tblAccounts)
                {
                    frmMain.myCurrUser = new User(kvpItem.Key, kvpItem.Value.Split(','));
                    if (frmMain.myCurrUser.ID == strID) break;
                }
                
                if (!frmMain.myCurrUser.Enabled)
                    lblLoginStatus.Text = "Account has been disabled.";
                else if (frmMain.myCurrUser.Locked)
                    lblLoginStatus.Text = "Account is locked.";
                else if (strPasscode != frmMain.myCurrUser.Passcode)
                {
                    iTriesRemaining--;
                    lblLoginStatus.Text = "Invalid Login. Tries Remaining: " + iTriesRemaining.ToString();
                }
                else
                    isSuccess = true;

                if (!isSuccess)
                {
                    if (iTriesRemaining < 1)
                    {
                        frmMain.myCurrUser.Locked = true;
                        frmMain.isChanged = true;
                        this.DialogResult = DialogResult.Cancel;
                    }
                    else
                    {
                        txtLoginCode.SelectAll();
                        txtLoginCode.Focus();
                        e.Cancel = true;
                    }
                }
            }
        }

        private void updatePasswordfromButton(String sNumber)
        {
            if (txtLoginCode.SelectionLength > 0)
            {
                txtLoginCode.SelectedText = sNumber;
            }
            else if (txtLoginCode.Text.Length < vdstValues.PASSCODE_LENGTH)
            {
                txtLoginCode.AppendText(sNumber);
            }

            txtLoginCode.Focus();
        }

        private void btnLogin1_Click(object sender, EventArgs e)
        {
            updatePasswordfromButton((sender as Button).Text);
        }

        private void btnLogin2_Click(object sender, EventArgs e)
        {
            updatePasswordfromButton((sender as Button).Text);

        }

        private void btnLogin3_Click(object sender, EventArgs e)
        {
            updatePasswordfromButton((sender as Button).Text);
        }

        private void btnLogin4_Click(object sender, EventArgs e)
        {
            updatePasswordfromButton((sender as Button).Text);
        }

        private void btnLogin5_Click(object sender, EventArgs e)
        {
            updatePasswordfromButton((sender as Button).Text);
        }

        private void btnLogin6_Click(object sender, EventArgs e)
        {
            updatePasswordfromButton((sender as Button).Text);
        }

        private void btnLogin7_Click(object sender, EventArgs e)
        {
            updatePasswordfromButton((sender as Button).Text);
        }

        private void btnLogin8_Click(object sender, EventArgs e)
        {
            updatePasswordfromButton((sender as Button).Text);
        }

        private void btnLogin9_Click(object sender, EventArgs e)
        {
            updatePasswordfromButton((sender as Button).Text);
        }

        private void btnLogin0_Click(object sender, EventArgs e)
        {
            updatePasswordfromButton((sender as Button).Text);
        }

        private void btnLoginBack_Click(object sender, EventArgs e)
        {
            int iInputLength = txtLoginCode.Text.Length;
            if (iInputLength > 0)
            {
                txtLoginCode.Text = txtLoginCode.Text.Substring(0, iInputLength - 1);
            }
        }

        private void btnLoginX_Click(object sender, EventArgs e)
        {
            txtLoginCode.Text = "";
        }

        private void txtLoginCode_TextChanged(object sender, EventArgs e)
        {
            lblLoginStatus.Text = "";
            btnLoginOK.Enabled = (txtLoginCode.Text.Length == vdstValues.PASSCODE_LENGTH);
        }
        private void txtLoginCode_KeyPress(object sender, KeyPressEventArgs e)
        { 
            e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
        }        
    }
}
