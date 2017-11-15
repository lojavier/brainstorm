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
    public partial class frmDefaults : Form
    {
        private static String strLastHelpScreen = "";
        private static String strLastHelpField = "";
        private static bool isClicked = false;
        private string strPrevValue = "";

        public frmDefaults()
        {
            InitializeComponent();
            setFieldsToDefaults(frmMain.tblDefaults, "Defaults");

        }

        private void frmDefaults_Load(object sender, EventArgs e)
        {
            cmbDefaultsHelpScreen.Text = cmbDefaultsHelpScreen.Items[0].ToString();
            cmbDefaultsHelpField.Text = cmbDefaultsHelpField.Items[0].ToString();
        }

        private void setFieldsToDefaults(SortedDictionary<string, string> oData, string strForm)
        {
            //ToDo: Move this uplicate function to a shared code library

            foreach (KeyValuePair<string, string> kvpItem in oData)
            {
                string strFieldType = kvpItem.Key.Substring(0, 3);
                string strName = kvpItem.Key.Substring(3);
                string strFieldName = strFieldType + strForm + strName;               
                Control ctl = Controls.Find(strFieldName, true).FirstOrDefault();

                if (ctl != null)
                {
                    if (strFieldType == "chk")
                        ((CheckBox)ctl).Checked = (kvpItem.Value == "true");
                    else if (strFieldType == "cmb")
                        ((ComboBox)ctl).Text = kvpItem.Value;
                    else if (strFieldType == "txt")
                        ((TextBox)ctl).Text = kvpItem.Value;
                }
            }
        }
        
        private void setDefaultsToFields(SortedDictionary<string, string> oData, string strField)
        {
            //ToDo: Move this uplicate function to a shared code library
            
            // Can't update list elements in foreach loop unless use .ToList
            foreach (string strKey in oData.Keys.ToList())
            {
                string strFieldType = strKey.Substring(0, 3);
                string strName = strKey.Substring(3);
                string strFieldName = strFieldType + strField + strName;
                Control ctl = Controls.Find(strFieldName, true).FirstOrDefault();

                if (ctl != null)
                {
                    if (strFieldType == "chk")
                        oData[strKey] = (((CheckBox)ctl).Checked ? "true" : "false");
                    else if (strFieldType == "cmb")
                        oData[strKey] = oData[strKey] = ((ComboBox)ctl).Text;
                    else if (strFieldType == "txt")
                        oData[strKey] = ((TextBox)ctl).Text;
                }         
            }
        }

        private void cmbDefaultsHelpScreen_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeSelections();
        }

        private void cmbDefaultsHelpScreen_MouseUp(object sender, EventArgs e)
        {
            isClicked = true;
        }

        private void cmbDefaultsHelpScreen_KeyDown(object sender, EventArgs e)
        {
            isClicked = false;
        }

        private void cmbDefaultsHelpField_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeSelections();
        }

        private void cmbDefaultsHelpField_MouseUp(object sender, EventArgs e)
        {
            isClicked = true;
        }

        private void cmbDefaultsHelpField_KeyDown(object sender, EventArgs e)
        {
            isClicked = false;
        }

        private void changeSelections()
        {
            try
            {
                if (strLastHelpScreen != "" && strLastHelpField != "")
                {
                    frmMain.tblFieldInfo[strLastHelpScreen + strLastHelpField] = txtDefaultsHelpText.Text;
                }
                strLastHelpScreen = cmbDefaultsHelpScreen.SelectedItem.ToString().Trim();
                strLastHelpField = cmbDefaultsHelpField.SelectedItem.ToString().Trim();
                txtDefaultsHelpText.Text = frmMain.tblFieldInfo[strLastHelpScreen + strLastHelpField];
            }
            catch (Exception)
            {
                txtDefaultsHelpText.ResetText();
            }

            if (isClicked)
            {
                txtDefaultsHelpText.Focus();
                txtDefaultsHelpText.SelectionLength = 0;
                txtDefaultsHelpText.SelectionStart = txtDefaultsHelpText.Text.Length;
                isClicked = false;
            }
        }

        private void btnDefaultsOK_Click(object sender, EventArgs e)
        {
            //changeSelections();
            setDefaultsToFields(frmMain.tblDefaults, "Defaults");
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void lblDefaultsMotorPowers_Click(object sender, EventArgs e)
        {

        }

        

       

        private void txtDefaultsTempAlarm1_TextChanged(object sender, EventArgs e)
        {
            txtDefaultsTempAlarm2.Enabled = (txtDefaultsTempAlarm1.Text != "");
            txtDefaultsTempAlarm3.Enabled = (txtDefaultsTempAlarm1.Text != "" && txtDefaultsTempAlarm2.Text != "");
            txtDefaultsTempAlarm4.Enabled = (txtDefaultsTempAlarm1.Text != "" && txtDefaultsTempAlarm2.Text != "" && txtDefaultsTempAlarm3.Text != "");
            txtDefaultsTempAlarm5.Enabled = (txtDefaultsTempAlarm1.Text != "" && txtDefaultsTempAlarm2.Text != "" && txtDefaultsTempAlarm3.Text != "" && txtDefaultsTempAlarm4.Text != "");
        }

        private void txtDefaultsTempAlarm2_TextChanged(object sender, EventArgs e)
        {
            txtDefaultsTempAlarm3.Enabled = (txtDefaultsTempAlarm2.Text != "");
            txtDefaultsTempAlarm4.Enabled = (txtDefaultsTempAlarm2.Text != "" && txtDefaultsTempAlarm3.Text != "");
            txtDefaultsTempAlarm5.Enabled = (txtDefaultsTempAlarm2.Text != "" && txtDefaultsTempAlarm3.Text != "" && txtDefaultsTempAlarm4.Text != "");
        }

        private void txtDefaultsTempAlarm3_TextChanged(object sender, EventArgs e)
        {
            txtDefaultsTempAlarm4.Enabled = (txtDefaultsTempAlarm3.Text != "");
            txtDefaultsTempAlarm5.Enabled = (txtDefaultsTempAlarm3.Text != "" && txtDefaultsTempAlarm4.Text != "");
        }

        private void txtDefaultsTempAlarm4_TextChanged(object sender, EventArgs e)
        {
            txtDefaultsTempAlarm5.Enabled = (txtDefaultsTempAlarm4.Text != "");
        }

        private void txtDefaultsLaserPower1_TextChanged(object sender, EventArgs e)
        {
            txtDefaultsLaserPower2.Enabled = (txtDefaultsLaserPower1.Text != "");
            txtDefaultsLaserPower3.Enabled = (txtDefaultsLaserPower1.Text != "" && txtDefaultsLaserPower2.Text != "");
            txtDefaultsLaserPower4.Enabled = (txtDefaultsLaserPower1.Text != "" && txtDefaultsLaserPower2.Text != "" && txtDefaultsLaserPower3.Text != "");
            txtDefaultsLaserPower5.Enabled = (txtDefaultsLaserPower1.Text != "" && txtDefaultsLaserPower2.Text != "" && txtDefaultsLaserPower3.Text != "" && txtDefaultsLaserPower4.Text != "");
        }

        private void txtDefaultsLaserPower2_TextChanged(object sender, EventArgs e)
        {
            txtDefaultsLaserPower3.Enabled = (txtDefaultsLaserPower2.Text != "");
            txtDefaultsLaserPower4.Enabled = (txtDefaultsLaserPower2.Text != "" && txtDefaultsLaserPower3.Text != "");
            txtDefaultsLaserPower5.Enabled = (txtDefaultsLaserPower2.Text != "" && txtDefaultsLaserPower3.Text != "" && txtDefaultsLaserPower4.Text != "");
        }

        private void txtDefaultsLaserPower3_TextChanged(object sender, EventArgs e)
        {
            txtDefaultsLaserPower4.Enabled = (txtDefaultsLaserPower3.Text != "");
            txtDefaultsLaserPower5.Enabled = (txtDefaultsLaserPower3.Text != "" && txtDefaultsLaserPower4.Text != "");
        }

        private void txtDefaultsLaserPower4_TextChanged(object sender, EventArgs e)
        {
            txtDefaultsLaserPower5.Enabled = (txtDefaultsLaserPower4.Text != "");
        }

        private void txtDefaultsMotorSpeed1_TextChanged(object sender, EventArgs e)
        {
            txtDefaultsMotorSpeed2.Enabled = (txtDefaultsMotorSpeed1.Text != "");
            txtDefaultsMotorSpeed3.Enabled = (txtDefaultsMotorSpeed1.Text != "" && txtDefaultsMotorSpeed2.Text != "");
            txtDefaultsMotorSpeed4.Enabled = (txtDefaultsMotorSpeed1.Text != "" && txtDefaultsMotorSpeed2.Text != "" && txtDefaultsMotorSpeed3.Text != "");
            txtDefaultsMotorSpeed5.Enabled = (txtDefaultsMotorSpeed1.Text != "" && txtDefaultsMotorSpeed2.Text != "" && txtDefaultsMotorSpeed3.Text != "" && txtDefaultsMotorSpeed4.Text != "");
        }

        private void txtDefaultsMotorSpeed2_TextChanged(object sender, EventArgs e)
        {
            txtDefaultsMotorSpeed3.Enabled = (txtDefaultsMotorSpeed2.Text != "");
            txtDefaultsMotorSpeed4.Enabled = (txtDefaultsMotorSpeed2.Text != "" && txtDefaultsMotorSpeed3.Text != "");
            txtDefaultsMotorSpeed5.Enabled = (txtDefaultsMotorSpeed2.Text != "" && txtDefaultsMotorSpeed3.Text != "" && txtDefaultsMotorSpeed4.Text != "");
        }

        private void txtDefaultsMotorSpeed3_TextChanged(object sender, EventArgs e)
        {
            txtDefaultsMotorSpeed4.Enabled = (txtDefaultsMotorSpeed3.Text != "");
            txtDefaultsMotorSpeed5.Enabled = (txtDefaultsMotorSpeed3.Text != "" && txtDefaultsMotorSpeed4.Text != "");
        }

        private void txtDefaultsMotorSpeed4_TextChanged(object sender, EventArgs e)
        {
            txtDefaultsMotorSpeed5.Enabled = (txtDefaultsMotorSpeed4.Text != "");
        }

        private void txtDefaultsTempAlarmInterval_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtControlNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control ctl = (Control)sender;

            bool isNotLeadingMinus = (e.KeyChar != 45 || (e.KeyChar == 45 && ((TextBox)ctl).SelectionStart > 0)); //ctl.Text.Length > 0;
            e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 11 && isNotLeadingMinus;
        }

        private void txtControlNumeric_LostFocus(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            String strName = ctl.Name;
            int iValue;

            // If the tag is NULL, it does not contain a note 'Allow Negative'
            if (!Int32.TryParse(ctl.Text, out iValue) || (ctl.Tag == null && iValue < 0))
                ctl.Text = strPrevValue;
            else
                ctl.Text = iValue.ToString();

            checkFieldAgainstMinMax(strName);
        }

        private void txtControlNumericPair_LostFocus(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            string strName = ctl.Name;
            int iValue;

            tmrDefaultsValueSwitch.Tag = strName.Substring(0, strName.Length - 3);
            tmrDefaultsValueSwitch.Start();

            // If the tag is NULL, it does not contain a note 'Allow Negative'
            if (!Int32.TryParse(ctl.Text, out iValue) || (ctl.Tag == null && iValue < 0))
                ctl.Text = strPrevValue;
            else
                ctl.Text = iValue.ToString();
        }

        private void txtControlNumeric_GotFocus(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            strPrevValue = ctl.Text;
        }
        private void txtControlNumericPair_GotFocus(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            string strName = ctl.Name;
            strPrevValue = ctl.Text;

            if (tmrDefaultsValueSwitch.Enabled && strName.IndexOf(tmrDefaultsValueSwitch.Tag.ToString()) > -1)
            {
                tmrDefaultsValueSwitch.Stop();
            }
        }
        
        private void checkFieldAgainstMinMax(string strField)
        {
            // Get rid of 'PowerX' at the end of the field name
            string strPrefix = strField.Substring(0, strField.Length - 6);

            TextBox txtMin = Controls.Find(strPrefix + "RangeMin", true).FirstOrDefault() as TextBox;
            TextBox txtMax = Controls.Find(strPrefix + "RangeMax", true).FirstOrDefault() as TextBox;
            TextBox txtVal = Controls.Find(strField, true).FirstOrDefault() as TextBox;

            int iMinValue;
            int iMaxValue;
            int iValue;

            if (txtMin != null && txtMax != null && Int32.TryParse(txtVal.Text, out iValue))
            {
                if (Int32.TryParse(txtMin.Text, out iMinValue) && iValue < iMinValue)
                    txtVal.Text = txtMin.Text;
                else if (Int32.TryParse(txtMax.Text, out iMaxValue) && iValue > iMaxValue)
                    txtVal.Text = txtMax.Text;
            }
        }

        private void tmrDefaultsValueSwitch_Tick(object sender, EventArgs e)
        {
            tmrDefaultsValueSwitch.Stop();

            int iMinValue;
            int iMaxValue;
            string strPrefix = tmrDefaultsValueSwitch.Tag.ToString();
            TextBox txtMin = Controls.Find(strPrefix + "Min", true).FirstOrDefault() as TextBox;
            TextBox txtMax = Controls.Find(strPrefix + "Max", true).FirstOrDefault() as TextBox;

            if (Int32.TryParse(txtMin.Text, out iMinValue) && Int32.TryParse(txtMax.Text, out iMaxValue))
            {
                if (iMinValue > iMaxValue)
                {
                    txtMin.Text = iMaxValue.ToString();
                    txtMax.Text = iMinValue.ToString();
                }
            }
        }

        private void chkDefaultsAccountsLocked_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbDefaultsSyncPort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbDefaultsSyncPort_MouseDown(object sender, MouseEventArgs e)
        {
            string sPort = cmbDefaultsSyncPort.Text;

            cmbDefaultsSyncPort.Items.Clear();
            foreach (String strPort in SerialPort.GetPortNames())
            {
                cmbDefaultsSyncPort.Items.Add(strPort);
            }

            cmbDefaultsSyncPort.Text = sPort;
        }
    }
}
