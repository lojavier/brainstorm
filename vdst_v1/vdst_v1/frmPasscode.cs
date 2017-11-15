using System;
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
    public partial class frmPasscode : Form
    {
        int iTriesRemaining = vdstValues.PASSCODE_TRIES;
        public string sAdminPassword;
        public string sCurrID; 

        public frmPasscode()
        {
            InitializeComponent();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((sender as Form).DialogResult.ToString() == "OK")
            {
                bool isSuccess = false;
                string sOldID = txtPasscodeOld.Text.Substring(0, 2);
                string sOldPasscode = txtPasscodeOld.Text.Substring(2);
                string sNewID = txtPasscodeNew.Text.Substring(0, 2);
                string sNewPasscode = txtPasscodeNew.Text.Substring(2);
                string strPasscodeRetype = txtPasscodeRetype.Text.Substring(2);

                if (txtPasscodeNew.Text != txtPasscodeRetype.Text)
                {
                    txtPasscodeNew.SelectAll();
                    txtPasscodeNew.Focus();
                    lblPasscodeStatus.Text = "Retyped password does not match.";
                }
                else if (!txtPasscodeOld.Enabled && sNewID != sOldID)
                {
                    txtPasscodeNew.SelectAll();
                    txtPasscodeNew.Focus();
                                        
                    lblPasscodeStatus.Text = "Invalid password [1].";
                }
                else if (sCurrID != "" && sNewID != sCurrID)
                {
                    txtPasscodeOld.SelectAll();
                    txtPasscodeOld.Focus();

                    lblPasscodeStatus.Text = "Invalid password [2].";
                }
                else if (sAdminPassword !="" && sAdminPassword != txtPasscodeOld.Text)
                {
                    txtPasscodeOld.SelectAll();
                    txtPasscodeOld.Focus();

                    lblPasscodeStatus.Text = "Invalid password [3].";
                }
                else if (sNewPasscode == frmMain.myCurrUser.Passcode && !frmMain.myCurrUser.AllowPasswordReuse)
                {
                    txtPasscodeNew.SelectAll();
                    txtPasscodeNew.Focus();
                    lblPasscodeStatus.Text = "New passcode can not match old passcode.";
                }
                else
                {
                    isSuccess = true;
                    frmMain.isChanged = true;

                    if (sCurrID == frmMain.myCurrUser.ID || sNewID == frmMain.myCurrUser.ID)
                    {
                        frmMain.myCurrUser.RequiredPasswordChange = false;
                        frmMain.myCurrUser.Passcode = txtPasscodeNew.Text.Substring(2);
                    }
                    else
                    {
                        sCurrID = (sCurrID == "" ? sNewID : sCurrID);

                        User aUser = new User(sCurrID, frmMain.tblAccounts[sCurrID].Split(','));
                        aUser.Passcode = txtPasscodeNew.Text.Substring(2);
                        if (sCurrID!=frmMain.myCurrUser.ID) aUser.RequiredPasswordChange = true;
                        frmMain.tblAccounts[sCurrID] = aUser.getAccountValuesString();
                    }
                }

                if (!isSuccess) e.Cancel = true;                
            }


            // Tries Remaining: " + iTriesRemaining.ToString()
            //    if (iTriesRemaining < 1)
            //    {
            //        this.DialogResult = DialogResult.Cancel;
            //    }
 
            //}
        }

        private void txtPasscode_TextChanged(object sender, EventArgs e)
        {
            lblPasscodeStatus.Text = "";
            btnPasscodeOK.Enabled = (txtPasscodeOld.Text.Length == vdstValues.PASSCODE_LENGTH && txtPasscodeNew.Text.Length == vdstValues.PASSCODE_LENGTH && txtPasscodeRetype.Text.Length == vdstValues.PASSCODE_LENGTH);
        }

        private void txtPasscode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8;
        }

    }
}
